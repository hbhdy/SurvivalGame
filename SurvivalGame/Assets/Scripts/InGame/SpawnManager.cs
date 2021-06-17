using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance = null;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
