using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{

    public float timerDelay = 1.0f;
    private float nextTimeEvent;

    // Start is called before the first frame update
    void Start()
    {
        nextTimeEvent = timerDelay;
    }

    // Update is called once per frame
    void Update()
    {
        //every frame we're going to decrease nextTimeEvent until it's 0
        nextTimeEvent -= Time.deltaTime;
        if (0 >= nextTimeEvent) 
        {
            Debug.Log("yooooo");
            nextTimeEvent = timerDelay;
        }
    }
}
