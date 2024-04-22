using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    //The possible states the game can enter
    public enum GameStates {Title, MainMenu, Ingame, Gameover, Credits, Options};


    public GameStates currentState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //this is the state system for the Game to switch between
    public void setState()
    {
        switch(currentState)
        {
            case GameStates.Title:
            break;
            case GameStates.MainMenu:
            break;
            case GameStates.Ingame:
            break;
            case GameStates.Gameover:
            break;
            case GameStates.Credits:
            break;
            case GameStates.Options:
            break;
        }
    }



}
