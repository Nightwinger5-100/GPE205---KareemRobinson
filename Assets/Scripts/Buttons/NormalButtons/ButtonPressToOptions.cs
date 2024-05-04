using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressToOptions : MonoBehaviour
{
    public AudioSource sfxAudioSource;
    public AudioClip buttonSound;

    public void ChangeToOptions ()
    {
        sfxAudioSource.PlayOneShot(buttonSound);
        GameManager.instance.ActivateOptions();
    }
}
