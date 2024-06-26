using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    //variables
    //the next time the tank can shoot
    private float nextTimeCanShoot;
    public GameObject tankAudioSource;

    public AudioClip deathSfx;

    // Start is called before the first frame update
    public override void Start()
    {   

        nextTimeCanShoot = Time.time;
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
    public override void MoveForward(bool sprintBool)
    {
    if (gameObject != null)
    {
            if(sprintBool == true)
        {
        mover.Move(transform.forward, moveSpeed*sprintSpeedIncrease);
        }
        else
        {
        mover.Move(transform.forward, moveSpeed);
        }
    }

        
    }

    //get the object to move and move it backward based on the moveSpeed
    public override void MoveBackward(bool sprintBool)
    {
        
        if(sprintBool == true)
        {
        mover.Move(transform.forward, -moveSpeed*sprintSpeedIncrease);
        }
        else
        {
        mover.Move(transform.forward, -moveSpeed);
        }
    }

    //get the object to rotate and rotate it clockwise based on the turnSpeed
    public override void RotateClockwise(bool sprintBool)
    {
        if(sprintBool == true)
        {
        mover.Rotate(turnSpeed*sprintSpeedIncrease);
        }
        else
        {
        mover.Rotate(turnSpeed);
        }
    }

    //get the object to rotate and rotate it counterclockwise based on the turnSpeed
    public override void RotateCounterClockwise(bool sprintBool)
    {
        if(sprintBool == true)
        {
        mover.Rotate(-turnSpeed*sprintSpeedIncrease);
        }
        else
        {
        mover.Rotate(-turnSpeed);
        }
    }

    //check if shoot is on cd, if it's not then shoot and put it on cd
    public override void Shoot()
    {
        //if the time thats passed is greater than the next time they can shoot
        if (Time.time >= nextTimeCanShoot) 
        {
            //shoot and reset the shoot cooldown
            shooter.Shoot(bulletPrefab, fireForce, damageDone, lifeTime);
            nextTimeCanShoot = Time.time + fireRate;
        }
        else
        {
            //Debug.Log("Can't shoot yet bud");
        }
    }

    public override void RotateTowards(Vector3 targetPosition)
    {
        //find the difference between the two vector3s of the target and pawn
        Vector3 vectorToTarget = targetPosition - transform.position;

        //the vector to look at based on given the up direction
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);

        //rotate the object over time based on however fast their turnSpeed is, toward the target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    public void deathSound()
    {
        AudioSource audio = ((GameObject) Instantiate (tankAudioSource, transform.position, Quaternion.identity)).GetComponent<AudioSource>();

        audio.clip = deathSfx;

        audio.Play();
    }

}