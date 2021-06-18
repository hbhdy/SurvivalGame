using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawnPoint))]
public class SpawnPointEditor : Editor
{
    SpawnPoint point = null;

    private void OnEnable()
    {
        point = (SpawnPoint)target;
    }

    public override void OnInspectorGUI()
    {
        int index = 0;
        bool del = false;

        if (GUILayout.Button("+ Add Point", GUILayout.Width(100)))
        {
            SpawnerInfo info = new SpawnerInfo();
            point.spawnerInfos.Add(info);
        }

        for (int i = 0; i < point.spawnerInfos.Count; ++i)
        {
            GUILayout.BeginVertical(GUI.skin.box);
            string name = "";
            if (point.spawnerInfos[i].objPoint != null)
            {
                name = point.spawnerInfos[i].objPoint.name;
            }
            else
                name = "Need Point";

            point.spawnerInfos[i].isFold = EditorGUILayout.Foldout(point.spawnerInfos[i].isFold, name);

            if (point.spawnerInfos[i].isFold)
            {
                point.spawnerInfos[i].canRecycle = EditorGUILayout.Toggle("Is Recycle", point.spawnerInfos[i].canRecycle);
                if (point.spawnerInfos[i].canRecycle)
                {
                    point.spawnerInfos[i].spawnTimer = EditorGUILayout.FloatField("Interval Time", point.spawnerInfos[i].spawnTimer);
                }               

                point.spawnerInfos[i].objPoint = (GameObject)EditorGUILayout.ObjectField("PointLink", point.spawnerInfos[i].objPoint, typeof(GameObject), true);

                if (GUILayout.Button("- Del Point", GUILayout.Width(100)))
                {
                    index = i;
                    del = true;
                }
                EditorGUILayout.Space();
            }
            GUILayout.EndVertical();
        }

        if (del)
        {
            point.spawnerInfos.RemoveAt(index);
        }

        del = false;

        EditorGUILayout.Space();
        if (GUILayout.Button("+ Add Key", GUILayout.Width(100)))
        {
            SpawnKeyInfo key = new SpawnKeyInfo();
            point.spawnKeyInfos.Add(key);
        }

        for (int i = 0; i < point.spawnKeyInfos.Count; ++i)
        {
            GUILayout.BeginVertical(GUI.skin.box);

            point.spawnKeyInfos[i].spawnKey = EditorGUILayout.TextField("Spawn Key", point.spawnKeyInfos[i].spawnKey);

            if (GUILayout.Button("- Del Key", GUILayout.Width(100)))
            {
                index = i;
                del = true;
            }

            EditorGUILayout.Space();

            GUILayout.EndVertical();
        }

        if (del)
        {
            point.spawnKeyInfos.RemoveAt(index);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(point);
        }
    }
}
