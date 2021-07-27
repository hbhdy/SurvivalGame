using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//  오브젝트풀매니저는 지정한 오브젝트 1. 생성, 2. 저장, 3.관리  
public class HSSObjectPoolManager : MonoBehaviour
{
    public static HSSObjectPoolManager instance = null;

    // 카테고리를 나눠서 인스펙터상 편리하게 관리하기 위함 ( 적, 총알 등등 )
    [HideInInspector]
    public HSSObjectCategory[] poolCategories;

    // 나눠진 카테고리를 내부적으로는 모아서 관리 및 처리함
    [HideInInspector]
    public ObjectInfo[] poolTable;

    // 키값과 해당 종류의 오브젝트 보관
    private Dictionary<string, HSSObjectPool> pools = new Dictionary<string, HSSObjectPool>();


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        poolCategories = GetComponentsInChildren<HSSObjectCategory>();

        int maxCount = 0;
        for (int i = 0; i < poolCategories.Length; i++)
        {
            maxCount += poolCategories[i].GetTableLength();
        }

        if (poolCategories.Length > 0)
        {
            poolTable = null;
            poolTable = new ObjectInfo[maxCount];

            int index = 0;
            for (int i = 0; i < poolCategories.Length; i++)
            {
                // 깊은 복사
                Array.Copy(poolCategories[i].poolTable, 0, poolTable, index, poolCategories[i].poolTable.Length);
                index += poolCategories[i].poolTable.Length;
            }
        }
    }

    // Dictionary 세팅
    public void MakeObjectPool()
    {
        for (int i = 0; i < poolTable.Length; i++)
        {
            // 해당 프리팹 없을 경우 패스
            if (poolTable[i].prefab == null)
                continue;

            HSSObjectPool pool = null;

            if (poolTable[i].originParent != null)
                pool = new HSSObjectPool(poolTable[i].keyName, poolTable[i].prefab, poolTable[i].count, poolTable[i].originParent);
            else
                pool = new HSSObjectPool(poolTable[i].keyName, poolTable[i].prefab, poolTable[i].count, this.gameObject);

            pools.Add(poolTable[i].keyName, pool);
        }
    }

    // 키값 찾아서 오브젝트 풀에서 해당 위치로 생성
    public GameObject SpawnObject(string key, Vector3 pos, Quaternion qt, Transform parent = null, int level = 1, float scale = 1, bool setScale = false)
    {
        HSSObjectPool pool = null;

        if (!pools.TryGetValue(key, out pool))
        {
            return null;
        }

        GameObject obj = pool.SpawnObject(pos, qt, parent, level);

        if (setScale)
            obj.transform.localScale = new Vector3(scale, scale, 1);

        return obj;
    }

    // 키값 찾아서 오브젝트 풀로 귀환 및 초기화
    public void SaveObject(string key, GameObject obj)
    {
        HSSObjectPool pool = null;

        // Dictionary 키값으로 Value 바로 받아 씀
        if (pools.TryGetValue(key, out pool))
        {
            pool.SaveObject(obj);
        }
    }
}

[System.Serializable]
public struct ObjectInfo
{
    public string keyName;
    public GameObject prefab;
    public GameObject originParent;
    public int count;
}