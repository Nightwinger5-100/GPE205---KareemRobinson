using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class Level : MonoBehaviour
{
    //the rooms generated
    List<Room> allRooms = new List<Room>();

    //the key to print all current rooms
    public KeyCode printRoomsKey;

    //the key to clear the room list
    public KeyCode clearRoomListKey;

    //grabes the the rooms that were made
   public void storeRooms(Room generatedRooms)
   {
            allRooms.Add(generatedRooms);
   }
    
    public void Update()
    {
        ProcessInputs();
    }

    //retuns all the rooms that were made
   public void printRoomsList()
   {
    for (int room = 0; room < allRooms.Count; room++)
    {
        Debug.Log(allRooms[room].name);
    }
    
   }

    //clears the room list
   public void clearRoomList()
   {
    allRooms.Clear();
   }

    //check for the key input
   public void ProcessInputs()
   {
    if (Input.GetKey(printRoomsKey))
    {
        printRoomsList();
    }
    if (Input.GetKey(clearRoomListKey))
    {
        clearRoomList();
    }
   }
}
