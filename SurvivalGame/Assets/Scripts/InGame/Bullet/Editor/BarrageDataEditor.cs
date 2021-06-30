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
        if (GUILayout.Button("CSV Reader [颇老 佬扁]"))
        {
                CSVFunction.BarrageDataReader(info);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("CSV Writer [颇老 积己]"))
        {
                CSVFunction.BarrageDataWriter(info);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        base.OnInspectorGUI();

        for (int i = 0; i < info.barrageDataList.Count; i++)
        {
            GUILayout.BeginVertical();
            if (GUILayout.Button("No." + i + " Patten Reset"))
            {
                for (int x = 0; x < info.barrageDataList[i].patten.Length; x++)
                {
                    for (int y = 0; y < info.barrageDataList[i].patten[x].boolDir.Length; y++)
                    {
                        info.barrageDataList[i].patten[x].boolDir[y] = false;
                    }
                }
            }

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
