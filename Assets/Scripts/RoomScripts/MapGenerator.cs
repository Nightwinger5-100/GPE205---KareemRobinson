using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //the input to (re)generate the map
    public KeyCode makeMapKey;

    //each room generated
    public GameObject[] gridPrefabs;

    //the amount of room rows/columns that'll be generated 
    public int rows;
    public int cols;

    //the size of the rooms
    public float roomWidth = 50.0f;
    public float roomHeight = 50.0f;

    //the map that''ll be generated based on the seed
    public int mapSeed;
    
    //the map that'll be generated based on time
    public bool isTimeBasedSeedGeneration;

    //the map that'll be generated based on the year,month,day
    public bool isMapOfTheDay;

    //check to see if at least one player room has spawned
    private bool ifThereIsOnePlayerSpawn;
    
    //the col and rows
    private Room[,] grid;

    // Start is called before the first frame update
    public void Start()
    {
        pickMapSeedGen();
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }
    
    //decides what the map seed will be and how it'lll be picked
    public void pickMapSeedGen()
    {
        if (isMapOfTheDay)
        {
            mapSeed = DateToInt (DateTime.Now.Date);           
        }
        else if (isTimeBasedSeedGeneration)
        {
            UnityEngine.Random.InitState(DateToInt(DateTime.Now));
        }
        else if (mapSeed > 0)
        {
            
            UnityEngine.Random.InitState(mapSeed);
        }
        Debug.Log("The map seed is " + mapSeed);
    }

    public int DateToInt ( DateTime dateToUse ) 
    {
     // Add our date up and return it
     return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }

     // Returns a random room
    public GameObject RandomRoomPrefab () 
    {
     return gridPrefabs[UnityEngine.Random.Range(0, gridPrefabs.Length)];
    }

    public void GenerateMap() 
     {
        
     // Clear out the grid - "column" is our X, "row" is our Y
     grid = new Room[cols, rows];

     //for each column in each row...
     for (int currentRow = 0; currentRow < rows; currentRow++) 
     {
        for (int currentCol = 0; currentCol < cols ; currentCol++) 
        {
            //set the position
           float xPosition = roomWidth * currentCol;
           float zPosition = roomHeight * currentRow;
           Vector3 newPosition =  new Vector3 (xPosition, transform.position.y, zPosition);
            //if the player has a place to spawn...
            if (ifThereIsOnePlayerSpawn)
            {
                //create the room object                 
                GameObject tempRoomObj = Instantiate (RandomRoomPrefab(),  newPosition, Quaternion.identity) as GameObject;
                //parent that room object and name it
                tempRoomObj.transform.parent = this.transform;
                tempRoomObj.name = "Room_"+currentCol+","+currentRow;
            
                //give the room the room component
                Room tempRoom = tempRoomObj.GetComponent<Room>();
                //add that room reference to the Room array
                grid[currentCol,currentRow] = tempRoom;

                //check to see what doors need to be opened on the columns and rows
                openColumnDoor(tempRoom, currentCol);
                openRowDoor( tempRoom,  currentRow);
                //stores the room in a list to reference at any time
                FindObjectOfType<Level>().storeRooms(tempRoom);
            }
            //otherwise, make the room a player spawn
            else
            {
                //create the room object                 
                GameObject tempRoomObj = Instantiate (gridPrefabs[0],  newPosition, Quaternion.identity) as GameObject;
                //parent that room object and name it
                tempRoomObj.transform.parent = this.transform;
                tempRoomObj.name = "Room_"+currentCol+","+currentRow;
            
                //give the room the room component
                Room tempRoom = tempRoomObj.GetComponent<Room>();
                //add that room reference to the Room array
                grid[currentCol,currentRow] = tempRoom;

                //check to see what doors need to be opened on the columns and rows
                openColumnDoor(tempRoom, currentCol);
                openRowDoor( tempRoom,  currentRow);
                //stores the room in a list to reference at any time
                FindObjectOfType<Level>().storeRooms(tempRoom);
                ifThereIsOnePlayerSpawn = true;
            }
        } 
     }
    
    //spawns the ai and spawns the player
    FindObjectOfType<GameManager>().randomAiSpawn();
    FindObjectOfType<GameManager>().randomPlayerSpawn();
    }

    public void openColumnDoor(Room tempRoom, int currentCol)
    {
     // Open the doors
     // If we are on the leftmost columnn, open the west door
     if (currentCol == 0) 
     {
          tempRoom.doorEast.SetActive(false);
     } 
     else if ( currentCol == cols-1 )
     {
          // Otherwise, if we are on the rightmost column, open the east door
          Destroy(tempRoom.doorWest);
     }
     else {
          // Otherwise, we are in the middle, so open both doors
          Destroy(tempRoom.doorWest);
          Destroy(tempRoom.doorEast);
     }          
    }

    private void openRowDoor(Room tempRoom, int currentRow)
    {
        // Open the doors
        // If we are on the bottom row, open the north door
        if (currentRow == 0) 
        {
            tempRoom.doorNorth.SetActive(false);
        } 
        else if ( currentRow == rows-1 )
        {
            // Otherwise, if we are on the top row, open the south door
            Destroy(tempRoom.doorSouth);
        }
        else 
        {
            // Otherwise, we are in the middle, so open both doors
            Destroy(tempRoom.doorNorth);
            Destroy(tempRoom.doorSouth);
        }          
    }

    //check for the key input
   public void ProcessInputs()
   {
    if (Input.GetKey(makeMapKey))
    {
        pickMapSeedGen();
        GenerateMap();
    }
   }
}
