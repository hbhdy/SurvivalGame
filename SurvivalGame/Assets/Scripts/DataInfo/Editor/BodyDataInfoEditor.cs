using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DataInfoBody))]
public class BodyDataInfoEditor : Editor
{
    DataInfoBody info = null;

    //private void OnEnable()
    //{
    //    info = (BodyDataInfo)target;
    //}

    //public override void OnInspectorGUI()
    //{
    //    GUILayout.BeginHorizontal();
    //    if (GUILayout.Button("CSV Reader [���� �б�]"))
    //    {
    //        if (info.name == "BodyDataInfo")
    //            CSVFunction.BodyDataInfoReader(info);
    //        else if (info.name == "EnemyBodyDataInfo")
    //            CSVFunction.EnemyBodyDataInfoReader(info);
    //    }
    //    GUILayout.EndHorizontal();

    //    GUILayout.Space(10);

    //    GUILayout.BeginHorizontal();
    //    if (GUILayout.Button("CSV Writer [���� ����]"))
    //    {
    //        if (info.name == "BodyDataInfo")
    //            CSVFunction.BodyDataInfoWriter(info);
    //        else if (info.name == "EnemyBodyDataInfo")
    //            CSVFunction.EnemyBodyDataInfoWriter(info);
    //    }
    //    GUILayout.EndHorizontal();

    //    GUILayout.Space(10);

    //    for (int i = 0; i < info.bodyDataList.Count; ++i)
    //    {
    //        info.bodyDataList[i].objPrefab = (GameObject)Resources.Load(info.prefabPath + info.bodyDataList[i].prefabName);
    //    }

    //    base.OnInspectorGUI();

    //    if (GUI.changed)
    //    {
    //        EditorUtility.SetDirty(info);
    //    }
    //}
}
