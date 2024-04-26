using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressToQuit : MonoBehaviour
{
    public void quitGame()
    {
        Debug.Log("This would close the game, but you're in the editor.");
        Application.Quit();
    }
}
