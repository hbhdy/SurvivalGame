using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WheelDataInfo))]
public class WheelDataInfoEditor : Editor
{
    WheelDataInfo info = null;

    private void OnEnable()
    {
        info = (WheelDataInfo)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("CSV Reader [���� �б�]"))
        {
            CSVFunction.WheelDataInfoReader(info);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("CSV Writer [���� ����]"))
        {
            CSVFunction.WheelDataInfoWriter(info);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        for (int i = 0; i < info.wheelDataList.Count; ++i)
        {
            info.wheelDataList[i].objPrefab = (GameObject)Resources.Load(info.prefabPath + info.wheelDataList[i].prefabName);
        }

        base.OnInspectorGUI();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(info);
        }
    }
}
