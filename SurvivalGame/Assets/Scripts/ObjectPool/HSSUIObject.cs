using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UI 오브젝트 풀의 기본 객체
public class HSSUIObject : MonoBehaviour
{
    protected string key;
    private Transform originParentTr;

    public void SetOriginParent(Transform parent)
    {
        originParentTr = transform.parent;
    }

    public void SetKey(string name)
    {
        key = name;
    }

    public virtual void Spawn(Transform parent = null)
    {
        if (parent != null)
            transform.SetParent(parent);
    }

    public void Save()
    {
        transform.SetParent(originParentTr);
    }
}
