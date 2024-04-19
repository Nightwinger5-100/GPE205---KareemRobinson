using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    
    //as a static so this is the one that can be accessed anywhere
    public static GameManager instance;

    // Player Controller Prefab
    public GameObject playerControllerPrefab;

    // Ai Controller prefabs
    public GameObject chaserAiControllerPrefab;

    public GameObject guarderAiControllerPrefab;

    public GameObject patrollerAiControllerPrefab;

    public GameObject runnerAiControllerPrefab;

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

    //ai list
    public List<AiController> ai;

    //max amount of powerups that can exist
    public int maxAmountOfPowerups;

    //current amount of power ups that exist
    public int currentAmountOfPowerups;

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

    public void SpawnChaserAi(GameObject spawnPoint)
    {
        // Spawn the Player Controller at (0,0,0) with no rotation
        GameObject aiObj = Instantiate(chaserAiControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        // Spawn the pawn and connect it to the controller
         GameObject newPawnObj = Instantiate(chaserAiPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;

        //Get the spawnPoint Component and get all the waypoints the ai can patrol to
        PawnSpawnPoint aiSpawn = spawnPoint.GetComponent<PawnSpawnPoint>();

        //for each waypoint on the spawner... 
        for(int waypoint = 0; waypoint < aiSpawn.waypoints.Length; waypoint++)
        {
            //add it to the ai controller
            aiObj.GetComponent<AiController>().waypoints.Add(aiSpawn.waypoints[waypoint].transform);
        }

        // Get the Player Controller and Pawn components
        Controller newController = aiObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        //Connect
        newController.pawn = newPawn;
    }

    public void SpawnGuarderAi(GameObject spawnPoint)
    {
        // Spawn the Player Controller at (0,0,0) with no rotation
        GameObject aiObj = Instantiate(guarderAiControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        // Spawn the pawn and connect it to the controller
         GameObject newPawnObj = Instantiate(guarderAiPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;

        //Get the spawnPoint Component and get all the waypoints the ai can patrol to
        PawnSpawnPoint aiSpawn = spawnPoint.GetComponent<PawnSpawnPoint>();

        //for each waypoint on the spawner... 
        for(int waypoint = 0; waypoint < aiSpawn.waypoints.Length; waypoint++)
        {
            //add it to the ai controller
            aiObj.GetComponent<AiController>().waypoints.Add(aiSpawn.waypoints[waypoint].transform);
        }

        // Get the Player Controller and Pawn components
        Controller newController = aiObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        //Connect
        newController.pawn = newPawn;
    }

    public void SpawnPatrollerAi(GameObject spawnPoint)
    {
        // Spawn the Player Controller at (0,0,0) with no rotation
        GameObject aiObj = Instantiate(patrollerAiControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        // Spawn the pawn and connect it to the controller
        GameObject newPawnObj = Instantiate(patrollerAiPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        
        //Get the spawnPoint Component and get all the waypoints the ai can patrol to
        PawnSpawnPoint aiSpawn = spawnPoint.GetComponent<PawnSpawnPoint>();

        //for each waypoint on the spawner... 
        for(int waypoint = 0; waypoint < aiSpawn.waypoints.Length; waypoint++)
        {
            //add it to the ai controller
            aiObj.GetComponent<AiController>().waypoints.Add(aiSpawn.waypoints[waypoint].transform);
        }

        // Get the Player Controller and Pawn components
        Controller newController = aiObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        //Connect
        newController.pawn = newPawn;
    }

    public void SpawnRunnerAi(GameObject spawnPoint)
    {
        // Spawn the Player Controller at (0,0,0) with no rotation
        GameObject aiObj = Instantiate(runnerAiControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        // Spawn the pawn and connect it to the controller
         GameObject newPawnObj = Instantiate(runnerAiPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        
        //Get the spawnPoint Component and get all the waypoints the ai can patrol to
        PawnSpawnPoint aiSpawn = spawnPoint.GetComponent<PawnSpawnPoint>();

        //for each waypoint on the spawner... 
        for(int waypoint = 0; waypoint < aiSpawn.waypoints.Length; waypoint++)
        {
            //add it to the ai controller
            aiObj.GetComponent<AiController>().waypoints.Add(aiSpawn.waypoints[waypoint].transform);
        }

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

    public void randomSpawn()
    {
        PawnSpawnPoint[] spawns = FindObjectsOfType<PawnSpawnPoint>();

        //for each spawn point spawn, pick a random ai type, then spawn it.
        for (int spawner = 0; spawner < spawns.Length-1; spawner++)
        {
            int pickedAi = Random.Range(0, 3);
            switch (pickedAi)
            {
                //spawn the chaser ai
                case 3:
                SpawnChaserAi(spawns[spawner].gameObject);
                break;

                //spawn the guarder ai
                case 2:
                SpawnGuarderAi(spawns[spawner].gameObject);
                break;

                //spawn the patroller ai
                case 1:
                SpawnPatrollerAi(spawns[spawner].gameObject);
                break;

                //spawn the runner ai
                case 0:

                SpawnRunnerAi(spawns[spawner].gameObject);
                break;
            }
                    
        }
        //spawn = UnityEngine.Random.Range(0, spawns.Length);
        
    }
    
    public void pickAiToSpawn(int caseNumber)
    {
        
    }

    // Start is called before the first frame update
    private void Start()
    {
        //Spawn the player
        SpawnPlayer();
    }

}
