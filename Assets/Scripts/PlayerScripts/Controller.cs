using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{

//the object that will be the pawn
public Pawn pawn;

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    //Input that'll be overridden by the child classes
    public virtual void ProcessInputs()
    {
        
    }

}
