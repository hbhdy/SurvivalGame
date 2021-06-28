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
        if (GUILayout.Button("CSV Reader [파일 읽기]"))
        {
                CSVFunction.BarrageDataReader(info);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("CSV Writer [파일 생성]"))
        {
                CSVFunction.BarrageDataWriter(info);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        base.OnInspectorGUI();

        for (int i = 0; i < info.barrageDataList.Count; i++)
        {
            GUILayout.BeginVertical();
            for (int x = 0; x < info.barrageDataList[i].patten.Length; x++)
            {
                GUILayout.BeginHorizontal();
                for (int y = 0; y < info.barrageDataList[i].patten[x].boolDir.Length; y++)
                {
                    info.barrageDataList[i].patten[x].boolDir[y] = EditorGUILayout.Toggle(info.barrageDataList[i].patten[x].boolDir[y], GUILayout.Width(15));
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();

            GUILayout.Space(30);
        }


        if (GUI.changed)
        {
            EditorUtility.SetDirty(info);
        }
    }

}
