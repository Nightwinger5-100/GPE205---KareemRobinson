using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Controller
{
    //movement and rotation keys
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode rotateClockwiseKey;

    public KeyCode rotateCounterClockwiseKey;

    //adds to score key
    public KeyCode uitest;

    //shoot projectile key
    public KeyCode shootKey;

    //sprint key
    public KeyCode sprintKey;

    //isSprintingBool
    public bool isSprintingBool;

    public int Lives = 0;

    public Canvas playerCanvas;

    public TextMeshPro scoreText;

    // Start is called before the first frame update
    public override void Start()
    {
        //if the gameManager exists
       if(GameManager.instance != null) 
       {
        //if the list "players" exists
        if(GameManager.instance.players != null) 
        {
            //add this player to the list
            GameManager.instance.players.Add(this);
            //updateCanvas();
        }
    }
    
    //run Start() from the parent class
    base.Start();
    }

    //remove the player from the gameManage list if they died
    public void Destroy()
    {
        //if the gameManager exists
       if(GameManager.instance != null) 
       {
        //if the list "players" exists
        if(GameManager.instance.players != null) 
        {
            //remove this player from the list
            GameManager.instance.players.Remove(this);
        }
       }
    }
    
    // Update is called once per frame so it will check if the key is down every frame
    public override void Update()
    {
        //check for inputs
        ProcessInputs();

        //get the parent Update()
        base.Update();
    }
    
    public void updateCanvas()
    {
        Canvas playerCanvas = FindObjectOfType<Canvas>();
        TextMeshPro playerCanvasText = playerCanvas.GetComponent<TextMeshPro>();
        playerCanvasText.text = "Score: " + score + "\n" + "Lives: " + Lives;
        playerCanvasText.text = playerCanvasText.text.Replace("\\n", "\n");
    }

    public void addToScore(float scoreAmount)
    {
        Canvas playerCanvas = FindObjectOfType<Canvas>();
        score += scoreAmount;
        TextMeshPro playerCanvasText = playerCanvas.GetComponent<TextMeshPro>();
        playerCanvasText.text = "Score: " + score + "\n" + "Lives: " + Lives;
        playerCanvasText.text = playerCanvasText.text.Replace("\\n", "\n");
    }

    public void removeFromScore(float scoreAmount)
    {
        score -= scoreAmount;
        TextMeshPro playerCanvasText = playerCanvas.GetComponent<TextMeshPro>();
        playerCanvasText.text = "Score: " + score + "\n" + "Lives: " + Lives;
        playerCanvasText.text = playerCanvasText.text.Replace("\\n", "\n");
    }

    //checks to see if the movement or rotation keys are down
    //if they're down then run the function for that action
    public override void ProcessInputs()
    {
        if (Input.GetKey(moveForwardKey))
        { 
            sprintCheck();
            pawn.MoveForward(isSprintingBool);
        }
        if (Input.GetKey(moveBackwardKey))
        { 
            sprintCheck();
            pawn.MoveBackward(isSprintingBool);
        }
        if (Input.GetKey(rotateClockwiseKey))
        { 
            sprintCheck();
            pawn.RotateClockwise(isSprintingBool);
        }
        if (Input.GetKey(rotateCounterClockwiseKey))
        { 
            sprintCheck();
            pawn.RotateCounterClockwise(isSprintingBool);
        }
        if (Input.GetKey(shootKey))
        {
            pawn.Shoot();
        }
        if(Input.GetKey(uitest))
        {
            addToScore(299);
        }
    }

    public void sprintCheck()
    {
        if (Input.GetKey(sprintKey))
        {
            isSprintingBool = true;
        }
        else
        {
            isSprintingBool = false;
        }
    }
}
