using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    //power ups that are on a timer
    public List<Powerup> powerups;

    //power ups to remove
    private List<Powerup> removedPowerupQueue;
    
    // Start is called before the first frame update
    void Start()
    {
        //the list power ups will be added to and removed from
        powerups = new List<Powerup>();
    }

    // Update is called once per frame
    void Update()
    {
        DecrementPowerupTimers();
    }

    //runs ApplyRemovePowerupsQueue() after Update()
    private void LateUpdate()
    {
        ApplyRemovePowerupsQueue();
    }

    //adds the power up
    public void Add (Powerup powerupToAdd)
    {
        //add the power up and if the effect is temporary add it to the array
        powerupToAdd.Apply(this);
        if (!powerupToAdd.isPermanent)
        {
            powerups.Add(powerupToAdd);
        }
    }

    //removes the power up
    public void Remove (Powerup powerupToRemove)
    {
        //remove the power up and if the effect is temporary add it to the array
        powerupToRemove.Remove(this);
        if (!powerupToRemove.isPermanent)
        {
            removedPowerupQueue.Add(powerupToRemove);
        }

    }

    //removes power ups that need to be removed
    private void ApplyRemovePowerupsQueue()
    {
        //if there's any power ups that need to be removed...
        if (removedPowerupQueue.Count > 0)
        {
        foreach (Powerup powerup in removedPowerupQueue) 
        {
            powerups.Remove(powerup);
        }
        // And reset our temporary list
        removedPowerupQueue.Clear();        
        }

    }

    //handles the power ups on a timer
    public void DecrementPowerupTimers()
    {
        //if there's any power ups on a timer
        if (powerups.Count > 0)
        {
            //loop through the array
            foreach (Powerup powerup in powerups) 
            {
                // Subtract the time by the duration number
                powerup.duration -= Time.deltaTime;
                // If time is up remove this powerup
                if (powerup.duration <= 0) 
                {
                    Remove(powerup);
                }
            }
        }
    }
}
