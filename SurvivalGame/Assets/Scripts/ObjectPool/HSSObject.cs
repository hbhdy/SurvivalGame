using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 오브젝트 풀의 기본 객체
public class HSSObject : MonoBehaviour
{
    protected string key;
    private Transform trOriginParent;

    public void SetOriginParent(Transform parent)
    {
        trOriginParent = transform.parent;
    }

    public void SetKey(string setKey)
    {
        key = setKey;
    }

    public string GeyKey()
    {
        return key;
    }

    public virtual void Spawn(Transform parent = null, int level = 1)
    {
        if (parent != null)
            transform.SetParent(parent);
    }
    public void Save()
    {
        transform.SetParent(trOriginParent);
    }
}
