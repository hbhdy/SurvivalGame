using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SpawnPoint를 묶어서 관리함 ( 해당 스테이지마다 각 ZonePoint가 켜짐 )
public class ZonePoint : MonoBehaviour
{
    [HideInInspector]
    public List<SpawnPoint> zoneLists = new List<SpawnPoint>();

    public void Start()
    {
        SpawnPoint[] zones = GetComponentsInChildren<SpawnPoint>();

        for (int i = 0; i < zones.Length; i++)
            zoneLists.Add(zones[i]);
    }

    public void FixedUpdate()
    {
        if (!gameObject.activeSelf)
            return;

        for(int i = 0; i < zoneLists.Count; i++)
        {
            if (!zoneLists[i].spawnClear)
                return;
        }

        Debug.Log("해당 스테이지 클리어!!!");
    }
}
