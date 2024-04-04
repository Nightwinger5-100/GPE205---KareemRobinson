using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;

public class TankMover : Mover
{
    //the object's rigidBody that'll be used to control it's movement and rotation
    private Rigidbody rb;

    public override void Start()
    {
        //find the rigidBody
        rb = GetComponent<Rigidbody>();
    }

    public override void Move(Vector3 direction, float speed)
    {
        //get the direction(with a magnitude of 1) times the speed and the time(frameBased to timeBased)
        //then take that number and add it to the position of the tank
        Vector3 moveVector = direction.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.position + moveVector);
    }

    public override void Rotate(float rotateSpeed)
    {
        //multiply the rotate speed by the deltaTime(frameBased to timeBased)
        //then take that number and rotate the tank that value
        Debug.Log("hello");
        float rotateVector = rotateSpeed * Time.deltaTime;
        rb.transform.Rotate(0,rotateVector,0);
    }
}
