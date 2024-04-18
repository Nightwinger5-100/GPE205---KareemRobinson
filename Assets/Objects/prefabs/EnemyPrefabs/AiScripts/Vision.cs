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
    
    //toggle for the visible ray
    public bool lineRendBool;

    //if the console will log if the target is in fov
    public bool isInFOV;

    //if the console will log rayresults
    public bool printRayResult;

    //if the console will log rayresults even if they aren't the correct object
    public bool printFailedRayResult;

    public void Start()
    {
        //targetPlayerOne();
    }
    public void Update()
    {
    }

        //set the first player as the target
    public void targetPlayerOne()
    {   
        //if the gameManager exists
        if (GameManager.instance != null)
        {   //if the player list exists
            if (GameManager.instance.players != null)
            {   
                //if there's at least 1 player
                if (GameManager.instance.players.Count > 0)
                {
                    //find the first instance of the player and make that the target
                    thetarget = GameManager.instance.players[0].pawn.gameObject;
                }
            }
        }
    }

    //testing raycast
    public bool visionObstructed(Transform startOfRay, Vector3 directionOfRay, GameObject target)
    {
        //startOfRay should just be the component owner, directionOfRay is the direction the ray will travel
        
        RaycastHit hit;
        //Physics.Raycast(startOfRay.position, directionOfRay, out hit, Mathf.Infinity);

        //enable the line render and have travel to the directionOfRay
        if (lineRendBool)
        {
        lineRend.enabled = true;
        lineRend.SetPosition(0, startOfRay.position);
        lineRend.SetPosition(1, directionOfRay);
        
        }

        Vector3 agentToTargetVector = target.transform.position - transform.position;
        //if there's something hit...otherwise...
        //the final parameter is the distance it'll travel and the out hit is the referece to the raycast
        if (Physics.Raycast(startOfRay.position, agentToTargetVector.normalized, out hit, maxDistance))
        {
            //if the target was hit...
            if (hit.collider == target.GetComponent<SphereCollider>())
            {
                if (printRayResult)
                {
                    Debug.Log("Spotted the target: " + target);
                }
                return true;
            }
            //if something other than the target was hit...
            else
            {
                if (printFailedRayResult)
                {
                    Debug.Log("Target blocked by: " + hit.collider);
                }
                return false;
            }
        }
        //if nothing was hit by the raycast...
        else
        {  
            if (printRayResult)
                {
                    Debug.Log("Nothing spotted");
                }
            return false;
        }
    }

    //check if the target is within the ai's field of vision
    public bool CanSee(GameObject target)
    {
        thetarget = target;
        //if this isn't working change the unity engine part to System.Numerics
        //get the distance between the target and the ai 
        Vector3 agentToTargetVector = target.transform.position - transform.position;

        //if this isn't working change the unity engine part to System.Numerics
        //the angle of the distance between the ai and target
        float angleToTarget = Vector3.Angle(agentToTargetVector, transform.forward);

        //if the angle is less than the field of vision...
        if (angleToTarget < fov)
        {
            if (isInFOV)
            {
                print("in fov");
            }
            return visionObstructed(transform, target.transform.position, target);
        }
        //if the field of vision is greater...
        else
        {
            if (isInFOV)
            {
                print("not in fov");
            }
            return false; 
        }  
    }
}
