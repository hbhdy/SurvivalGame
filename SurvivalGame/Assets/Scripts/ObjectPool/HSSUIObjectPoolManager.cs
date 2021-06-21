using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSSUIObjectPoolManager : MonoBehaviour
{
    public static HSSUIObjectPoolManager instance = null;

    [System.Serializable]
    public struct ObjectInfo
    {
        public string keyName;
        public GameObject prefab;
        public GameObject parent;
        public int count;
    }

    public ObjectInfo[] poolTable;

    // 키값과 해당 종류의 오브젝트 보관
    private Dictionary<string, HSSUIObjectPool> pools = new Dictionary<string, HSSUIObjectPool>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Dictionary 세팅
    public void MakeObjectPool()
    {
        for (int i = 0; i < poolTable.Length; ++i)
        {
            // 해당 프리팹 없을 경우 패스
            if (poolTable[i].prefab == null)
                continue;

            HSSUIObjectPool pool = null;

            if (poolTable[i].parent != null)
                pool = new HSSUIObjectPool(poolTable[i].keyName, poolTable[i].prefab, poolTable[i].count, poolTable[i].parent);
            else
                pool = new HSSUIObjectPool(poolTable[i].keyName, poolTable[i].prefab, poolTable[i].count, this.gameObject);

            pools.Add(poolTable[i].keyName, pool);
        }
    }

    // 키값 찾아서 오브젝트 풀에서 해당 위치로 생성
    public GameObject SpawnObject(string key, Vector3 pos, Quaternion qt, Transform parent = null)
    {
        HSSUIObjectPool pool = null;

        if (!pools.TryGetValue(key, out pool))
        {
            return null;
        }

        GameObject obj = pool.SpawnObject(pos, qt, parent);
   
        return obj;
    }

    // 키값 찾아서 오브젝트 풀로 귀환 및 초기화
    public void SaveObject(string key, GameObject obj)
    {
        HSSUIObjectPool pool = null;

        // Dictionary 키값으로 Value 바로 받아 씀
        if (pools.TryGetValue(key, out pool))
        {
            pool.SaveObject(obj);
        }
    }
}
