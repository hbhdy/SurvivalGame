using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance = null;

    private List<ZonePoint> zonePoints = new List<ZonePoint>();



    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        ZonePoint[] zones = GetComponentsInChildren<ZonePoint>();

        for (int i = 0; i < zones.Length; i++)
            zonePoints.Add(zones[i]);
    }

    public void Start()
    {

    }
}
