using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtonPress : MonoBehaviour
{
    public AudioSource sfxAudioSource;
    public AudioClip buttonSound;

    public void buttonPressed()
    {
        sfxAudioSource.PlayOneShot(buttonSound);
    }
}
