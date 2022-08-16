using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DataInfoBody : DataInfoBase
{
    public string prefabPathPlayer = "Prefabs/Player/Body/";
    public string prefabPathEnemy = "Prefabs/Enemy/Body/";

    public Dictionary<string, BodyData> dicBodyDataList = new Dictionary<string, BodyData>();

    public IEnumerator InitData()
    {
        EDataLoadResult type = Load();

        switch (type)
        {
            case EDataLoadResult.Complate:
                Debug.Log("DataInfoBody - Load Complate)");
                break;
            case EDataLoadResult.Fail:
                Debug.Log("DataInfoBody - Load Fail)");
                break;
            case EDataLoadResult.Skip:
                Debug.Log("DataInfoBody - Already have a key");
                break;
        }

        yield return true;
    }

    public EDataLoadResult Load()
    {
        BodyData[] bodyData = UtilFunction.LoadJson<BodyData>("DataInfoBody");

        if (bodyData == null)
            return EDataLoadResult.Fail;

        for (int i = 0; i < bodyData.Length; ++i)
        {
            if (bodyData[i].eOwner == EOwner.Player)
                bodyData[i].objPrefab = Resources.Load<GameObject>(prefabPathPlayer + bodyData[i].prefabName);
            else
                bodyData[i].objPrefab = Resources.Load<GameObject>(prefabPathEnemy + bodyData[i].prefabName);

            if (bodyData[i].objPrefab == null)
                Debug.LogFormat("objPrefab Null -> Key: {0}", bodyData[i].prefabName);

            if (dicBodyDataList.ContainsKey(bodyData[i].key) == false)
                dicBodyDataList.Add(bodyData[i].key, bodyData[i]);
            else
                return EDataLoadResult.Skip;
        }

        return EDataLoadResult.Complate;
    }

    public GameObject GetPrefabData(string key)  // itemcode
    {
        return dicBodyDataList[key].objPrefab;
    }

    public BodyData GetBodyData(string key)  // itemcode
    {
        return dicBodyDataList[key];
    }
}

[System.Serializable]
public class BodyData
{
    public string key;
    public EOwner eOwner;
    public string prefabName;
    public int hp = 0;
    public int defence = 0;

    public GameObject objPrefab;
}