using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    public string exampleText = "Hello World";

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(exampleText);

        exampleText = "Hello again!";

        Debug.Log(exampleText);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
