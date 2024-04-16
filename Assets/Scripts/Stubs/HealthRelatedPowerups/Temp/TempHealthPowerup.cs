using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class TempHealthPowerup : Powerup
{
    public float healSpeed;
    public float healthToAdd;
    public float healthToRemove;

    //apply health

    public override void Apply(PowerUpManager target)
    {
        // grab the health and pawn components from the target
        Health health = target.GetComponent<Health>();
        Pawn pawn = target.GetComponent<Pawn>();
        

        //check if they have both components
        if (health != null && pawn != null)
        {
            //apply the heal amount to the target's health
            health.HealOvertime(healthToAdd, duration, healSpeed, pawn);

        }
    }

    //remove health
    public override void Remove(PowerUpManager target)
    {
        Debug.Log("Heal overtime finished");
    }
    
}
