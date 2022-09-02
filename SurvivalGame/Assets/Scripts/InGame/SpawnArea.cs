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
    //private bool isReady = false;

    Vector2 playerPos = new Vector2();

    private void Awake()
    {
        fov2d = this.GetComponent<FOV2D>();
        //isReady = true;
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

        yield return new WaitUntil(()=> InGameCore.instance.isInGameCoreReady);

        Player player = InGameCore.instance.playerSetting.player;  

        while (true)
        {
            playerPos = player.GetPlayerBodyPos();

            xPos = Random.Range(-halfRadius, halfRadius);
            yPos = Random.Range(-halfRadius, halfRadius);
            randPos = new Vector3(playerPos.x + xPos, playerPos.y + yPos, 0);

            HSSObjectPoolManager.instance.SpawnObject(enemyKey, randPos, Quaternion.identity, null);

            yield return waitTime;
        }
    }
}
