using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject pickupPrefab;
    public float spawnDelay;
    private float nextSpawnTime;
    private Transform tf;
    private GameObject spawnedPickup;
    
    void Start()
    {
        //set the spawn time
        nextSpawnTime = spawnDelay + Time.time;
        tf = GetComponent<Transform>();
    }

    
    void Update()
    {
        //if there's no pickup
        if (spawnedPickup == null)
        {
            //check if it's time to (re)spawn the pickup and...
            if (Time.time > nextSpawnTime)
            {
                //if so create it at the transform position and reset the timer
                spawnedPickup = Instantiate(pickupPrefab, tf.position, Quaternion.identity) as GameObject;
                nextSpawnTime = Time.time + spawnDelay;
            }
        }
        else
        {
            //re-set the spawn time
            nextSpawnTime = Time.time + spawnDelay;
        }
    }

    
}
