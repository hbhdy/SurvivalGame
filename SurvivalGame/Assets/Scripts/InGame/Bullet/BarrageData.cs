using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "BarrageData", menuName = "DataScript/BarrageData", order = 4)]
public class BarrageData : ScriptableObject
{
    public List<Barrage> barrageDataList = new List<Barrage>();

    public Dictionary<string, Barrage> dicBarrageDataList = new Dictionary<string, Barrage>();

    public IEnumerator InitData()
    {
        for (int i = 0; i < barrageDataList.Count; ++i)
        {
            dicBarrageDataList.Add(barrageDataList[i].key, barrageDataList[i]);
        }

        yield return true;
    }

    public Barrage GetBarrageData(string key)  // itemcode
    {
        return dicBarrageDataList[key];
    }

#if UNITY_EDITOR
    public void OnEnable()
    {
        EditorUtility.SetDirty(this);
    }
#endif
}

[System.Serializable]
public class Barrage
{
    public string key;
    public EBarrageType eBarrageType;
    public float startAngle;
    public float addAngle;
    public float firingTime;
    public float firingDelay;
    public int bulletCount;
}
