using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooter : Shooter
{
    public Transform firepointTransform;

    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override void Shoot(GameObject bulletPrefab, float fireForce, float damageDone, float lifeTime)
    {
        // Instantiate bullet
        GameObject newBullet = Instantiate(bulletPrefab, firepointTransform.position, firepointTransform.rotation) as GameObject;

        // Get the DamageOnHit component
        DamageOnHit doh = newBullet.GetComponent<DamageOnHit>();
        // If it has one 
        if (doh != null) 
        {
            //set the damageDone in the DamageOnHit component to the value passed in
            doh.damageDone = damageDone;
            //set the doh owner to the pawn that shot the bullet, if there is one (otherwise, owner is null).
            doh.owner = GetComponent<Pawn>();
        }

        // Get the rigidbody 
        Rigidbody rb = newBullet.GetComponent<Rigidbody>();
        Debug.Log(rb);
        // If it has one
        if (rb != null) 
        {
            //AddForce to make it move forward
            rb.AddForce(firepointTransform.forward * fireForce);
        }
        // Destroy it after a set time
        Destroy(newBullet, lifeTime);
    }
}
