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

    // 각 오브젝트의 개별 설정 ( 이름, 키, 부모 ) - > 스택 추가
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
        // 풀에서 없으면 추가
        if (objectPool.Count == 0)
            MakeObjectPool(1);

        // 풀에서 빼서 할당
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

        // 위치 및 회전 초기화
        obj.SetActive(false);
        obj.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        // 부모 할당
        obj.GetComponent<HSSUIObject>().Save();

        // 풀에 넣기
        objectPool.Push(obj);
    }

    public bool CheckSave(GameObject obj)
    {
        if (objectPool.Contains(obj))
            return true;

        return false;
    }
}
