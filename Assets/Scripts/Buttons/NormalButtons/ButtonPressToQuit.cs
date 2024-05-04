using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressToQuit : MonoBehaviour
{
    public AudioSource sfxAudioSource;
    public AudioClip buttonSound;

    public void quitGame()
    {
        sfxAudioSource.PlayOneShot(buttonSound);
        Debug.Log("This would close the game, but you're in the editor.");
        Application.Quit();
    }
}
