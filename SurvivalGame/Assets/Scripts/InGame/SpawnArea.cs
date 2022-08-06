using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public string enemyKey;
    public float spwnTime;

    [HideInInspector]
    public FOV2D fov2d;

    private Coroutine co_CreateEnemyRoutine = null;
    private bool isReady = false;

    private void Awake()
    {
        fov2d = this.GetComponent<FOV2D>();
        isReady = true;
    }

    private void Start()
    {
        co_CreateEnemyRoutine = StartCoroutine(Co_CreateEnemyRoutine());
    }

    private IEnumerator Co_CreateEnemyRoutine()
    {
        WaitForSeconds waitTime = new WaitForSeconds(spwnTime);

        Vector3 randPos = new Vector3();
        float halfRadius = fov2d.viewRadius / 2;
        float xPos = 0;
        float yPos = 0;

        while (true)
        {
            if (InGameCore.instance.isInGameCoreReady)
            {
                xPos = Random.Range(-halfRadius, halfRadius);
                yPos = Random.Range(-halfRadius, halfRadius);
                randPos = new Vector3(xPos, yPos, 0);
                Debug.Log(randPos);

                HSSObjectPoolManager.instance.SpawnObject(enemyKey, randPos, Quaternion.identity, null);
            }
            yield return waitTime;
        }
    }
}
