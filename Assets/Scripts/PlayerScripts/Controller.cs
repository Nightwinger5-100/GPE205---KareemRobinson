using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Controller : MonoBehaviour
{

    //public TextMeshPro scoreUiText;

    //the object that will be the pawn
    public Pawn pawn;

    public float score = 0;
    

    public KeyCode uitest;
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        keyinputs();
    }

    //Input that'll be overridden by the child classes
    public virtual void ProcessInputs()
    {
        
    }

    public void addToScore(float scoreAmount)
    {
        score += scoreAmount;
        //scoreUiText.text = "Score: "+ score;
    }

    public void removeFromScore(float scoreAmount)
    {
        score -= scoreAmount;
        Debug.Log(score);
    }


    public void keyinputs()
    {
        if(Input.GetKey(uitest))
        {
            addToScore(299);
        }
    }
}
