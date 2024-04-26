using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressToGame : MonoBehaviour
{
    public void ChangeToGameplay ()
    {
        if (GameManager.instance != null) {
            GameManager.instance.ActivateGameplay();
        }
    }
}
