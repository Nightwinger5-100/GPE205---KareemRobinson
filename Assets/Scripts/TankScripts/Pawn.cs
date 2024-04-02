using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{

    //Movement and Turn Speed variables
    public float moveSpeed;

    public float turnSpeed;


    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }


public abstract void MoveForward();
public abstract void MoveBackward();
public abstract void RotateClockwise();
public abstract void RotateCounterClockwise();


}