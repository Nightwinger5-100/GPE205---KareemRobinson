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
public abstract void MoveForward();
public abstract void MoveBackward();
public abstract void RotateClockwise();
public abstract void RotateCounterClockwise();

public abstract void shoot();
}