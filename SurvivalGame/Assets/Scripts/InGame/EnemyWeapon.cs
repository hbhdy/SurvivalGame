using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public EOwner eOwner = EOwner.AI;

    public List<string> barrageKeyLists = new List<string>();

    public string bulletKey;
    public WeaponData weaponData;

    private List<Barrage> barrage = new List<Barrage>();

    [HideInInspector]
    public RadarWithFOV2D raderFov2D;
    private FOV2D fov2D;
    private Enemy enemy;

    private bool isFireReady = true;
    private float addFireInterval = 0.0f;
    private float addFireDelay = 0.0f;
    private float angle = 0;
    private int nowBarrageNum = 0;

    public Dictionary<int, List<Vector2>> dicDirLists = new Dictionary<int, List<Vector2>>();

    public void Awake()
    {
        fov2D = GetComponent<FOV2D>();
        raderFov2D = GetComponent<RadarWithFOV2D>();
        enemy = GetComponentInParent<Enemy>();
    }

    public void SetWeaponData()
    {
        isFireReady = true;

        weaponData = Core.RSS.GetEnemyWeaponData(weaponData.itemCode);

        for (int i = 0; i < barrageKeyLists.Count; i++)
        {
            barrage.Add(Core.RSS.GetBarrageData(barrageKeyLists[i]));
        }

        // 각 패턴에 따른 탄막 방향 저장 - Dictionary로 저장
        for (int i = 0; i < barrage.Count; i++)
        {
            if (barrage[i].eBarrageType != EBarrageType.Custom)
                continue;

            List<Vector2> dir = new List<Vector2>();
            for (int y = 0; y < barrage[i].patten.Length; y++)
            {
                for (int x = 0; x < barrage[i].patten[y].boolDir.Length; x++)
                {
                    if (barrage[i].patten[y].boolDir[x])
                    {
                        dir.Add(new Vector2((x - 7) * 0.1f, (7 - y) * 0.1f));
                    }
                }
            }
            dicDirLists.Add(i, dir);
        }
    }

    public void FixedUpdate()
    {
        if (!InGameCore.instance.isInGameCoreReady)
            return;

        if (!enemy.isLive)
            return;

        if (raderFov2D.objTarget)
        {
            if (isFireReady)
            {            
                isFireReady = false;

                StartCoroutine(BulletRoutine());
            }
            else
            {
                // 현재 패턴 공격 시간
                if (addFireInterval <= barrage[nowBarrageNum].fireRunningTime)
                    addFireInterval += Time.deltaTime;
                else
                {
                    // 딜레이 시간
                    if (addFireDelay <= barrage[nowBarrageNum].fireDelay)
                        addFireDelay += Time.deltaTime;
                    else
                    {
                        // 다음 패턴 결정 및 공격 설정
                        nowBarrageNum = Random.Range(0, barrage.Count);

                        addFireInterval = 0;
                        addFireDelay = 0;
                        isFireReady = true;
                    }
                }
            }
        }
    }

    public IEnumerator BulletRoutine()
    {
        switch (barrage[nowBarrageNum].eBarrageType)
        {
            case EBarrageType.Custom:
                while (addFireInterval <= barrage[nowBarrageNum].fireRunningTime)
                {
                    if (!enemy.isLive)
                        yield break;

                    if (barrage[nowBarrageNum].isTargetOn)
                    {
                        Vector3 straightDir = raderFov2D.objTarget.transform.position - gameObject.transform.position;
                        straightDir = straightDir.normalized;

                        for (int i = 0; i < dicDirLists[nowBarrageNum].Count; i++)
                        {
                            GameObject bullet = HSSObjectPoolManager.instance.SpawnObject(bulletKey, gameObject.transform.position, gameObject.transform.rotation);
                            bullet.GetComponent<Bullet>().SetBulletDoubleDirection(dicDirLists[nowBarrageNum][i], straightDir);
                            bullet.GetComponent<Bullet>().SetBulletState(weaponData);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dicDirLists[nowBarrageNum].Count; i++)
                        {
                            GameObject bullet = HSSObjectPoolManager.instance.SpawnObject(bulletKey, gameObject.transform.position, gameObject.transform.rotation);
                            bullet.GetComponent<Bullet>().SetBulletDirection(dicDirLists[nowBarrageNum][i]);
                            bullet.GetComponent<Bullet>().SetBulletState(weaponData);
                        }
                    }
                    yield return new WaitForSeconds(barrage[nowBarrageNum].fireInterval);
                }
                barrage[nowBarrageNum].isTargetOn = !barrage[nowBarrageNum].isTargetOn;
                break;

            case EBarrageType.Straight:
                while (addFireInterval <= barrage[nowBarrageNum].fireRunningTime)
                {
                    if (!enemy.isLive)
                        yield break;

                    Vector3 straightDir = raderFov2D.objTarget.transform.position - gameObject.transform.position;
                    straightDir = straightDir.normalized;

                    for (int i = 0; i < barrage[nowBarrageNum].bulletCount; i++)
                    {
                        GameObject bullet = HSSObjectPoolManager.instance.SpawnObject(bulletKey, gameObject.transform.position, gameObject.transform.rotation);
                        bullet.GetComponent<Bullet>().SetBulletDirection(straightDir);
                        bullet.GetComponent<Bullet>().SetBulletState(weaponData);
                        yield return new WaitForSeconds(0.1f);
                    }

                    yield return new WaitForSeconds(barrage[nowBarrageNum].fireInterval);
                }
                break;
            case EBarrageType.Angle:
                while (addFireInterval <= barrage[nowBarrageNum].fireRunningTime)
                {
                    if (!enemy.isLive)
                        yield break;

                    Vector3 angleDir = raderFov2D.objTarget.transform.position - gameObject.transform.position;
                    angleDir = angleDir.normalized;

                    angle = barrage[nowBarrageNum].startAngle;
                    for (int i = 0; i < barrage[nowBarrageNum].bulletCount; i++)
                    {
                        Vector3 degree = Quaternion.Euler(0, 0, angle) * angleDir;
                        GameObject bullet = HSSObjectPoolManager.instance.SpawnObject(bulletKey, gameObject.transform.position, gameObject.transform.rotation);
                        bullet.GetComponent<Bullet>().SetBulletDirection(degree);
                        bullet.GetComponent<Bullet>().SetBulletState(weaponData);
                        angle += barrage[nowBarrageNum].addAngle;
                    }
                    yield return new WaitForSeconds(barrage[nowBarrageNum].fireInterval);
                }
                break;
            case EBarrageType.Shot:
                while (addFireInterval <= barrage[nowBarrageNum].fireRunningTime)
                {
                    if (!enemy.isLive)
                        yield break;

                    Vector3 shotDir = raderFov2D.objTarget.transform.position - gameObject.transform.position;
                    shotDir = shotDir.normalized;

                    for (int i = 0; i < barrage[nowBarrageNum].bulletCount; i++)
                    {
                        shotDir = new Vector3(shotDir.x + Random.Range(-0.25f, 0.25f), shotDir.y + Random.Range(-0.1f, 0.1f));
                        GameObject bullet1 = HSSObjectPoolManager.instance.SpawnObject(bulletKey, gameObject.transform.position, gameObject.transform.rotation);
                        bullet1.GetComponent<Bullet>().SetBulletDirection(shotDir);
                        bullet1.GetComponent<Bullet>().SetBulletState(weaponData);
                    }
                    yield return new WaitForSeconds(barrage[nowBarrageNum].fireInterval);
                }
                break;
            case EBarrageType.Tornado:
                Vector3 tornadoDir = raderFov2D.objTarget.transform.position - gameObject.transform.position;
                tornadoDir = tornadoDir.normalized;

                angle = barrage[nowBarrageNum].startAngle;

                while (addFireInterval <= barrage[nowBarrageNum].fireRunningTime)
                {
                    if (!enemy.isLive)
                        yield break;
                    Vector3 degree = Quaternion.Euler(0, 0, angle) * tornadoDir;
                    GameObject bullet1 = HSSObjectPoolManager.instance.SpawnObject(bulletKey, gameObject.transform.position, gameObject.transform.rotation);
                    bullet1.GetComponent<Bullet>().SetBulletDirection(degree);
                    bullet1.GetComponent<Bullet>().SetBulletState(weaponData);
                    angle += barrage[nowBarrageNum].addAngle;

                    yield return new WaitForSeconds(barrage[nowBarrageNum].fireInterval);
                }
                break;
        }
    }
}
