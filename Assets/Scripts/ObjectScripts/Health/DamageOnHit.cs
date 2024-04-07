using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    public float damageDone;
    public Pawn owner;

    public void OnTriggerEnter(Collider other)
    {
        //Get the Health comp from the Game object that has the Collider that we are overlapping
        Health otherHealth = other.gameObject.GetComponent<Health>();
        //Only damage if it has a Health Component
        if (otherHealth != null)
        {   
            print(owner);
            //Do damage
            otherHealth.TakeDamage(damageDone, owner);
        }
        
        //destroy self after trigger
        Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
