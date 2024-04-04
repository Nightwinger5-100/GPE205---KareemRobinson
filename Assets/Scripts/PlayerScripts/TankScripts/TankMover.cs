using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class TankMover : Mover
{
    private Rigidbody rb;

    public override void Start()
    {
        //find the rigidBody
        rb = GetComponent<Rigidbody>();
    }

    public override void Move(Vector3 direction, float speed)
    {
        Vector3 moveVector = direction.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.position + moveVector);
    }

    public override void Rotate(float rotateSpeed)
    {
        rb.transform.Rotate(0,rotateSpeed,0);
    }
}
