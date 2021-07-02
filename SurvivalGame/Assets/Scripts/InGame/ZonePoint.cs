using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SpawnPoint�� ��� ������ ( �ش� ������������ �� ZonePoint�� ���� )
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

        Debug.Log("�ش� �������� Ŭ����!!!");
    }
}
