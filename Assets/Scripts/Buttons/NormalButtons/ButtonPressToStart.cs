using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressToStart : MonoBehaviour
{
    public AudioSource sfxAudioSource;
    public AudioClip buttonSound;
    public void ChangeToMainMenu ()
    {
        if (GameManager.instance != null) {
            sfxAudioSource.PlayOneShot(buttonSound);
            GameManager.instance.ActivateMainMenu();
        }
    }

}
