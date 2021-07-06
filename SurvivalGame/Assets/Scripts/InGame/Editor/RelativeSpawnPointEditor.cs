using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// 해당 보스의 체력에 따라 작동하는 스포너 ( 페이즈 변화, 난이도 조절 등을 연출 )
[CustomEditor(typeof(RelativeSpawnPoint))]
public class RelativeSpawnPointEditor : Editor
{
    RelativeSpawnPoint point = null;

    private void OnEnable()
    {
        point = (RelativeSpawnPoint)target;
    }

    public override void OnInspectorGUI()
    {
        int index = 0;
        int pindex = 0;
        bool del = false;
        bool pdel = false;

        if (GUILayout.Button("+ Add Step", GUILayout.Width(100)))
        {
            GroupSpawn info = new GroupSpawn();
            point.pointList.Add(info);
        }

        for (int i = 0; i < point.pointList.Count; ++i)
        {
            GUILayout.BeginVertical(GUI.skin.box);
            string name = point.pointList[i].hpStep.ToString();

            point.pointList[i].isFold = EditorGUILayout.Foldout(point.pointList[i].isFold, name);

            if (point.pointList[i].isFold)
            {
                point.pointList[i].hpStep = EditorGUILayout.Slider("HP Step", point.pointList[i].hpStep, 0f, 100f);           

                point.pointList[i].spawnKey = EditorGUILayout.TextField("Spawn Key", point.pointList[i].spawnKey);
                point.pointList[i].level = EditorGUILayout.IntField("Enemy Level", point.pointList[i].level);

                GUILayout.BeginVertical(GUI.skin.box);
                if (GUILayout.Button(" + Add Point", GUILayout.Width(80)))
                {
                    point.pointList[i].objPoint.Add(null);
                }

                for (int j = 0; j < point.pointList[i].objPoint.Count; ++j)
                {
                    point.pointList[i].objPoint[j] = (GameObject)EditorGUILayout.ObjectField("PointLink", point.pointList[i].objPoint[j], typeof(GameObject), true);
                    if (GUILayout.Button(" - Del Point", GUILayout.Width(80)))
                    {
                        pindex = j;
                        pdel = true;
                    }
                }

                if (pdel)
                {
                    point.pointList[i].objPoint.RemoveAt(pindex);
                }
                GUILayout.EndVertical();

                if (GUILayout.Button("- Del Step", GUILayout.Width(100)))
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
            point.pointList.RemoveAt(index);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(point);
        }
    }

}
