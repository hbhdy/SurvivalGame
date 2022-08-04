using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSSObjectPool : MonoBehaviour
{
    private string poolKey;
    private GameObject targetObj;
    private GameObject parentObj;
    private Stack<GameObject> objectPool = null;

    public HSSObjectPool(string key, GameObject obj, int count, GameObject parent)
    {
        poolKey = key;
        targetObj = obj;
        parentObj = parent;

        MakeObjectPool(count);
    }

    // �� ������Ʈ�� ���� ���� ( �̸�, Ű, �θ� ) - > ���� �߰�
    public void MakeObjectPool(int count)
    {
        GameObject obj = null;
        objectPool = new Stack<GameObject>(count);

        for (int i = 0; i < count; i++)
        {
            if (targetObj == null)
                continue;

            obj = Object.Instantiate(targetObj, parentObj.transform);

            obj.GetComponent<HSSObject>().SetOriginParent(parentObj.transform);
            obj.GetComponent<HSSObject>().SetKey(poolKey);

            obj.SetActive(false);
            obj.name = string.Format("{0}_{1}", targetObj.name, obj.GetInstanceID());

            objectPool.Push(obj);
        }
    }

    public GameObject SpawnObject(Vector3 pos, Quaternion qt, Transform parent = null, int level = 1)
    {
        // Ǯ���� ������ �߰�
        if (objectPool.Count == 0)
            MakeObjectPool(4);

        // Ǯ���� ���� �Ҵ�
        GameObject obj = objectPool.Pop();

        obj.transform.position = pos;
        if (qt != Quaternion.identity)
            obj.transform.localRotation = qt;

        obj.SetActive(true);

        if (parent == null)
            obj.GetComponent<HSSObject>().Spawn(null);
        else
            obj.GetComponent<HSSObject>().Spawn(parent);

        return obj;
    }

    public void SaveObject(GameObject obj)
    {
        if (SavePoolCheck(obj))
            return;

        // ��ġ �� ȸ�� �ʱ�ȭ
        obj.SetActive(false);
        obj.transform.position = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        // �θ� �Ҵ�
        obj.GetComponent<HSSObject>().Save();

        // Ǯ�� �ֱ�
        objectPool.Push(obj);
    }

    // ������Ʈ Ǯ�� ���Դ��� üũ
    public bool SavePoolCheck(GameObject obj)
    {
        if (objectPool.Contains(obj))
            return true;

        return false;
    }
}
