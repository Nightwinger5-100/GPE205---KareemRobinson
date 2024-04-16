using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup
{
    //the list of powerups added
    public List<Powerup> powerups;
    
    //if the powerup will eventually be removed
    public bool isPermanent;

    //the duration before a powerup is removed
    public float duration;
    
    //applying the powerup 
    public abstract void Apply(PowerUpManager target);

    //removing the power up
    public abstract void Remove(PowerUpManager target);

}


