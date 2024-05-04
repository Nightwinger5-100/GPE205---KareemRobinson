using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressToCredits : MonoBehaviour
{
    public AudioSource sfxAudioSource;
    public AudioClip buttonSound;

    public void ChangeToCredits ()
    {
        sfxAudioSource.PlayOneShot(buttonSound);
        GameManager.instance.ActivateCredits();
    }
}
