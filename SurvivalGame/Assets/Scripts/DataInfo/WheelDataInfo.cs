using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json.Linq;

public class WheelDataInfo : DataInfoBase
{
    public string prefabPath;

    private WheelData[] wheelLists;

    public List<WheelData> wheelDataList = new List<WheelData>();

    public Dictionary<string, WheelData> dicWheelDataList = new Dictionary<string, WheelData>();

    public IEnumerator InitData()
    {
        EDataLoadResult type = Load();

        switch(type)
        {
            case EDataLoadResult.Complate:
                Debug.Log("WheelDataInfo - Load Complate)");
                break;
            case EDataLoadResult.Fail:
                Debug.Log("WheelDataInfo - Load Fail)");
                break;
            case EDataLoadResult.Skip:
                Debug.Log("WheelDataInfo - Already have a key");
                break;
        }

        yield return true;
    }

    public EDataLoadResult Load()
    {
        string data = Resources.Load<TextAsset>("JsonFile/WheelDataInfo").text;
        wheelLists = Newtonsoft.Json.JsonConvert.DeserializeObject<WheelData[]>(data);

        if (wheelLists == null)
            return EDataLoadResult.Fail;

        for (int i = 0; i < wheelLists.Length; ++i)
        {
            if (dicWheelDataList.ContainsKey(wheelLists[i].key) == false)
                dicWheelDataList.Add(wheelLists[i].key, wheelLists[i]);
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

    public string prefabName;
    public GameObject objPrefab;

    public float movingSpeed = 1f;
    public float rotateSpeed = 360f;
}
