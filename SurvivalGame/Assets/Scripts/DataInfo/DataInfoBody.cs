using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DataInfoBody : DataInfoBase
{
    public string prefabPath;

    private BodyData[] bodyLists;

    // 사용 안함
    public List<BodyData> bodyDataList = new List<BodyData>();

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
        string data = Resources.Load<TextAsset>("JsonFile/DataInfoBody").text;
        bodyLists = Newtonsoft.Json.JsonConvert.DeserializeObject<BodyData[]>(data);

        if (bodyLists == null)
            return EDataLoadResult.Fail;

        for (int i = 0; i < bodyLists.Length; ++i)
        {
            if (dicBodyDataList.ContainsKey(bodyLists[i].key) == false)
                dicBodyDataList.Add(bodyLists[i].key, bodyLists[i]);
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
        
    public string prefabName;
    public GameObject objPrefab;

    public int hp = 0;
    public int defence = 0;
}