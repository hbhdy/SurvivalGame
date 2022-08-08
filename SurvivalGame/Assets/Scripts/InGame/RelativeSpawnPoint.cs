using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativeSpawnPoint : MonoBehaviour
{
    public FOV2D fov2d;

    public GameObject objParent = null;

    public List<GroupSpawn> pointList = new List<GroupSpawn>();

    private bool isReady = false;
    private Enemy parentEnemy;

    public void LinkGameObject(GameObject objLink)
    {
        objParent = objLink;
        parentEnemy = objParent.GetComponent<Enemy>();
        isReady = true;
    }

    public void FixedUpdate()
    {
        if (!isReady)
            return;

        // 일정 비율이하에 도달하면 스폰
        for (int i = 0; i < pointList.Count; i++)
        {
            if (pointList[i].isUsed)
                continue;

            if (pointList[i].hpStep * 0.01f >= parentEnemy.GetHpRatio())
            {
                pointList[i].isUsed = true;

                for (int j = 0; j < pointList[i].objPoint.Count; ++j)
                    StartCoroutine(SpawnCoroutine(pointList[i], j));
            }
        }

        if (parentEnemy.GetHpRatio() <= 0)
        {
            for (int i = 0; i < pointList.Count; i++)
            {
                if (!pointList[i].isUsed)
                    continue;

                StopAllCoroutines();

                for (int j = 0; j < pointList[i].objLink.Count; j++)
                {
                    //pointList[i].objLink[j].GetComponent<Enemy>().SaveEnemy();
                }
            }

            isReady = false;
        }
    }

    public IEnumerator SpawnCoroutine(GroupSpawn info, int num)
    {
        GameObject obj = HSSObjectPoolManager.instance.SpawnObject(info.spawnKey, info.objPoint[num].transform.position, info.objPoint[num].transform.rotation, null, info.level);

        info.objLink.Add(obj);

        yield return null;
    }
}

[System.Serializable]
public class GroupSpawn
{
    public float hpStep;

    public string spawnKey;
    public int level = 1;
    public List<GameObject> objPoint = new List<GameObject>();
    public List<GameObject> objLink = new List<GameObject>();

    public bool isUsed = false;
    public bool isFold = true;
}
