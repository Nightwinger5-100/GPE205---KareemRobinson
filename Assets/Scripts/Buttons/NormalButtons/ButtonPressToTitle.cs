using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressToTitle : MonoBehaviour
{
    public AudioSource sfxAudioSource;
    public AudioClip buttonSound;
    public AudioSource musicPlayer;

    public void ChangeToTitleScreen ()
    {
        sfxAudioSource.PlayOneShot(buttonSound);
        GameManager.instance.ActivateTitleScreen();
        musicPlayer.Play();
    }
}
