using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class PlayerController : Controller
{
//movement and rotation keys
public KeyCode moveForwardKey;
public KeyCode moveBackwardKey;
public KeyCode rotateClockwiseKey;

public KeyCode rotateCounterClockwiseKey;

//shoot projectile key
public KeyCode shootKey;

    // Start is called before the first frame update
    public override void Start()
    {
        //if the gameManager exists
       if(GameManager.instance != null) 
       {
        //if the list "players" exists
        if(GameManager.instance.players != null) 
        {
            //add this player to the list
            GameManager.instance.players.Add(this);
        }
       }
    //run Start() from the parent class
    base.Start();
    }

    //remove the player from the gameManage list if they died
    public void Destroy()
    {
        //if the gameManager exists
       if(GameManager.instance != null) 
       {
        //if the list "players" exists
        if(GameManager.instance.players != null) 
        {
            //remove this player from the list
            GameManager.instance.players.Remove(this);
        }
       }
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
        if (Input.GetKey(shootKey))
        {
            pawn.shoot();
        }
    }

}
