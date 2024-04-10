using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class AiController : Controller
{

    //the possible states the ai can be in
    public enum AiState {Guard, Chase, Flee, Patrol, Attack, Scan, BackToPost, Idle};
    
    public bool canGuard;

    public bool canChase;

    public bool canFlee;

    public bool canPatrol;
    
    public bool canAttack;

    public bool canScan;

    public bool canBackToPost;


    //the current state the ai is in
    public AiState currentState;

    //the prev state the ai machine is in
    private float lastStateChangeTime;

    //the object the ai will base it's actions around
    public GameObject target;

    //the distance for acknowleding the target
    public float chaseDistance;

    //stores the original chaseDistance
    private float defaultChaseDistance;

    //the distance for running away
    public float fleeDistance;

    //stores the original fleeDistance
    private float defaultFleeDistance;

    //increase the distance by this amount for distance variables to make it less stiff
    public float distanceMultiplier;

    //the bool for if the ai is sprinting
    public bool sprintBool;

    //if the ai will restart their patrol upon reaching the end
    public bool resetPatrolBool;

    //an array of places to patrol between
    public Transform[] waypoints;

    //the distance to travel before halting within a patrol
    public float waypointStopDistance;

    //the current waypoint to patrol
    private int currentWaypoint = 0;
    
    //Adds target if necessary
    public override void Start()
    {
        //if there's currently no target make the first player the target
        if (AiHasTarget())
        {
           targetPlayerOne(); 
        }

        //store the original values of the distances
        defaultChaseDistance = chaseDistance;
        defaultFleeDistance = fleeDistance;

        //run the parent start()
        base.Start();
    }

    //Check if there's a target before running makeDecisions
    public override void Update()
    {
        //if there's a target...
        if (AiHasTarget())
        {
        //run makeDecisions
        MakeDecisions();    
        }
        else
        {
        targetPlayerOne();    
        }
        //run parent update()
        base.Update();
    }

    //set the first player as the target
    public void targetPlayerOne()
    {   
        //if the gameManager exists
        if (GameManager.instance != null)
        {   //if the player list exists
            if (GameManager.instance.players != null)
            {   
                //if there's at least 1 player
                if (GameManager.instance.players.Count > 0)
                {
                    //find the first instance of the player and make that the target
                    target = GameManager.instance.players[0].pawn.gameObject;
                }
            }
        }
    }

    //locate the closest tank out of all the tanks, then make that the target
    protected void FindNearestTank()
    {
        //get the list of  all the tanks
        Pawn[] allTanks = FindObjectsOfType<Pawn>();

        //make the first tank the closest
        Pawn closestTank = allTanks[0];

        //make the first tank the current closest distance
        float closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);

        //loop through each instance in the tank list
        foreach(Pawn tank in allTanks)
        {
            //check if the current tank distance is closer than the current closestTankDistance
            if (Vector3.Distance(pawn.transform.position, tank.transform.position) < closestTankDistance)
            {
                //if the tank is the closer make it the new closest tank
                closestTank = tank;
                //and make it's distance the new closest
                closestTankDistance = Vector3.Distance(pawn.transform.position, tank.transform.position);;
            }
        }
        //make the closest tank the target
        target = closestTank.gameObject;
    }

    //How do i reference all the players in the game?
    
    //the states the ai will switch between at any given time
    
    //
    public void MakeDecisions()
    {
        switch (currentState) 
        {
            //check if if needs to switch states and if those states are enabled!
            case AiState.Idle:
                DoIdleState();
                
                //check if they're within the distance to start the state
                if (IsDistanceLessThan(target, chaseDistance))
                {
                    if (canChase)
                    {
                    //store the normal chase distance and start chase state;
                    chaseDistance *= distanceMultiplier;
                    ChangeState(AiState.Chase); 
                    }
                }
                if (IsDistanceLessThan(target, fleeDistance))
                {
                    if (canFlee)
                    {
                    //store the normal flee distance and start flee state;
                    fleeDistance *= distanceMultiplier;
                    ChangeState(AiState.Flee); 
                    }
                }
                break;

            //check if they're in chase distance still
            case AiState.Chase:
                DoChaseState();
                if (!IsDistanceLessThan(target, chaseDistance))
                {
                    //reset chaseDistance and stance
                    chaseDistance = defaultChaseDistance; 
                    ChangeState(AiState.Idle); 
                }
                break;

            //check if they're in flee distance still
            case AiState.Flee:
                 Flee();
                if (!IsDistanceLessThan(target, fleeDistance))
                {
                    //reset fleeDistance and stance
                    fleeDistance = defaultFleeDistance;
                    ChangeState(AiState.Idle); 
                }
                break;
            
        }
    }

    //stores the previous state and updates the current state
    public virtual void ChangeState (AiState newState)
    {
        // Change the current state
        currentState = newState;
        // Save the time when we changed states
        lastStateChangeTime = Time.time;

    }

    //check the distance between the target and the ai
    protected bool IsDistanceLessThan(GameObject target, float distance)
    {
        if(Vector3.Distance (pawn.transform.position, target.transform.position) < distance)
        {
            return true;
        }
        return false;
    }

    //do nothing 
    protected void DoIdleState()
    {
        //do nothing while in this state
    }

    //do the gameObject Seek
    protected virtual void DoChaseState()
    {
        //move toward and face the target
        Seek(target);

        //check if the object can attack!
        if (canAttack)
        {
        pawn.Shoot();
        }
        
    }
    
    //do the seek action on the target
    public void DoSeekState()
    {
        // Seek our target
        Seek(target);
    }

    //the way the ai will move around to find a target
    protected void Patrol()
    {   
        //if the current waypoint is less than the final instance in the waypoint array
        if (waypoints.Length > currentWaypoint)
        {   
            //find the index of waypoint thats equal to the currentWaypoint
            Seek(waypoints[currentWaypoint]);
            //if close enough move onto the next way point
            if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) < waypointStopDistance)
            {
                currentWaypoint++;
            }
            //otherwise restart the patrol num
        }  
        //if we reached the end and want to reset the patrol
        else if (resetPatrolBool)   
        {
            RestartPatrol();
        }
    }

    //the way the ai will flee from a target
    protected void Flee()
    {
        //find the difference between the two vector3s of the target and pawn
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;

        //get the opposite of that vector
        Vector3 vectorAwayFromTarget = -vectorToTarget;

        //the direction to travel * the length of the travel distance
        Vector3 fleeVector = vectorAwayFromTarget.normalized * fleeDistance;

        //take the position of the pawn + the vector it'll travel and have it seek that path
        Seek(pawn.transform.position + fleeVector);
    }
    
    //Seek the target gameObject's position
    public void Seek (GameObject target)
    {
        Seek(target.transform.position);
    }

    //Seek the target transform component
    public void Seek (Transform targetTransform)
    {
        Seek(targetTransform.position);
    }

    //seek the pawn's transform
    public void Seek (Pawn targetPawn)
    {
        Seek(targetPawn.transform);
    }

    //Seek the controller owner
    public void Seek (Controller targetController)
    {
        Seek(targetController);
    }

    //Rotate toward a position and move toward it
    public void Seek(Vector3 targetPosition)
    {
        // RotateTowards the Funciton
        pawn.RotateTowards(targetPosition);
        // Move Forward
        pawn.MoveForward(sprintBool);
    }
    
    //retart the patrol
    protected void RestartPatrol()
    {
        //set the current waypoint to 0
        currentWaypoint = 0;
    }

    //check if there's a target
    protected bool AiHasTarget()
    {
        //return true if we have a target and false otherwise
        return (target != null);
    }

}

