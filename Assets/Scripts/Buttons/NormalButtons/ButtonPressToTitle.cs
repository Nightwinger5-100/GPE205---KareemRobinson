using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressToTitle : MonoBehaviour
{
    public AudioSource musicPlayer;

    public void ChangeToTitleScreen ()
    {
        if (GameManager.instance != null) 
        {
            GameManager.instance.ActivateTitleScreen();
            musicPlayer.Play();
        }
    }
}
