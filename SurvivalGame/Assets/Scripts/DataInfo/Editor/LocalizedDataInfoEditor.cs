using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LocalizedDataInfo))]
public class LocalizedDataInfoEditor : Editor
{
    LocalizedDataInfo info = null;

    public void OnEnable()
    {
        info = (LocalizedDataInfo)target;

        info.Save();
    }

    public void OnDisable()
    {
        info.Save();
    }
}
