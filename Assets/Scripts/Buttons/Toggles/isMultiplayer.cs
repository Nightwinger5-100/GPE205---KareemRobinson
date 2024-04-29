using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isMultiplayer : MonoBehaviour
{
    public void switchMultiplayer()
    {
        GameManager.instance.muliplayer = !GameManager.instance.muliplayer;
    }
}
