using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    //the distance for chasing the target
    public float chaseDistance;

    //the distance for acknowleding the target
    public float guardDistance;

    //stores the original chaseDistance
    private float defaultChaseDistance;

    //stores the original guardDistance
    private float defaultGuardDistance;

    //the distance for running away
    public float fleeDistance;

    //stores the original fleeDistance
    private float defaultFleeDistance;

    //increase the distance by this amount for distance variables to make it less stiff
    public float distanceMultiplier;

    //the distance to travel before halting within a patrol
    public float waypointStopDistance;

    //an List of places to patrol between
    public List<Transform> waypoints = new List<Transform>();

    //if the ai will restart their patrol upon reaching the end
    public bool resetPatrolBool;

    //the bool for if the ai is sprinting
    public bool sprintBool;

    //if the target will be the closest in proxi
    public bool targetNearest;
    
    //the current waypoint to patrol
    private int currentWaypoint = 0;


    //the spawnPoint selected
    int spawn;

    //Adds target if necessary
    public override void Start()
    {

        //if the gameManager exists
       if(GameManager.instance != null) 
       {
        //if the list "ai" exists
        if(GameManager.instance.ai != null) 
        {
            //add this ai to the list
            GameManager.instance.ai.Add(this);
        }
    }
        

        //if there's currently no target make the first player the target
        if (AiHasTarget())
        {
            if (targetNearest)
            {
                FindNearestTank();
            }
            else
            {
                targetPlayerOne();
            }     
        }

        //store the original values of the distances
        defaultGuardDistance = guardDistance;
        defaultChaseDistance = chaseDistance;
        defaultFleeDistance = fleeDistance;

        //run the parent start()
        base.Start();
    }

    //Check if there's a target before running makeDecisions
    public override void Update()
    {
        if (pawn != null)
        {
            //if there's a target...
        if (AiHasTarget())
        {
        //run makeDecisions
        MakeDecisions();
 
        //check if they can listen for the target
        if (pawn != null && (pawn.GetComponent<NoiseMaker>())&&(pawn.GetComponent<NoiseMaker>().enabled))
        {
            pawn.GetComponent<NoiseMaker>().CanHear(target); 
        }  

        }

        if (targetNearest)
            {
                FindNearestTank();
            }
        else if (!AiHasTarget())
            {   
                targetPlayerOne();
            }      
        
        //run parent update()
        base.Update();
        }

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

    //How do i reference all the players in the game?
    
    //the states the ai will switch between at any given time
    public void MakeDecisions()
    {
        switch (currentState) 
        {
            //check if if needs to switch states and if those states are enabled!
            case AiState.Idle:
                DoIdleState();
                checkStates();
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
            
            //check if they're in guard distance still
            case AiState.Guard:
                DoGuardState();
                if (!IsDistanceLessThan(target, guardDistance))
                {
                    //reset chaseDistance and stance
                    guardDistance = defaultGuardDistance; 
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
        
            //check if they should still be patrolling or switching to another state
            case AiState.Patrol:

            if (waypoints.Count > 0)
            {
                Patrol();
            }
                checkStates();
            break;
        }
    }
    
    //check if the ai should be in either distanceBased state. Otherwise, make them patrol if they should be on patrol
    public void checkStates()
    {
        //check if they're within the distance to start the state
        if (pawn != null)
        {
                    if (canChase)
                {
                    if (IsDistanceLessThan(target, chaseDistance))
                    {
                    //store the normal chase distance and start chase state;
                    chaseDistance *= distanceMultiplier;
                    ChangeState(AiState.Chase); 
                    }
                else
                {
                    if (canPatrol)
                    {
                    ChangeState(AiState.Patrol);    
                    }
                }
                }
                else if (canGuard)
                {
                    if (IsDistanceLessThan(target, guardDistance))
                    {
                    //store the normal chase distance and start chase state;
                    guardDistance *= distanceMultiplier;
                    ChangeState(AiState.Guard); 
                    }
                else
                {
                    if (canPatrol)
                    {
                    ChangeState(AiState.Patrol);    
                    }
                }
                }
                else if (canFlee)
                {
                    if (IsDistanceLessThan(target, fleeDistance))
                    {
                    //store the normal flee distance and start flee state;
                    fleeDistance *= distanceMultiplier;
                    ChangeState(AiState.Flee); 
                    }
                else
                {
                    if (canPatrol)
                    {
                    ChangeState(AiState.Patrol);    
                    }
                }

                }
                else if (canPatrol)
                {
                    ChangeState(AiState.Patrol);    
                }
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
        //if their within the distance...
        if(Vector3.Distance (pawn.transform.position, target.transform.position) < distance)
        {
            Vision vision = pawn.GetComponent<Vision>();
            //check if they even have vision, otherwise just return true
            if(vision != null && vision.enabled)
            {
                //check if theyre visible
                return IsVisible(target);
            }
            else
            {
                return true;
            }
            
        }
        else
        {
            return false;
        } 
    }

    //check if the target is in their fov and if theyre vision is obstructed
    protected bool IsVisible(GameObject target)
    {
        return pawn.GetComponent<Vision>().CanSee(target);
    }

    //do nothing 
    protected void DoIdleState()
    {
        //do nothing while in this state
    }

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
    
    protected virtual void DoGuardState()
    {
        //move toward and/or face the target
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
        if (canPatrol)
        {
            //if the current waypoint is less than the final instance in the waypoint array
            if (waypoints.Count > currentWaypoint && pawn != null)
            {   
                
                //find the index of waypoint thats equal to the currentWaypoint
                Seek(waypoints[currentWaypoint].transform);
                //if close enough move onto the next way point
                if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].transform.position) < waypointStopDistance)
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

    }

    //the way the ai will flee from a target
    protected void Flee()
    {
        //the distance between the target and the ai
        float targetDistance = Vector3.Distance(target.transform.position, pawn.transform.position);
        //the percentage of that distance from the flee distance
        float percentOfFleeDistance = targetDistance / fleeDistance;
        //make sure percentOfFleeDistance number is between 100% and 0% distance
        percentOfFleeDistance = Mathf.Clamp01(percentOfFleeDistance);
        //invert the % so now, the farther the ai is the "closer" it flees(and vice versa)
        float flippedPercentOfFleeDistance = 1 - percentOfFleeDistance;

        //find the difference between the two vector3s of the target and pawn
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;

        //get the opposite of that vector
        Vector3 vectorAwayFromTarget = -vectorToTarget;

        //the direction to travel * the length of the travel distance
        Vector3 fleeVector = vectorAwayFromTarget.normalized * flippedPercentOfFleeDistance;
    
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
        print("sdwqcev");
        Seek(pawn);
    }

    //Rotate toward a position and move toward it
    public void Seek(Vector3 targetPosition)
    {
        if(currentState == AiState.Guard)
        {
            //print(targetPosition);
        }
        
        // RotateTowards the Funciton
        
        if (targetPosition != Vector3.zero)
        {
            pawn.RotateTowards(targetPosition);
        }
        
        
        if (canChase || canPatrol || canFlee)
        {
        // Move Forward
        pawn.MoveForward(sprintBool);
        }
        
    }
    
    //locate the closest tank out of all the tanks, then make that the target
    protected void FindNearestTank()
    {
        //get the list of  all the tanks
        Pawn[] allTanks = FindObjectsOfType<Pawn>();

        //make the first tank the closest
        Pawn closestTank = allTanks[0];
        
        if (closestTank == this.pawn && gameObject != null)
        {
            closestTank = allTanks[1];
        }
        
        //make the first tank the current closest distance
        float closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);

        //loop through each instance in the tank list
        foreach(Pawn tank in allTanks)
        {
            if (this.pawn != tank)
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

        }

        //make the closest tank the target
        target = closestTank.gameObject;
        //Debug.Log(target);
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

