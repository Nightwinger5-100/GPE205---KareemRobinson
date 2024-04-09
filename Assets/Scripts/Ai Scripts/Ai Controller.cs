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

    //the current state the ai is in
    public AiState currentState;

    //the prev state the ai machine is in
    private float lastStateChangeTime;

    //the object the ai will base it's actions around
    public GameObject target;

    //the distance for acknowleding the target
    public float distance;

    //the distance for running away
    public float fleeDistance;

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

    // Start is called before the first frame update
    public override void Start()
    {
        //run the parent start()
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        //run makeDecisions
        MakeDecisions();
        //run parent update()
        base.Update();
    }

    public virtual void ChangeState (AiState newState)
    {
        // Change the current state
        currentState = newState;
        // Save the time when we changed states
        lastStateChangeTime = Time.time;

    }

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

    public void MakeDecisions()
    {
        switch (currentState) {
            case AiState.Idle:
                DoIdleState();
                if (IsDistanceLessThan(target, distance))
                {
                   ChangeState(AiState.Chase); 
                }
                break;
            case AiState.Chase:
                DoChaseState();
                if (!IsDistanceLessThan(target, distance))
                {
                   ChangeState(AiState.Idle); 
                }
                break;
            case AiState.Flee:
                if (!IsDistanceLessThan(target, distance))
                {
                   ChangeState(AiState.Idle); 
                }
                break;
            
        }
    }

    protected bool IsDistanceLessThan(GameObject target, float distance)
    {
        if(Vector3.Distance (pawn.transform.position, target.transform.position) < distance)
        {
            return true;
        }
        //HEY IF ITS NOT WORKING ADD AN ELSE STATEMENT HERE
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
        //have the pawn shoot
        pawn.Shoot();
    }
    
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

    //
    public void DoSeekState()
    {
        // Seek our target
        Seek(target);
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
    protected void RestartPatrol()
    {
        //set the current waypoint to 0
        currentWaypoint = 0;
    }

    protected bool IfHasTarget()
    {
        //return true if we have a target and false otherwise
        return (target != null);
    }
}

