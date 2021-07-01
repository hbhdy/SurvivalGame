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

        if (GUILayout.Button("Add Patten"))
        {
            info.barrageDataList.Add(new Barrage());
        }

        if (GUILayout.Button("Remove Patten (Last Index)"))
        {
            info.barrageDataList.RemoveAt(info.barrageDataList.Count - 1);
        }

        GUILayout.Space(10);

        for (int i = 0; i < info.barrageDataList.Count; i++)
        {
            EditorGUI.BeginChangeCheck();
            GUILayout.BeginVertical(GUI.skin.box);
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Key", GUILayout.Width(100));
            string key = EditorGUILayout.TextArea(info.barrageDataList[i].key);
            GUILayout.EndHorizontal();
            
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(info, "Key");
                info.barrageDataList[i].key = key;
                SceneView.RepaintAll();
            }

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Barrage Type", GUILayout.Width(100));
            info.barrageDataList[i].eBarrageType = (EBarrageType)EditorGUILayout.EnumPopup(info.barrageDataList[i].eBarrageType, GUILayout.Width(255));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Bullet Count", GUILayout.Width(105));
            info.barrageDataList[i].bulletCount = EditorGUILayout.IntField(info.barrageDataList[i].bulletCount, GUILayout.Width(40));
            EditorGUILayout.LabelField("Start Angle", GUILayout.Width(80));
            info.barrageDataList[i].startAngle = EditorGUILayout.FloatField(info.barrageDataList[i].startAngle, GUILayout.Width(40));
            EditorGUILayout.LabelField("Add Angle", GUILayout.Width(80));
            info.barrageDataList[i].addAngle = EditorGUILayout.FloatField(info.barrageDataList[i].addAngle, GUILayout.Width(40));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Fire Running Time", GUILayout.Width(105));
            info.barrageDataList[i].fireRunningTime = EditorGUILayout.FloatField(info.barrageDataList[i].fireRunningTime, GUILayout.Width(40));
            EditorGUILayout.LabelField("Fire Interval", GUILayout.Width(80));
            info.barrageDataList[i].fireInterval = EditorGUILayout.FloatField(info.barrageDataList[i].fireInterval, GUILayout.Width(40));
            EditorGUILayout.LabelField("Fire Delay", GUILayout.Width(80));
            info.barrageDataList[i].fireDelay = EditorGUILayout.FloatField(info.barrageDataList[i].fireDelay, GUILayout.Width(40));
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            if (info.barrageDataList[i].eBarrageType == EBarrageType.Custom)
            {
                GUILayout.BeginVertical();
                info.barrageDataList[i].isFold = EditorGUILayout.Foldout(info.barrageDataList[i].isFold, "Custom");

                if (info.barrageDataList[i].isFold)
                {
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

                    GUILayout.Space(10);

                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("TargetOn", GUILayout.Width(105));
                    info.barrageDataList[i].isTargetOn = EditorGUILayout.Toggle(info.barrageDataList[i].isTargetOn);
                    GUILayout.EndHorizontal();

                    GUILayout.Space(10);

                    for (int x = 0; x < info.barrageDataList[i].patten.Length; x++)
                    {
                        GUILayout.BeginHorizontal();
                        for (int y = 0; y < info.barrageDataList[i].patten[x].boolDir.Length; y++)
                        {
                            info.barrageDataList[i].patten[x].boolDir[y] = EditorGUILayout.Toggle(info.barrageDataList[i].patten[x].boolDir[y], GUILayout.Width(18));
                        }
                        GUILayout.EndHorizontal();
                    }
                }
                GUILayout.EndVertical();
            }

            GUILayout.EndVertical();
            GUILayout.Space(10);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(info);
        }
    }

}
