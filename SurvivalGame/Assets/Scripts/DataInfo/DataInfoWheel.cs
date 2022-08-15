using System;
using System.IO;
using System.IO.Compression;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

using UnityEditor;
using Newtonsoft.Json.Linq;

public class DataInfoWheel : DataInfoBase
{
    private string prefabPathPlayer = "Prefabs/Player/Wheel/";
    private string prefabPathEnemy = "Prefabs/Enemy/Wheel/";

    public Dictionary<string, WheelData> dicWheelDataList = new Dictionary<string, WheelData>();

    public IEnumerator InitData()
    {
        EDataLoadResult type = Load();

        switch(type)
        {
            case EDataLoadResult.Complate:
                Debug.Log("DataInfoWheel - Load Complate");
                break;
            case EDataLoadResult.Fail:
                Debug.Log("DataInfoWheel - Load Fail");
                break;
            case EDataLoadResult.Skip:
                Debug.Log("DataInfoWheel - Already have a key");
                break;
        }

        yield return true;
    }

    public EDataLoadResult Load()
    {
        WheelData[] wheelData = UtilFunction.LoadJson<WheelData>("DataInfoWheel");

        if (wheelData == null)
            return EDataLoadResult.Fail;

        for (int i = 0; i < wheelData.Length; ++i)
        {
            if (wheelData[i].eOwner == EOwner.Player)
                wheelData[i].objPrefab = Resources.Load<GameObject>(prefabPathPlayer + wheelData[i].prefabName);
            else
                wheelData[i].objPrefab = Resources.Load<GameObject>(prefabPathEnemy + wheelData[i].prefabName);

            if (wheelData[i].objPrefab == null)
                Debug.LogFormat("objPrefab Null -> Key: {0}", wheelData[i].prefabName);

            if (dicWheelDataList.ContainsKey(wheelData[i].key) == false)
                dicWheelDataList.Add(wheelData[i].key, wheelData[i]);
            else
                return EDataLoadResult.Skip;
        }

        return EDataLoadResult.Complate;
    }

    public GameObject GetPrefabData(string key)  // itemcode
    {
        return dicWheelDataList[key].objPrefab;
    }

    public WheelData GetWheelData(string key)  // itemcode
    {
        return dicWheelDataList[key];
    }
}

[System.Serializable]
public class WheelData
{
    public string key;
    public EOwner eOwner;
    public string prefabName;
    public float moveSpeed;
    public float rotationSpeed;

    public GameObject objPrefab;
}
