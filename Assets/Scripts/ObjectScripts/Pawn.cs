using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Pawn : MonoBehaviour
{

    //Movement and Turn Speed variables

    public float moveSpeed;

    public float turnSpeed;

    //the object to move
    public Mover mover;

    //how fast the pawn can shoot
    public float fireRate;
    
    //the number the moveSpeed is multiplied by when sprinting
    public float sprintSpeedIncrease;

    // Start is called before the first frame update
    public virtual void Start()
    {
        fireRate = 1 / fireRate;
        //connect mover to pawn
        mover = GetComponent<Mover>();
        
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

//the movement and rotation that'll be defined in the child(ren)
public abstract void MoveForward(bool sprintBool);
public abstract void MoveBackward(bool sprintBool);
public abstract void RotateClockwise(bool sprintBool);
public abstract void RotateCounterClockwise(bool sprintBool);

public abstract void Shoot();

public virtual void IsPawnSprinting()
{
    
}
}