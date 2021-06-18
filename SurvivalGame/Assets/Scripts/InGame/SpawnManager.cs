using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance = null;

    private SpawnPoint[] spawnPoints;


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        spawnPoints = this.GetComponentsInChildren<SpawnPoint>();
    }

    public void Start()
    {

    }
}
