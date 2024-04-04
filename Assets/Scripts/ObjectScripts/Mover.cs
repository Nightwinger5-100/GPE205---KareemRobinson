using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    //the start of the script that'll be defined in the child(ren)
    public abstract void Start();

    //the movement that holds the direction and speed it'll travel
    public abstract void Move(Vector3 direction, float speed);

    //the rotation that holds speed it'll rotate
    public abstract void Rotate(float rotateSpeed);
}
