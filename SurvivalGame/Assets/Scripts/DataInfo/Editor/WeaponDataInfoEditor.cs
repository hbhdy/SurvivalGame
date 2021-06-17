using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WeaponDataInfo))]
public class WeaponDataInfoEditor : Editor
{
    WeaponDataInfo info = null;

    private void OnEnable()
    {
        info = (WeaponDataInfo)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("CSV Reader [���� �б�]"))
        {
            if (info.name == "WeaponDataInfo")
                CSVFunction.WeaponDataInfoReader(info);
            else if (info.name == "EnemyWeaponDataInfo")
                CSVFunction.EnemyWeaponDataInfoReader(info);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("CSV Writer [���� ����]"))
        {
            if (info.name == "WeaponDataInfo")
                CSVFunction.WeaponDataInfoWriter(info);
            else if (info.name == "EnemyWeaponDataInfo")
                CSVFunction.EnemyWeaponDataInfoWriter(info);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        for (int i = 0; i < info.weaponDataList.Count; ++i)
        {
            info.weaponDataList[i].objPrefab = (GameObject)Resources.Load(info.prefabPath + info.weaponDataList[i].prefabName);
        }

        base.OnInspectorGUI();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(info);
        }
    }
}
