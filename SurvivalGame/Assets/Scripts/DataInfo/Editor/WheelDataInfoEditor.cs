using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DataInfoWheel))]
public class WheelDataInfoEditor : Editor
{
    DataInfoWheel info = null;

    //private void OnEnable()
    //{
    //    info = (WheelDataInfo)target;
    //}

    //public override void OnInspectorGUI()
    //{
    //    GUILayout.BeginHorizontal();
    //    if (GUILayout.Button("CSV Reader [파일 읽기]"))
    //    {
    //        if (info.name == "WheelDataInfo")
    //            CSVFunction.WheelDataInfoReader(info);
    //        else if (info.name == "EnemyWheelDataInfo")
    //            CSVFunction.EnemyWheelDataInfoReader(info);
    //    }
    //    GUILayout.EndHorizontal();

    //    GUILayout.Space(10);

    //    GUILayout.BeginHorizontal();
    //    if (GUILayout.Button("CSV Writer [파일 생성]"))
    //    {
    //        if (info.name == "WheelDataInfo")
    //            CSVFunction.WheelDataInfoWriter(info);
    //        else if (info.name == "EnemyWheelDataInfo")
    //            CSVFunction.EnemyWheelDataInfoWriter(info);
    //    }
    //    GUILayout.EndHorizontal();

    //    GUILayout.Space(10);

    //    for (int i = 0; i < info.wheelDataList.Count; ++i)
    //    {
    //        info.wheelDataList[i].objPrefab = (GameObject)Resources.Load(info.prefabPath + info.wheelDataList[i].prefabName);
    //    }

    //    base.OnInspectorGUI();

    //    if (GUI.changed)
    //    {
    //        EditorUtility.SetDirty(info);
    //    }
    //}
}
