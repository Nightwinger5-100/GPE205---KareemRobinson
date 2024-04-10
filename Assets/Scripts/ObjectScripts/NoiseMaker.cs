using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour
{
    //the distance of the noise this object is making
    public float volumeDistance;
    
    //however far this object can hear
    public float hearingDistance;

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
        if (noiseMaker.volumeDistance <= 0)
        {
            return false;
        }
        //add the volumeDistance in the noisemaker to the hearingDistance of this object
        float totalDistance  = noiseMaker.volumeDistance + hearingDistance;

        //if the distance being made is greater than the distance between the two objects return true
        if (Vector3.Distance(transform.position, target.transform.position) <= totalDistance)
        {
            return true;
        //otherwise return false
        } else {
            return false;    
        }    
    }
}
