using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public List<SpawnerInfo> spawnerInfos = new List<SpawnerInfo>();
    public List<SpawnKeyInfo> spawnKeyInfos = new List<SpawnKeyInfo>();

    public FOV2D fov2d;

    [HideInInspector]
    public bool spawnClear = false;

    public void Awake()
    {
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

        int index = 0;

        if (spawnKeyInfos[index].spawnKey != "")
        {
            info.objLinkedObject = HSSObjectPoolManager.instance.SpawnObject(spawnKeyInfos[index].spawnKey, info.objPoint.transform.position, info.objPoint.transform.rotation, null);

            if (info.objLinkedObject.GetComponent<Enemy>() != null)
            {
                
            }

            info.isLinked = true;
        }

        yield return null;

    }
}
