using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public EOwner eOwner = EOwner.Player;

    [HideInInspector]
    public GameObject objTransformCenter;

    private bool isCenterReady = false;

    public void SetTransformCenter(GameObject obj)
    {
        objTransformCenter = obj;
        isCenterReady = true;
    }
}
