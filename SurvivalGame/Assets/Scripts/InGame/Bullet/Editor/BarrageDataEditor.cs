using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BarrageData))]
public class BarrageDataEditor : Editor
{
    BarrageData info = null;

    private void OnEnable()
    {
        info = (BarrageData)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("CSV Reader [���� �б�]"))
        {
                CSVFunction.BarrageDataReader(info);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("CSV Writer [���� ����]"))
        {
                CSVFunction.BarrageDataWriter(info);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        base.OnInspectorGUI();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(info);
        }
    }
}
