using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleExample : MonoBehaviour
{
     public string theText = "Hello World";
    // Start is called before the first frame update
    private void Start()
    {
       
        Debug.Log(theText);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
