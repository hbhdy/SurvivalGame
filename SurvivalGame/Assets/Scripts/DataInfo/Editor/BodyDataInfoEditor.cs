using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BodyDataInfo))]
public class BodyDataInfoEditor : Editor
{
    BodyDataInfo info = null;

    private void OnEnable()
    {
        info = (BodyDataInfo)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("CSV Reader [파일 읽기]"))
        {
            CSVFunction.BodyDataInfoReader(info);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("CSV Writer [파일 생성]"))
        {
            CSVFunction.BodyDataInfoWriter(info);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        for (int i = 0; i < info.bodyDataList.Count; ++i)
        {
            info.bodyDataList[i].objPrefab = (GameObject)Resources.Load(info.prefabPath + info.bodyDataList[i].prefabName);
        }

        base.OnInspectorGUI();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(info);
        }
    }
}
