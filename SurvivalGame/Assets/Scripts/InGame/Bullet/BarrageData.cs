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
    public float fireRunningTime;       // 탄막 발사하는 시간
    public float fireInterval;          // 탄막 발사 간격
    public float fireDelay;             // 발사시간 이후 대기 시간
    public int bulletCount;             // 한번에 발사하는 개수
}
