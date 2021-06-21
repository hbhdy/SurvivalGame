using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSSUIObjectPool : MonoBehaviour
{
    private string keyName;
    private GameObject targetObj;
    private GameObject parentObj;
    private Stack<GameObject> objectPool = null;

    public HSSUIObjectPool(string key, GameObject obj, int count, GameObject parent, bool addCount = false)
    {
        keyName = key;
        targetObj = obj;
        parentObj = parent;

        MakeObjectPool(count);
    }

    // �� ������Ʈ�� ���� ���� ( �̸�, Ű, �θ� ) - > ���� �߰�
    void MakeObjectPool(int count)
    {
        GameObject obj = null;
        objectPool = new Stack<GameObject>(count);

        for (int i = 0; i < count; ++i)
        {
            if (targetObj == null)
                continue;

            obj = Object.Instantiate(targetObj, parentObj.transform);

            obj.GetComponent<HSSUIObject>().SetOriginParent(parentObj.transform);
            obj.GetComponent<HSSUIObject>().SetKey(keyName);

            obj.SetActive(false);
            obj.name = obj.GetInstanceID().ToString();

            objectPool.Push(obj);
        }
    }

    public GameObject SpawnObject(Vector3 pos, Quaternion qt, Transform parent = null)
    {
        // Ǯ���� ������ �߰�
        if (objectPool.Count == 0)
            MakeObjectPool(1);

        // Ǯ���� ���� �Ҵ�
        GameObject obj = objectPool.Pop();

        obj.GetComponent<RectTransform>().anchoredPosition = pos;
        if (qt != Quaternion.identity)
            obj.transform.localRotation = qt;

        obj.SetActive(true);

        if (parent == null)
            obj.GetComponent<HSSUIObject>().Spawn();
        else
            obj.GetComponent<HSSUIObject>().Spawn(parent);

        return obj;
    }

    public void SaveObject(GameObject obj)
    {
        if (CheckSave(obj))
            return;

        // ��ġ �� ȸ�� �ʱ�ȭ
        obj.SetActive(false);
        obj.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        // �θ� �Ҵ�
        obj.GetComponent<HSSUIObject>().Save();

        // Ǯ�� �ֱ�
        objectPool.Push(obj);
    }

    public bool CheckSave(GameObject obj)
    {
        if (objectPool.Contains(obj))
            return true;

        return false;
    }
}
