using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    
    //as a static so this is the one that can be accessed anywhere
    public static GameManager instance;

    // Prefabs
    public GameObject playerControllerPrefab;
    public GameObject tankPawnPrefab;

    public GameObject tankMimicPrefab;
    
    //Spawnpoint
    public Transform playerSpawnTransform;

    public Transform enemySpawnTransform;

    public void SpawnPlayer()
    {
        // Spawn the Player Controller at (0,0,0) with no rotation
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        // Spawn the pawn and connect it to the controller
         GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation) as GameObject;

        // Get the Player Controller and Pawn components
        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        //Connect
        newController.pawn = newPawn;
    }
    
     public void SpawnEnemy()
    {
        // Spawn the enemy actor and connect it to the controller
         GameObject newPawnObj = Instantiate(tankMimicPrefab, enemySpawnTransform.position, enemySpawnTransform.rotation) as GameObject;

        // Get enemy components
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();
    }

    // As soon as this is created before even start()
    private void Awake()
    {
       // if instance doesn't exist
        if (instance == null) 
        {
            instance = this;
            //Don't destroy it if we load a new scene
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            // Otherwise, there is already an instance so destroy this instance
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        //Spawn the player
        SpawnPlayer();
    }

}
