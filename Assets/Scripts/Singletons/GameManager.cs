using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    
    //as a static so this is the one that can be accessed anywhere
    public static GameManager instance;

    // Controller Prefabs
    public GameObject playerControllerPrefab;

    public GameObject aiControllerPrefab;

    //player prefab
    public GameObject tankPawnPrefab;
    
    //ai prefabs
    public GameObject chaserAiPrefab;

    public GameObject guarderAiPrefab;

    public GameObject patrollerAiPrefab;

    public GameObject runnerAiPrefab;
    
    //player spawn point
    public Transform playerSpawnTransform;
    
    //enemy spawn points
    public Transform chaserAiSpawn;

    public Transform guarderAiSpawn;

    public Transform patrollerAiSpawn;

    public Transform runnerAiSpawn;
    


    //player list
    public List<PlayerController> players;

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

    public void SpawnChaserAi()
    {
        // Spawn the Player Controller at (0,0,0) with no rotation
        //GameObject aiObj = Instantiate(aiControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        // Spawn the pawn and connect it to the controller
         GameObject newPawnObj = Instantiate(chaserAiPrefab, chaserAiSpawn.position, chaserAiSpawn.rotation) as GameObject;

        // Get the Player Controller and Pawn components
        //Controller newController = aiObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        //Connect
        //newController.pawn = newPawn;
    }

    public void SpawnGuarderAi()
    {
        // Spawn the Player Controller at (0,0,0) with no rotation
        GameObject aiObj = Instantiate(aiControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        // Spawn the pawn and connect it to the controller
         GameObject newPawnObj = Instantiate(guarderAiPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation) as GameObject;

        // Get the Player Controller and Pawn components
        Controller newController = aiObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        //Connect
        newController.pawn = newPawn;
    }

    public void SpawnPatrollerAi()
    {
        // Spawn the Player Controller at (0,0,0) with no rotation
        GameObject aiObj = Instantiate(aiControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        // Spawn the pawn and connect it to the controller
         GameObject newPawnObj = Instantiate(patrollerAiPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation) as GameObject;

        // Get the Player Controller and Pawn components
        Controller newController = aiObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        //Connect
        newController.pawn = newPawn;
    }

    public void SpawnRunnerAi()
    {
        // Spawn the Player Controller at (0,0,0) with no rotation
        GameObject aiObj = Instantiate(aiControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        // Spawn the pawn and connect it to the controller
         GameObject newPawnObj = Instantiate(runnerAiPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation) as GameObject;

        // Get the Player Controller and Pawn components
        Controller newController = aiObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        //Connect
        newController.pawn = newPawn;
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

        //if there a spawn assigned spawn them
        if (chaserAiSpawn != null)
        {
            SpawnChaserAi();
        }
        if (guarderAiSpawn != null)
        {
            SpawnGuarderAi();
        }
        if (patrollerAiSpawn != null)
        {
            SpawnPatrollerAi();
        }
        if (runnerAiSpawn != null)
        {
            SpawnRunnerAi();
        }
    }

}
