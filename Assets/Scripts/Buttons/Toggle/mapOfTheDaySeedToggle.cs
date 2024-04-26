using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class mapOfTheDaySeedToggle : MonoBehaviour
{
    public void toggleMOTDSeed()
    {
        FindObjectOfType<MapGenerator>().isMapOfTheDay = !FindObjectOfType<MapGenerator>().isMapOfTheDay;
    }
}
