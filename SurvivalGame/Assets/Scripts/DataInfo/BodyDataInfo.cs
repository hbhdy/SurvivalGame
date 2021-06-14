using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "BodyDataInfo", menuName = "DataScript/BodyDataInfo", order = 1)]
public class BodyDataInfo : ScriptableObject
{
    public string prefabPath;

    public List<BodyData> bodyDataList = new List<BodyData>();

    public Dictionary<string, BodyData> dicBodyDataList = new Dictionary<string, BodyData>();

    public IEnumerator InitData()
    {
        for (int i = 0; i < bodyDataList.Count; ++i)
        {
            dicBodyDataList.Add(bodyDataList[i].itemCode, bodyDataList[i]);
        }

        yield return true;
    }

    public GameObject GetPrefabData(string key)  // itemcode
    {
        return dicBodyDataList[key].objPrefab;
    }

    public BodyData GetBodyData(string key)  // itemcode
    {
        return dicBodyDataList[key];
    }

#if UNITY_EDITOR
    public void OnEnable()
    {
        EditorUtility.SetDirty(this);
    }
#endif
}

[System.Serializable]
public class BodyData
{
    public string itemCode;
        
    public string prefabName;
    public GameObject objPrefab;

    public int hp = 0;
    public int defence = 0;
}