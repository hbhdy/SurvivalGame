using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSSObjectCategory : MonoBehaviour
{
    public ObjectInfo[] poolTable;

    public int GetTableLength()
    {
        return poolTable.Length;
    }

    public void SetObjectInfoData()
    {
        for (int i = 0; i < poolTable.Length; i++)
        {
            poolTable[i].originParent = gameObject;
            poolTable[i].keyName = poolTable[i].prefab.name;
        }
    }
}
