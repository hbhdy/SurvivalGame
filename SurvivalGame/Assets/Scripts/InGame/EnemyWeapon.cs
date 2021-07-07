using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public EOwner eOwner = EOwner.AI;

    public string barrageKey;
    public string bulletKey;
    public WeaponData weaponData;

    private Barrage barrage;

    [HideInInspector]
    public RadarWithFOV2D raderFov2D;
    private FOV2D fov2D;
    private Enemy enemy;

    private bool isFireReady = true;
    private float addFireInterval = 0.0f;
    private float angle = 0;

    public List<Vector2> dirList = new List<Vector2>();

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

        barrage = Core.RSS.GetBarrageData(barrageKey);

        for (int i = 0; i < barrage.patten.Length; i++)
        {
            for (int j = 0; j < barrage.patten[i].boolDir.Length; j++)
            {
                if (barrage.patten[i].boolDir[j])
                {
                    dirList.Add(new Vector2((j - 7) * 0.1f, (7 - i) * 0.1f));
                }
            }
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

                Vector3 dir = raderFov2D.objTarget.transform.position - gameObject.transform.position;
                dir = dir.normalized;

                StartCoroutine(BulletRoutine());
            }
        }

        //if (!isFireReady)
        //{
        //    addFireInterval += Time.deltaTime;
        //    if (addFireInterval >= barrage.firingDelay)
        //    {
        //        isFireReady = true;
        //        addFireInterval = 0.0f;
        //    }
        //}
    }

    public IEnumerator BulletRoutine()
    {
        switch (barrage.eBarrageType)
        {
            case EBarrageType.Custom:
                while (addFireInterval <= barrage.fireRunningTime)
                {
                    if (!enemy.isLive)
                        yield break;

                    addFireInterval += Time.deltaTime;

                    if (barrage.isTargetOn)
                    {
                        Vector3 straightDir = raderFov2D.objTarget.transform.position - gameObject.transform.position;
                        straightDir = straightDir.normalized;

                        for (int i = 0; i < dirList.Count; i++)
                        {
                            GameObject bullet = HSSObjectPoolManager.instance.SpawnObject(bulletKey, gameObject.transform.position, gameObject.transform.rotation);
                            bullet.GetComponent<Bullet>().SetBulletDoubleDirection(dirList[i], straightDir);
                            bullet.GetComponent<Bullet>().SetBulletState(weaponData);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dirList.Count; i++)
                        {
                            GameObject bullet = HSSObjectPoolManager.instance.SpawnObject(bulletKey, gameObject.transform.position, gameObject.transform.rotation);
                            bullet.GetComponent<Bullet>().SetBulletDirection(dirList[i]);
                            bullet.GetComponent<Bullet>().SetBulletState(weaponData);
                        }
                    }
                    yield return new WaitForSeconds(barrage.fireInterval);
                }
                break;

            case EBarrageType.Straight:
                while (addFireInterval <= barrage.fireRunningTime)
                {
                    if (!enemy.isLive)
                        yield break;

                    addFireInterval += Time.deltaTime;

                    Vector3 straightDir = raderFov2D.objTarget.transform.position - gameObject.transform.position;
                    straightDir = straightDir.normalized;

                    for (int i = 0; i < barrage.bulletCount; i++)
                    {
                        GameObject bullet = HSSObjectPoolManager.instance.SpawnObject(bulletKey, gameObject.transform.position, gameObject.transform.rotation);
                        bullet.GetComponent<Bullet>().SetBulletDirection(straightDir);
                        bullet.GetComponent<Bullet>().SetBulletState(weaponData);
                        yield return new WaitForSeconds(0.1f);
                    }

                    yield return new WaitForSeconds(barrage.fireInterval);
                }
                break;
            case EBarrageType.Angle:
                while (addFireInterval <= barrage.fireRunningTime)
                {
                    if (!enemy.isLive)
                        yield break;

                    addFireInterval += Time.deltaTime;

                    Vector3 angleDir = raderFov2D.objTarget.transform.position - gameObject.transform.position;
                    angleDir = angleDir.normalized;

                    angle = barrage.startAngle;
                    for (int i = 0; i < barrage.bulletCount; i++)
                    {
                        Vector3 degree = Quaternion.Euler(0, 0, angle) * angleDir;
                        GameObject bullet = HSSObjectPoolManager.instance.SpawnObject(bulletKey, gameObject.transform.position, gameObject.transform.rotation);
                        bullet.GetComponent<Bullet>().SetBulletDirection(degree);
                        bullet.GetComponent<Bullet>().SetBulletState(weaponData);
                        angle += barrage.addAngle;
                    }
                    yield return new WaitForSeconds(barrage.fireInterval);
                }
                break;
            case EBarrageType.Shot:
                while (addFireInterval <= barrage.fireRunningTime)
                {
                    if (!enemy.isLive)
                        yield break;

                    addFireInterval += Time.deltaTime;

                    Vector3 shotDir = raderFov2D.objTarget.transform.position - gameObject.transform.position;
                    shotDir = shotDir.normalized;

                    for (int i = 0; i < barrage.bulletCount; i++)
                    {
                        shotDir = new Vector3(shotDir.x + Random.Range(-0.25f, 0.25f), shotDir.y + Random.Range(-0.1f, 0.1f));
                        GameObject bullet1 = HSSObjectPoolManager.instance.SpawnObject(bulletKey, gameObject.transform.position, gameObject.transform.rotation);
                        bullet1.GetComponent<Bullet>().SetBulletDirection(shotDir);
                        bullet1.GetComponent<Bullet>().SetBulletState(weaponData);
                    }
                    yield return new WaitForSeconds(barrage.fireInterval);
                }
                break;
            case EBarrageType.Tornado:
                Vector3 tornadoDir = raderFov2D.objTarget.transform.position - gameObject.transform.position;
                tornadoDir = tornadoDir.normalized;

                angle = barrage.startAngle;

                while (addFireInterval <= barrage.fireRunningTime)
                {
                    if (!enemy.isLive)
                        yield break;

                    addFireInterval += Time.deltaTime;

                    Vector3 degree = Quaternion.Euler(0, 0, angle) * tornadoDir;
                    GameObject bullet1 = HSSObjectPoolManager.instance.SpawnObject(bulletKey, gameObject.transform.position, gameObject.transform.rotation);
                    bullet1.GetComponent<Bullet>().SetBulletDirection(degree);
                    bullet1.GetComponent<Bullet>().SetBulletState(weaponData);
                    angle += barrage.addAngle;

                    yield return new WaitForSeconds(barrage.fireInterval);
                }
                break;
        }
    }
}
