using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GambleHealthPowerup : Powerup
{
    public float healthToGamble;
    public float gambleMultiplier;

    //takes away health from the player based on however much is being gambled
    public override void Apply(PowerUpManager target)
    {
        // grab the health and pawn components from the target
        Health health = target.GetComponent<Health>();
        Pawn pawn = target.GetComponent<Pawn>();
        
        //if the gambled amount would kill the pawn then just gamble them to 1 hp
        if (healthToGamble >= health.currentHealth)
        {
            healthToGamble = health.currentHealth-1;
        }

        //check if they have both components
        if (health != null && pawn != null)
        {
            //take the gambled amount from the pawn
            health.TakeDamage(healthToGamble, pawn);
        }
    }

   //returns the gambled health amount times the gamble multipler if the pawn is still alive
    public override void Remove(PowerUpManager target)
    {
        // grab the health and pawn components from the target
        Health health = target.GetComponent<Health>();
        Pawn pawn = target.GetComponent<Pawn>();
        
        //check if they have both components
        if (health != null && pawn != null)
        {
            //apply the heal amount time the gamble multipler to the target's health
            health.Heal(healthToGamble*gambleMultiplier, pawn);
        }
    }
    
}
