using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    //the speed the object will move at
    public float speed;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        //get the position of the object and adjust it based on the speed
        transform.position = transform.position + (Vector3.right * speed);
    }
}
