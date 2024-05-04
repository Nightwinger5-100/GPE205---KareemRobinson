using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveHealthPickup : MonoBehaviour
{
    public HealthPowerup powerup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void destroyPickupSound()
    {
        AudioSource audio = ((GameObject) Instantiate (powerup.powerUpAudioSource, transform.position, Quaternion.identity)).GetComponent<AudioSource>();

        audio.clip = powerup.powerUpSound;

        audio.Play();
    }

    //on colliding with the parent
    public void OnTriggerEnter(Collider other)
    {
        //grab the powerUpManager components from the target
        PowerUpManager powerUpManager = other.GetComponent<PowerUpManager>();

        //check if the component exists then...
        if (powerUpManager != null)
        {
            destroyPickupSound();
            //remove said power up and destroy itself
            powerUpManager.Remove(powerup);
        
            Destroy(gameObject);
        }
    }
}
