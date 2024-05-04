using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ButtonPressToGame : MonoBehaviour
{
    public AudioSource sfxAudioSource;
    public AudioClip buttonSound;
    public AudioSource menuMusic;
    public AudioSource ingameMusic;
    
    public void ChangeToGameplay ()
    {
        sfxAudioSource.PlayOneShot(buttonSound);
        GameManager.instance.ActivateGameplay();
        menuMusic.Stop();
        ingameMusic.Play();
    }
}
