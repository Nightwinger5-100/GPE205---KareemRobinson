using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class customGameTimeInput : MonoBehaviour
{
    public InputField inputField;

    public int newGameTime;
    public void newTimeInput(string input)
    {
        int.TryParse(input, out newGameTime);
        if(newGameTime > 999)
        {
            newGameTime = 999;
        }
        GameManager.instance.lengthOfGame = newGameTime;
    }
}
