using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
//movement and rotation keys
public KeyCode moveForwardKey;
public KeyCode moveBackwardKey;
public KeyCode rotateClockwiseKey;

public KeyCode rotateCounterClockwiseKey;

    // Start is called before the first frame update
    public override void Start()
    {
        //get the parent start()
        base.Start();
    }

    // Update is called once per frame so it will check if the key is down every frame
    public override void Update()
    {
        //check for inputs
        ProcessInputs();

        //get the parent Update()
        base.Update();
    }
    
    //checks to see if the movement or rotation keys are down
    //if they're down then run the function for that action
    public override void ProcessInputs()
    {

        if (Input.GetKey(moveForwardKey))
        { 
            pawn.MoveForward();
        }
        if (Input.GetKey(moveBackwardKey))
        { 
            pawn.MoveBackward();
        }
        if (Input.GetKey(rotateClockwiseKey))
        { 
            pawn.RotateClockwise();
        }
        if (Input.GetKey(rotateCounterClockwiseKey))
        { 
            pawn.RotateCounterClockwise();
        }
    }
}
