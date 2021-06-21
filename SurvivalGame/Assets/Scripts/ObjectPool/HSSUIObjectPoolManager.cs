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

    // Ű���� �ش� ������ ������Ʈ ����
    private Dictionary<string, HSSUIObjectPool> pools = new Dictionary<string, HSSUIObjectPool>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Dictionary ����
    public void MakeObjectPool()
    {
        for (int i = 0; i < poolTable.Length; ++i)
        {
            // �ش� ������ ���� ��� �н�
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

    // Ű�� ã�Ƽ� ������Ʈ Ǯ���� �ش� ��ġ�� ����
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

    // Ű�� ã�Ƽ� ������Ʈ Ǯ�� ��ȯ �� �ʱ�ȭ
    public void SaveObject(string key, GameObject obj)
    {
        HSSUIObjectPool pool = null;

        // Dictionary Ű������ Value �ٷ� �޾� ��
        if (pools.TryGetValue(key, out pool))
        {
            pool.SaveObject(obj);
        }
    }
}
