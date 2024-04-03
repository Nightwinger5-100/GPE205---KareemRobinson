using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
public KeyCode moveForwardKey;
public KeyCode moveBackwardKey;
public KeyCode rotateClockwiseKey;

public KeyCode rotateCounterClockwiseKey;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame so it will check if the key is down every frame
    public override void Update()
    {
        ProcessInputs();
        
        base.Update();
    }

    public override void ProcessInputs()
    {
        if (Input.GetKey(moveForwardKey))
        { 
            Debug.Log("pressed W");
            pawn.MoveForward();
        }
        if (Input.GetKey(moveBackwardKey))
        { 
            Debug.Log("pressed s");
            pawn.MoveBackward();
        }
        if (Input.GetKey(rotateClockwiseKey))
        { 
            Debug.Log("pressed d");
            pawn.RotateClockwise();
        }
        if (Input.GetKey(rotateCounterClockwiseKey))
        { 
            //Debug.Log("pressed a");
            pawn.RotateCounterClockwise();
        }
    }
}
