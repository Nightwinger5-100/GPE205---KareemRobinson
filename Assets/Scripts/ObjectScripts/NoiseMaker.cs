using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour
{
    //the distance of the noise this object is making
    private float currentVolumeDistance;
    
    //the distance of noise this object WILL make
    public float volumeDistance;

    //however far this object can hear
    public float hearingDistance;
    
    //The delay before the sound start
    public float startTimerDelay = 1.0f;

    //The delay before the sound ends
    public float endTimerDelay = 1.0f;

    //Make the sound eventually start
    public bool startDelay;

    //Make the sound eventually stop
    public bool endDelay;
    
    //the number being used to compare the time before ending the sound
    private float startTimeEvent;

    //the number being used to compare the time before ending the sound
    private float endTimeEvent;

    //if the startTimeEvent began
    private bool beganStartTimeEvent;

    //if the endTimeEvent began
    private bool beganEndTimeEvent;

    //check the sound of the object
    public bool CanHear(GameObject target)
    {
        //Get the target's NoiseMaker
        NoiseMaker noiseMaker = target.GetComponent<NoiseMaker>();
        
        //if the target doesn't have a noiseMaker comp return false
        if (noiseMaker == null)
        {
            return false;
        }
        //if the target isn't making any noise then return false
        if (noiseMaker.currentVolumeDistance <= 0)
        {
            return false;
        }
        //add the volumeDistance in the noisemaker to the hearingDistance of this object
        float totalDistance  = noiseMaker.currentVolumeDistance + hearingDistance;

        //if the distance being made is greater than the distance between the two objects return true
        if (Vector3.Distance(transform.position, target.transform.position) <= totalDistance)
        {
            return true;
        //otherwise return false
        } else {
            return false;    
        }    
    }

    //make sound and sets the timeEvent var
    public void makeSound()
    {
        //play the sound if it's not being played
        if (currentVolumeDistance != volumeDistance)
        {
        currentVolumeDistance = volumeDistance;
        print("playing sound at distance: "+ currentVolumeDistance);
        }
        //update the timeEvent numbers
        if (startDelay)
        {
            updateTimeEvent();
        }
        else if (endDelay)
        {
            updateTimeEvent();
        }
        
    }

    //stops the sound
    public void stopSound()
    {
        //set the volume to 0
        currentVolumeDistance = 0;
        startTimeEvent = 0;
        endTimeEvent = 0;
        print("no more sound");
    }

    //starts a timer for update()
    public void updateTimeEvent()
    {
        //set the nextTimeEvent
        startTimeEvent = Time.time + startTimerDelay;
        endTimeEvent = Time.time + endTimerDelay;
    }
    
    public void Start()
    {
        makeSound();
    }

    //mainly checking if it needs to start a sound or stop a sound
    public void Update()
    {
        //if the sound is gonna start eventually on its own...
        if (startDelay)
        {
            //if they're currently not making the noise...
            if (currentVolumeDistance != volumeDistance)
            {
                //if the timer started...
                if (beganStartTimeEvent)
                {
                    //when the current time is greater than the nextTimeEvent...
                    if (Time.time >= startTimeEvent)
                    {
                        Debug.Log("Start");
                        makeSound();
                        beganStartTimeEvent = false;
                        updateTimeEvent();
                    }
                }
                //if the timer hasn't started...
                else
                {
                    //start the timer
                    Debug.Log("Start the starter");
                    updateTimeEvent();
                    beganStartTimeEvent = true;
                }
            }
        }
        //if the sound is gonna stop eventually on its own...
        if (endDelay)
        {
            //if they're currently making noise...
            if (currentVolumeDistance > 0)
            {   
                if (!beganEndTimeEvent)
                {
                    Debug.Log("Starting to end the sound");
                    beganEndTimeEvent = true;
                }
                //when the current time is greater than the nextTimeEvent...
                if (Time.time >= endTimeEvent)
                {
                    print("Stopping sound");
                    stopSound();
                    beganEndTimeEvent = false;
                }
            }    
        }
        //if the sound is never gonna start or end on it's own...
        else
        {
            if (!startDelay)
            {
                if (volumeDistance > 0)
                {
                    //the current volume distance will always be the volume distance
                    makeSound();
                }
                //if there's no volume distance anymore then set the currentvol to that number 
                else
                {
                    if (currentVolumeDistance != volumeDistance)
                    {
                        currentVolumeDistance = volumeDistance;
                        Debug.Log("Not playign sound anymore!");
                    }
                }
            }
        }     
    }
}
