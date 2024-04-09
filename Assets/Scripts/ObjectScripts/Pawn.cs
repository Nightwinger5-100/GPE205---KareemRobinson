using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Pawn : MonoBehaviour
{
    
    //the mover reference
    public Mover mover;

    //Movement and Turn Speed variables
    public float moveSpeed;

    public float turnSpeed;

    //how fast the pawn can shoot
    public float fireRate;
    
    //the number the moveSpeed is multiplied by when sprinting
    public float sprintSpeedIncrease;

    //the shooter reference
    public Shooter shooter;

    //the projectile that'll be shot
    public GameObject bulletPrefab;

    //the force of the projectile
    public float fireForce; 

    //the damage of the projectile
    public float damageDone;

    //the time the projectile will exist
    public float lifeTime;

    // Start is called before the first frame update
    public virtual void Start()
    {
        fireRate = 1 / fireRate;
        //connect mover to pawn
        mover = GetComponent<Mover>();
        //connect shooter to pawn
        shooter = GetComponent<Shooter>();
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

    //the shoot functions that'll be defined in the child(ren)
    public abstract void Shoot();

    //making the pawn face a position that'll be defined in the child(ren)
    public abstract void RotateTowards(Vector3 targetPosition);

    
}