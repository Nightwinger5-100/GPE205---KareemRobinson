using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ButtonPressToGame : MonoBehaviour
{
    public AudioSource menuMusic;
    public AudioSource ingameMusic;
    public void ChangeToGameplay ()
    {
        if (GameManager.instance != null) 
        {
            GameManager.instance.ActivateGameplay();
            menuMusic.Stop();
            ingameMusic.Play();
        }
    }
}
