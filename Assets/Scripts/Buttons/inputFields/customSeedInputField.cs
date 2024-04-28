using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;


public class customSeedInputField : MonoBehaviour
{
    public InputField inputField;

    public int newSeed;
    public void newSeedInput(string input)
    {
        int.TryParse(input, out newSeed);
        if(newSeed > 9999)
        {
            newSeed = 9999;
        }
        FindObjectOfType<MapGenerator>().mapSeed = newSeed;
    }
}
