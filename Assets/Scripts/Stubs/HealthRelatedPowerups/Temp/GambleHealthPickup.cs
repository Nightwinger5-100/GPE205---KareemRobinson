using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambleHealthPickup : MonoBehaviour
{
    public GambleHealthPowerup powerup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        public void OnTriggerEnter(Collider other)
    {
        //grab the powerUpManager components from the target
        PowerUpManager powerUpManager = other.GetComponent<PowerUpManager>();

        //check if the component exists then...
        if (powerUpManager != null)
        {
            //apply said power up and destroy itself
            powerUpManager.Add(powerup);
        
            Destroy(gameObject);
        }
    }
}
