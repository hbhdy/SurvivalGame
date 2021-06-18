using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public List<SpawnerInfo> spawnerInfos = new List<SpawnerInfo>();
    public List<SpawnKeyInfo> spawnKeyInfos = new List<SpawnKeyInfo>();

    private float spawnKeyPercentTotal = 0.0f;
    private float percentRatio = 0.0f;

    [HideInInspector]
    public FOV2D fov2d;

    [HideInInspector]
    public bool spawnClear = false;

    public void Awake()
    {
        for (int i = 0; i < spawnerInfos.Count; ++i)
        {
            spawnerInfos[i].isFirst = true;

            spawnKeyPercentTotal += spawnerInfos[i].percent;
        }

        percentRatio = spawnKeyPercentTotal / 100.0f;

        fov2d = this.GetComponent<FOV2D>();
    }

    public void FixedUpdate()
    {
        if (!InGameCore.instance.isInGameCoreReady)
            return;

        SpawnTimerCheck();

        CheckLiveEnemy();

        if (fov2d.isTargetInside)
        {
            for (int i = 0; i < spawnerInfos.Count; ++i)
            {
                if (spawnerInfos[i].isFirst)
                {
                    spawnerInfos[i].isUsed = true;
                    StartCoroutine(SpawnEnemy(spawnerInfos[i]));
                    spawnerInfos[i].isFirst = false;
                    spawnerInfos[i].canActive = false;
                }
                else
                {
                    if (spawnerInfos[i].canActive)
                    {
                        StartCoroutine(SpawnEnemy(spawnerInfos[i]));
                        spawnerInfos[i].canActive = false;
                    }
                }
            }
        }
    }

    //// ������ ������ ��� Ȱ��ȭ
    //public void ActivePoint()
    //{
    //    for (int i = 0; i < spawnerInfos.Count; ++i)
    //    {
    //        if (spawnerInfos[i].isLinked)
    //        {
    //            spawnerInfos[i].objLinkedObject.SetActive(true);
    //        }
    //    }
    //}

    //// ������ ����� ��� ��Ȱ��ȭ
    //public void DeactivePoint()
    //{
    //    for (int i = 0; i < spawnerInfos.Count; ++i)
    //    {
    //        if (spawnerInfos[i].isLinked)
    //        {
    //            spawnerInfos[i].objLinkedObject.SetActive(false);
    //        }
    //    }
    //}

    // canRecycle = true�� ���, interval�� ���� �ٽ� ������
    public void SpawnTimerCheck()
    {
        for (int i = 0; i < spawnerInfos.Count; ++i)
        {
            if (!spawnerInfos[i].canRecycle)
                continue;

            if (spawnerInfos[i].objLinkedObject == null)
            {
                spawnerInfos[i].addTimer += Time.deltaTime;

                if (spawnerInfos[i].addTimer >= spawnerInfos[i].spawnTimer)
                {
                    spawnerInfos[i].addTimer = 0.0f;
                    spawnerInfos[i].canActive = true;
                }
            }
        }
    }

    // ���� ���� ����Ʈ�� ���� ��� �����Ͽ����� üũ
    public void CheckLiveEnemy()
    {
        bool check = false;
        bool enterRoutine = false;
        for (int i = 0; i < spawnerInfos.Count; ++i)
        {
            if (spawnerInfos[i].isLinked)
            {
                if (!spawnerInfos[i].objLinkedObject.GetComponent<Enemy>().isLive)
                {
                    spawnerInfos[i].isLinked = false;
                    spawnerInfos[i].objLinkedObject = null;
                    enterRoutine = true;
                }
                else
                {
                    if (!spawnerInfos[i].isFirst)
                        check = true;
                }
            }
        }

        if (!check && enterRoutine)
            spawnClear = true;
    }

    public IEnumerator SpawnEnemy(SpawnerInfo info)
    {
        //GameObject objCreateEffect = Instantiate(Core.RSS.LoadGameObject("Prefabs\\Effect\\Enemy\\Respawn\\FX_EnemyRespawn"), info.objPoint.transform.position, Quaternion.identity);

        //yield return new WaitForSeconds(objCreateEffect.GetComponent<ParticleSystem>().main.startLifetimeMultiplier);

        float rand = Random.Range(0.0f, 1000.0f);
        rand = (rand / 10.0f) * percentRatio;
        int index = GetIntervalIndex(rand);

        if (spawnKeyInfos[index].spawnKey != "")
        {
            info.objLinkedObject = HSSObjectPoolManager.instance.SpawnObject(spawnKeyInfos[index].spawnKey, info.objPoint.transform.position, info.objPoint.transform.rotation, null);

            info.isLinked = true;
        }

        yield return null;
    }

    public int GetIntervalIndex(float rand)
    {
        for (int i = 0; i < spawnerInfos.Count; ++i)
        {
            spawnKeyPercentTotal += spawnerInfos[i].percent;
            if (rand < spawnKeyPercentTotal)
                return i;
        }

        return 0;
    }
}
