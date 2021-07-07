using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "WheelDataInfo", menuName = "DataScript/WheelDataInfo", order = 2)]
public class WheelDataInfo : ScriptableObject
{
    public string prefabPath;

    public List<WheelData> wheelDataList = new List<WheelData>();

    public Dictionary<string, WheelData> dicWheelDataList = new Dictionary<string, WheelData>();

    public IEnumerator InitData()
    {
        for (int i = 0; i < wheelDataList.Count; ++i)
        {
            dicWheelDataList.Add(wheelDataList[i].itemCode, wheelDataList[i]);
        }

        yield return true;
    }

    public GameObject GetPrefabData(string key)  // itemcode
    {
        return dicWheelDataList[key].objPrefab;
    }

    public WheelData GetWheelData(string key)  // itemcode
    {
        return dicWheelDataList[key];
    }

#if UNITY_EDITOR
    public void OnEnable()
    {
        EditorUtility.SetDirty(this);
    }
#endif
}

[System.Serializable]
public class WheelData
{
    public string itemCode;

    public string prefabName;
    public GameObject objPrefab;

    public float movingSpeed = 1f;
    public float rotateSpeed = 360f;
}
