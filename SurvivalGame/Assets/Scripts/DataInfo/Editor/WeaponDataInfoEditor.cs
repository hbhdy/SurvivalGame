using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DataInfoWeapon))]
public class WeaponDataInfoEditor : Editor
{
    DataInfoWeapon info = null;

    //private void OnEnable()
    //{
    //    info = (WeaponDataInfo)target;
    //}

    //public override void OnInspectorGUI()
    //{
    //    GUILayout.BeginHorizontal();
    //    if (GUILayout.Button("CSV Reader [파일 읽기]"))
    //    {
    //        if (info.name == "WeaponDataInfo")
    //            CSVFunction.WeaponDataInfoReader(info);
    //        else if (info.name == "EnemyWeaponDataInfo")
    //            CSVFunction.EnemyWeaponDataInfoReader(info);
    //    }
    //    GUILayout.EndHorizontal();

    //    GUILayout.Space(10);

    //    GUILayout.BeginHorizontal();
    //    if (GUILayout.Button("CSV Writer [파일 생성]"))
    //    {
    //        if (info.name == "WeaponDataInfo")
    //            CSVFunction.WeaponDataInfoWriter(info);
    //        else if (info.name == "EnemyWeaponDataInfo")
    //            CSVFunction.EnemyWeaponDataInfoWriter(info);
    //    }
    //    GUILayout.EndHorizontal();

    //    GUILayout.Space(10);

    //    for (int i = 0; i < info.weaponDataList.Count; ++i)
    //    {
    //        info.weaponDataList[i].objPrefab = (GameObject)Resources.Load(info.prefabPath + info.weaponDataList[i].prefabName);
    //    }

    //    base.OnInspectorGUI();

    //    if (GUI.changed)
    //    {
    //        EditorUtility.SetDirty(info);
    //    }
    //}
}
