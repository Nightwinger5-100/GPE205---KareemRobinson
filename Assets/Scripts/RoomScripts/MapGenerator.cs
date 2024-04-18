using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] gridPrefabs;
    public int rows;
    public int cols;
    public float roomWidth = 50.0f;
    public float roomHeight = 50.0f;
    private Room[,] grid;

    // Start is called before the first frame update
    public void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     // Returns a random room
    public GameObject RandomRoomPrefab () 
    {
     return gridPrefabs[Random.Range(0, gridPrefabs.Length)];
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
           
        }
     }
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

}
