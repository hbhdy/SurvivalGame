using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DataInfoLocalized))]
public class LocalizedDataInfoEditor : Editor
{
    DataInfoLocalized info = null;

    //public void OnEnable()
    //{
    //    info = (LocalizedDataInfo)target;

    //    info.Save();
    //}

    //public override void OnInspectorGUI()
    //{
    //    GUILayout.BeginHorizontal();
    //    if (GUILayout.Button("CSV Reader [파일 읽기]"))
    //    {
    //        CSVFunction.LocalizedDataReader(info);
    //    }
    //    GUILayout.EndHorizontal();

    //    GUILayout.Space(10);

    //    GUILayout.BeginHorizontal();
    //    if (GUILayout.Button("CSV Writer [파일 생성]"))
    //    {
    //        CSVFunction.LocalizedDataWriter(info);
    //    }
    //    GUILayout.EndHorizontal();

    //    if (GUI.changed)
    //    {
    //        EditorUtility.SetDirty(info);
    //    }
    //}


    //public void OnDisable()
    //{
    //    info.Save();
    //}
}
