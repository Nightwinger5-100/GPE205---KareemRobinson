using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AiController : Controller
{

    //the possible states the ai can be in
    public enum AiState {Guard, Chase, Flee, Patrol, Attack, Scan, BackToPost};

    //the current state the ai is in
    public AiState currentState;

    //the prev state the ai machine is in
    private float lastStateChangeTime;

    //the object the ai will base it's actions around
    public GameObject target;

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

    public void MakeDecisions()
    {
        switch (currentState) {
            case AiState.Guard:
                // Do work for StateOne
                // Check for transitions
                break;
            case AiState.Chase:
                Chase(target);
                break;
            case AiState.Flee:
                // Do work for StateOne
                // Check for transitions
                break;
            
        }
    }
    protected void DoIdleState()
    {
        //do nothing while in this state
    }

    public void DoChaseState()
    {
        Chase(target);
    }

    public void Chase (GameObject target)
    {
        // RotateTowards the object
        pawn.RotateTowards(target.transform.position);
        // Move Forward
        pawn.MoveForward(false);
    }
}
