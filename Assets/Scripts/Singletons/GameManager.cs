using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public bool muliplayer;
    
    //as a static so this is the one that can be accessed anywhere
    public static GameManager instance;

    // Player Controller Prefabs
    public GameObject playerOneControllerPrefab;

    public GameObject playerTwoControllerPrefab;

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
    public GameObject winScreenStateObject;

    //player list
    public List<PlayerController> players;

    //ai list
    public List<AiController> ai;

    //pickup list
    public List<Spawner> storedPickUpSpawns;

    public List<PawnSpawnPoint> storedPawnSpawns = new List<PawnSpawnPoint>();

    public List<PlayerSpawnPoint> storedPlayerSpawns = new List<PlayerSpawnPoint>();

    public List<Pawn> storedPawns = new List<Pawn>();

    private int playerSpawn = 0;

    //music/sounds
    public AudioSource bgMusic;
    public AudioSource sfxAudioSource;

    public AudioClip gameOverSFX;


    //hotkeys for debugging
    public KeyCode titleKey;

    //The time the game will go on for
    public float lengthOfGame = 30;

    //The time when the game will end
    private float endTime;

    private bool timerActive;

    // Start is called before the first frame update
    private void Start()
    {
        //Spawn the player
        //SpawnPlayer();
        ActivateTitleScreen();
    }

    private void Update()
    {
        timer();
        getInputs();
    }

    //Spawns the player at the spawn with their controller
    public void SpawnPlayerOne(GameObject spawnPoint)
    {
        
        // Spawn the Player Controller at (0,0,0) with no rotation
        GameObject newPlayerObj = Instantiate(playerOneControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        // Spawn the pawn and connect it to the controller
        GameObject newPawnObj = Instantiate(tankPawnPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;

        // Get the Player Controller and Pawn components
        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();
        newPawn.pawnController = newController;

        //set the lives on the controller
        PlayerController newPlayerController = newPlayerObj.GetComponent<PlayerController>();
        newPlayerController.Lives = defaultNumberOfLives;

        //Connect the pawn to the controller
        newController.pawn = newPawn;

        storedPawns.Add(newPawn);
         //update their ui
        newPlayerController.updateCanvas();

    }
    
        //Spawns the player at the spawn with their controller
    public void SpawnPlayerTwo(GameObject spawnPoint)
    {
        
        // Spawn the Player Controller at (0,0,0) with no rotation
        GameObject newPlayerObj = Instantiate(playerTwoControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        // Spawn the pawn and connect it to the controller
         GameObject newPawnObj = Instantiate(tankPawnPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;

        // Get the Player Controller and Pawn components
        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();
        newPawn.pawnController = newController;

        //set the lives on the controller
        PlayerController newPlayerController = newPlayerObj.GetComponent<PlayerController>();
        newPlayerController.Lives = defaultNumberOfLives;

        //Connect the pawn to the controller
        newController.pawn = newPawn;
        storedPawns.Add(newPawn);
         //update their ui
        newPlayerController.updateCanvas();

    }

    //Respawns the player if they have lives
    public void RespawnPlayer(PlayerController playerController)
    {
        //check 
        if (!checkIfGameOver(playerController))
        {
            //get a random Spawnpoint
            GameObject spawnPoint = randomPlayerSpawn();

            // Spawn the pawn and connect it to the controller
            GameObject newPawnObj = Instantiate(tankPawnPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;

            // Get the Player Controller and Pawn components
            Controller newController = playerController.GetComponent<Controller>();
            Pawn newPawn = newPawnObj.GetComponent<Pawn>();
            newPawn.pawnController = newController;

            //Connect
            newController.pawn = newPawn;
            //update ui
            playerController.updateCanvas();
            multiPlayerMode();

        }
        else
        {
            Debug.Log(playerController + " has ran out of lives");
        }
    }

    //stores the pickUps in a list to reference later
    public void storePickUps()
    {
        Spawner[] spawns = FindObjectsOfType<Spawner>();

        for(int currentPickUpSpawner = 0;  currentPickUpSpawner < spawns.Length; currentPickUpSpawner++)
        {
            storedPickUpSpawns.Add(spawns[currentPickUpSpawner]);
        }
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
        storedPawns.Add(newPawn);
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
        storedPawns.Add(newPawn);
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
        storedPawns.Add(newPawn);
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
        storedPawns.Add(newPawn);
    }
    
    //Updates lives and respawns the player is applicable
    public bool checkIfGameOver(PlayerController playerController)
    {
        if (!timerActive)
        {
            for (int playerlistNum = 0; playerlistNum < players.Count; playerlistNum++)
            {
            //remove that player from the list of players
            Destroy(playerController.gameObject);
            players.Remove(players[playerlistNum]);
            ActivateGameOver();
            }
            return true;
        }
        //if this player still has lives then the game continues
        else if (playerController.Lives > 0)
        {
            //just remove a life
            playerController.Lives -= 1;
            return false;
        }

        //if that player has ran out of lives...
        else
        {
            //find the player in the playerlist
            for (int playerlistNum = 0; playerlistNum < players.Count; playerlistNum++)
            {
                if (playerController == players[playerlistNum])
                {
                    //remove that player from the list of players
                    Destroy(playerController.gameObject);
                    players.Remove(players[playerlistNum]);
                    
                }
            }

            //if the player list is now empty...
            if (players.Count < 1)
            {
                Debug.Log("gg");
                //the game is over
                ActivateGameOver();
            }
            //it's gameover for that player
            return true;
        }
        
    }

    public void timer()
    {
        if (GameplayStateObject.activeSelf && timerActive)
        {
        Debug.Log(endTime - Mathf.Round(Time.time));
        }
        if(GameplayStateObject.activeSelf && endTime < Time.time && timerActive)
        {
            timerActive = false;
            checkIfGameOver(players[0]);
        }
        
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
    
    public void createPlayer()
    {
        if (!muliplayer)
        {
        //spawn one player
        SpawnPlayerOne(randomPlayerSpawn());
        }
        else
        {
        //spawn two players
        SpawnPlayerOne(randomPlayerSpawn());
        SpawnPlayerTwo(randomPlayerSpawn());
        }
        
    }

    public GameObject randomPlayerSpawn()
    {

        //Get the PlayerSpawnPoint Component and get all the spawn points the player can choose from
        PlayerSpawnPoint[] playerSpawns = FindObjectsOfType<PlayerSpawnPoint>();
        
        //store each player spawn
        for (int playerSpawnToAdd = 0; playerSpawnToAdd <  playerSpawns.Length; playerSpawnToAdd++)
        {
            storedPlayerSpawns.Add(playerSpawns[playerSpawnToAdd]);
        }

        //Pick a spawn point for the player
        playerSpawn = Random.Range(0, playerSpawns.Length);

        
        return playerSpawns[playerSpawn].gameObject;
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
        endTime = Mathf.Round(Time.time) + lengthOfGame;
        timerActive = true;
        FindObjectOfType<MapGenerator>().createMapWithPlayerFromSeed();
    }

    public void ActivateGameOver()
    {
        // Deactivate all states
        DeactivateAllStates();
        
        // Activate the gameover screen
        GameOverScreenStateObject.SetActive(true);
        
        //play gameover sound and stop music
        sfxAudioSource.PlayOneShot(gameOverSFX);
        bgMusic.Stop();

        //print all the scores
        for (int plr = players.Count-1 ; plr >= 0; plr--)
        {
            Debug.Log(players[plr].score);
        }

        //Delete all the gameObjects
        clearTheGame();

    }

    public void ActivateWin()
    {
        // Deactivate all states
        DeactivateAllStates();
        
        // Activate the gameover screen
        winScreenStateObject.SetActive(true);
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

    private void clearTheGame()
    {
        removeAllAi();
        removeAllPickups();
        removeAllRooms();
        removeAllPlayerControllers();
        removeAllTankPawns();
    }
    
    private void removeAllTankPawns()
    {
        for (int curTankPawn = 0; curTankPawn < storedPawns.Count; curTankPawn++)
        {
            Destroy(storedPawns[curTankPawn].gameObject);
        }
        storedPawns.Clear();
    }

    private void removeAllAi()
    {
        //destroy each ai pawn and controller
        for (int aiNum = ai.Count-1;  aiNum >= 0; aiNum-- )
        {
            if (ai[aiNum].pawn.gameObject != null)
            {
              //  Destroy(ai[aiNum].pawn.gameObject);
            }
            
            Destroy(ai[aiNum].gameObject);
        }
        //destroy each ai spawnpoint
        for (int pickUpNum = storedPawnSpawns.Count-1;  pickUpNum >= 0; pickUpNum-- )
        {
            Destroy(storedPawnSpawns[pickUpNum].gameObject);
            Destroy(storedPawnSpawns[pickUpNum]);
        }
        //clear the ai list and spawns list
        storedPawnSpawns.Clear();
        ai.Clear();
    }
    
    private void removeAllPickups()
    {
        //get a list of all the pickups
        pickUp[] pickuplist = FindObjectsOfType<pickUp>();
        //destroy each pickup
        for (int pickUpNum = pickuplist.Length-1;  pickUpNum >= 0; pickUpNum-- )
        {
            Destroy(pickuplist[pickUpNum].gameObject);
        }
        //clear the pick up list
        storedPickUpSpawns.Clear();
    }

    private void removeAllRooms()
    {
        List<Room> theRooms = FindObjectOfType<Level>().allRooms;
        for (int roomNum = theRooms.Count-1; roomNum >= 0;  roomNum--)
        {
            Destroy(theRooms[roomNum].gameObject);
        }
        FindObjectOfType<Level>().clearRoomList();
    }

    private void removeAllPlayerControllers()
    {   
        //Destroy every player controller and clear the list
        for (int plrControllerNum = players.Count-1 ; plrControllerNum >= 0; plrControllerNum--)
        {
            Destroy(players[plrControllerNum].gameObject);
        }
        players.Clear();
    }

    public void multiPlayerMode()
    {
        
        if (muliplayer)
        {
            Camera[] camList = FindObjectsOfType<Camera>();
            Debug.Log(camList.Length);
            for (int camNum = 0;  camNum < camList.Length; camNum++)
            {
                for(int playerNum = 0;  playerNum < players.Count; playerNum++)
                {
                    Camera thisPlayersCam = players[playerNum].pawn.transform.GetComponentInChildren<Camera>();
                    if (camList[camNum] == thisPlayersCam && playerNum == 0)
                    {
                        camList[camNum].rect = new Rect(0.0f, 0.5f, 1f, 1f);
                    }
                    else if (camList[camNum] == thisPlayersCam && playerNum == 1)
                    {
                        camList[camNum].rect = new Rect(0.0f, 0.0f, 1f, 0.5f);
                    }
                }
            }
        }
    }

    public void getInputs()
    {
        if (Input.GetKey(titleKey))
        {
            multiPlayerMode();
        }
    }
}
