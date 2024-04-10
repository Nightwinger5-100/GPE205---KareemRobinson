using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Vision : MonoBehaviour
{
    //the size of how far the ai can see(field of vision)
    float fov;

    //check if the target is within the ai's field of vision
    public bool CanSee(GameObject target)
    {
        //if this isn't working change the unity engine part to System.Numerics
        //get the distance between the target and the ai 
        UnityEngine.Vector3 agentToTargetVector = target.transform.position - transform.position;
       
        //if this isn't working change the unity engine part to System.Numerics
        //the angle of the distance between the ai and target
        float angleToTarget = UnityEngine.Vector3.Angle(agentToTargetVector, transform.forward);

        //if the angle is less than the field of vision...
        if (angleToTarget < fov)
        {
            return true;
        }
        //if the field of vision is greater...
        else
        {
        return false; 
        }  
    }
}
