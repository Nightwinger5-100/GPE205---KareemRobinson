using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    //variables
    //the next time the tank can shoot
    private float nextTimeCanShoot;
    

    // Start is called before the first frame update
    public override void Start()
    {   

        nextTimeCanShoot = Time.time + fireRate;
        //get the parent Pawn start()
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        //get the parent pawn Update()
        base.Update();
    }

    //Moving and Rotation functions
    //get the object to move and move it forward based on the moveSpeed
    public override void MoveForward()
    {
        mover.Move(transform.forward, moveSpeed);
    }

    //get the object to move and move it backward based on the moveSpeed
    public override void MoveBackward()
    {
        
        mover.Move(transform.forward, -moveSpeed);
    }

    //get the object to rotate and rotate it clockwise based on the turnSpeed
    public override void RotateClockwise()
    {
        mover.Rotate(turnSpeed);
    }

    //get the object to rotate and rotate it counterclockwise based on the turnSpeed
    public override void RotateCounterClockwise()
    {
        mover.Rotate(-turnSpeed);
    }

    //check if shoot is on cd, if it's not then shoot and put it on cd
    public override void shoot()
    {
            if (Time.time >= nextTimeCanShoot) 
        {
            Debug.Log("Shoot");
            nextTimeCanShoot = Time.time + fireRate;
        }
        else
        {
            //Debug.Log("Can't shoot yet bud");
        }
    }
}
