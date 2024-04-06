using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public float timerDelay = 1.0f;
    private float nextTimeEvent;

    // Start is called before the first frame update
    void Start()
    {
        nextTimeEvent = Time.time + timerDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTimeEvent) 
        {
            Debug.Log("fevwfdg");
            nextTimeEvent = Time.time + timerDelay;
        }
    }
}
