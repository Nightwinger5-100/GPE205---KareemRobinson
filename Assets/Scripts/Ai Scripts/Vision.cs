using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class Vision : MonoBehaviour
{
    //the size of how far the ai can see(field of vision)
    public float fov;
    public float maxDistance;
    [SerializeField] LineRenderer lineRend;
    public GameObject thetarget;
    public void Update()
    {
        CanSee(thetarget);
    }

    //testing raycast
    public void RaycastTest(Transform startOfRay, Vector3 directionOfRay, GameObject target)
    {
        //startOfRay should just be the component owner, directionOfRay is the direction the ray will travel
        //the final parameter is the distance it'll travel
        
        RaycastHit hit;
        //Physics.Raycast(startOfRay.position, directionOfRay, out hit, Mathf.Infinity);

        //enable the line render and have travel to the directionOfRay
        lineRend.enabled = true;
        lineRend.SetPosition(0, startOfRay.position);
        lineRend.SetPosition(1, directionOfRay);
        Vector3 agentToTargetVector = target.transform.position - transform.position;

        //if there's something hit...otherwise...
        if (Physics.Raycast(startOfRay.position, agentToTargetVector.normalized, out hit, maxDistance))
        {
            
            if (hit.collider == target.GetComponent<SphereCollider>())
            {
                Debug.Log("Spotted the target: " + target);
            }
            else
            {
                Debug.Log("Target blocked by: " + hit.collider);
            }
        }
        else
        {
            Debug.Log("i see nothing");
        }
    }

    //check if the target is within the ai's field of vision
    public bool CanSee(GameObject target)
    {
        //if this isn't working change the unity engine part to System.Numerics
        //get the distance between the target and the ai 
        Vector3 agentToTargetVector = target.transform.position - transform.position;

        

        //if this isn't working change the unity engine part to System.Numerics
        //the angle of the distance between the ai and target
        float angleToTarget = Vector3.Angle(agentToTargetVector, transform.forward);

        //if the angle is less than the field of vision...
        if (angleToTarget < fov)
        {
            RaycastTest(transform, target.transform.position, target);
            return true;
        }
        //if the field of vision is greater...
        else
        {
            print("not in fov");
        return false; 
        }  
    }
}
