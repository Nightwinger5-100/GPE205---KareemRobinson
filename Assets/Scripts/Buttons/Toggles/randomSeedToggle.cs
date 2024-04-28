using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSeedToggle : MonoBehaviour
{
    public void toggleRandomSeed()
    {
        FindObjectOfType<MapGenerator>().randomSeedGeneration = !FindObjectOfType<MapGenerator>().randomSeedGeneration;
    }
}
