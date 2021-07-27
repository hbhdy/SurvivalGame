using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//  ������ƮǮ�Ŵ����� ������ ������Ʈ 1. ����, 2. ����, 3.����  
public class HSSObjectPoolManager : MonoBehaviour
{
    public static HSSObjectPoolManager instance = null;

    // ī�װ��� ������ �ν����ͻ� ���ϰ� �����ϱ� ���� ( ��, �Ѿ� ��� )
    [HideInInspector]
    public HSSObjectCategory[] poolCategories;

    // ������ ī�װ��� ���������δ� ��Ƽ� ���� �� ó����
    [HideInInspector]
    public ObjectInfo[] poolTable;

    // Ű���� �ش� ������ ������Ʈ ����
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
                // ���� ����
                Array.Copy(poolCategories[i].poolTable, 0, poolTable, index, poolCategories[i].poolTable.Length);
                index += poolCategories[i].poolTable.Length;
            }
        }
    }

    // Dictionary ����
    public void MakeObjectPool()
    {
        for (int i = 0; i < poolTable.Length; i++)
        {
            // �ش� ������ ���� ��� �н�
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

    // Ű�� ã�Ƽ� ������Ʈ Ǯ���� �ش� ��ġ�� ����
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

    // Ű�� ã�Ƽ� ������Ʈ Ǯ�� ��ȯ �� �ʱ�ȭ
    public void SaveObject(string key, GameObject obj)
    {
        HSSObjectPool pool = null;

        // Dictionary Ű������ Value �ٷ� �޾� ��
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