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

    //max amount of powerups that can exist
    public int maxAmountOfPowerups;

    //current amount of power ups that exist
    public int currentAmountOfPowerups;

    public int defaultNumberOfLives = 3;

    //the game states
    public GameObject TitleScreenStateObject;
    public GameObject MainMenuStateObject;
    public GameObject OptionsScreenStateObject;
    public GameObject CreditsScreenStateObject;
    public GameObject GameplayStateObject;
    public GameObject GameOverScreenStateObject;

    //player list
    public List<PlayerController> players;

    //ai list
    public List<AiController> ai;

    List<PawnSpawnPoint> storedPawnSpawns = new List<PawnSpawnPoint>();

    List<PlayerSpawnPoint> storedPlayerSpawns = new List<PlayerSpawnPoint>();

    public KeyCode titleKey;

    public KeyCode mainMenuKey;

    public KeyCode optionsKey;

    public KeyCode creditsKey;

    public KeyCode gameplayKey;

    public KeyCode gameoverKey;


    // Start is called before the first frame update
    private void Start()
    {
        //Spawn the player
        //SpawnPlayer();
        ActivateTitleScreen();
    }

    private void Update()
    {
        ProcessInputs();
    }

    //Spawns the player at the spawn with their controller
    public void SpawnPlayer(GameObject spawnPoint)
    {
        
        // Spawn the Player Controller at (0,0,0) with no rotation
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        // Spawn the pawn and connect it to the controller
         GameObject newPawnObj = Instantiate(tankPawnPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;

        // Get the Player Controller and Pawn components
        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newPawn.pawnController = newController;
        newPawn.Lives = defaultNumberOfLives;

        //Connect
        newController.pawn = newPawn;
    }
    
    //Spawns the various ai at the spawn with their controller
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
        newPawn.pawnController = newController;

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
        newPawn.pawnController = newController;

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
        newPawn.pawnController = newController;

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
        newPawn.pawnController = newController;

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

    public void randomAiSpawn()
    {
        PawnSpawnPoint[] pawnSpawns = FindObjectsOfType<PawnSpawnPoint>();
        //for each spawn point spawn, pick a random ai type, then spawn it.
        for (int spawner = 0; spawner < pawnSpawns.Length-1; spawner++)
        {
            int pickedAi = Random.Range(0, 4);
            switch (pickedAi)
            {
                //spawn the chaser ai
                case 3:
                SpawnChaserAi(pawnSpawns[spawner].gameObject);
                break;

                //spawn the guarder ai
                case 2:
                SpawnGuarderAi(pawnSpawns[spawner].gameObject);
                break;

                //spawn the patroller ai
                case 1:
                SpawnPatrollerAi(pawnSpawns[spawner].gameObject);
                break;

                //spawn the runner ai
                case 0:

                SpawnRunnerAi(pawnSpawns[spawner].gameObject);
                break;
            }     
        
        //add the spawns to a list that can be referenced later
        storedPawnSpawns.Add(pawnSpawns[spawner]);          
        }
        //spawn = UnityEngine.Random.Range(0, spawns.Length);
        
    }
    
    public void randomPlayerSpawn()
    {
        //Get the PlayerSpawnPoint Component and get all the spawn points the player can choose from
        PlayerSpawnPoint[] playerSpawns = FindObjectsOfType<PlayerSpawnPoint>();
        
        //store each player spawn
        for (int playerSpawnToAdd = 0; playerSpawnToAdd <  playerSpawns.Length; playerSpawnToAdd++)
        {
            storedPlayerSpawns.Add(playerSpawns[playerSpawnToAdd]);
        }

        //Pick a spawn point for the player
        int playerSpawn = Random.Range(0, playerSpawns.Length);

        
        SpawnPlayer(playerSpawns[playerSpawn].gameObject);
    }


    //The functions for deactiving all the screens and enabling the screen of the function
    public void ActivateTitleScreen()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate the title screen
        TitleScreenStateObject.SetActive(true);
    }

    public void ActivateMainMenu()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate the main menu screen
        MainMenuStateObject.SetActive(true);
    }

    public void ActivateOptions()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate the options screen
        OptionsScreenStateObject.SetActive(true);
    }

    public void ActivateCredits()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate the credits screen
        CreditsScreenStateObject.SetActive(true);
    }

    public void ActivateGameplay()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate the gameplay screen
        GameplayStateObject.SetActive(true);
        FindObjectOfType<MapGenerator>().createMapWithPlayerFromSeed();
    }

    public void ActivateGameOver()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate the gameover screen
        GameOverScreenStateObject.SetActive(true);
    }

    //Disables all the screeens
    private void DeactivateAllStates()
    {
        // Deactivate all Game States
        TitleScreenStateObject.SetActive(false);
        MainMenuStateObject.SetActive(false);
        OptionsScreenStateObject.SetActive(false);
        CreditsScreenStateObject.SetActive(false);
        GameplayStateObject.SetActive(false);
        GameOverScreenStateObject.SetActive(false);
    }


    public void ProcessInputs()
   {
    if (Input.GetKey(titleKey) && !TitleScreenStateObject.activeSelf)
    {
        ActivateTitleScreen();
    }
    if (Input.GetKey(mainMenuKey) & !MainMenuStateObject.activeSelf)
    {
        ActivateMainMenu();
    }
        if (Input.GetKey(optionsKey) && !OptionsScreenStateObject.activeSelf)
    {
        ActivateOptions();
    }
    if (Input.GetKey(creditsKey) & !CreditsScreenStateObject.activeSelf)
    {
        ActivateCredits();
    }
        if (Input.GetKey(gameplayKey) && !GameplayStateObject.activeSelf)
    {
        ActivateGameplay();
    }
    if (Input.GetKey(gameoverKey) & !GameOverScreenStateObject.activeSelf)
    {
        ActivateGameOver();
    }
   }
}
