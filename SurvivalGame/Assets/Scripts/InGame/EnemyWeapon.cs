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

    private FOV2D fov2D;
    private RadarWithFOV2D raderFov2D;

    private bool isFireReady = true;
    private float addFireInterval = 0.0f;
    private float angle = 0;

    public void Awake()
    {
        fov2D = GetComponent<FOV2D>();
        raderFov2D = GetComponent<RadarWithFOV2D>();
    }

    public void SetWeaponData()
    {
        weaponData = Core.RSS.GetEnemyWeaponData(weaponData.itemCode);

        barrage = Core.RSS.GetBarrageData(barrageKey);
    }

    public void FixedUpdate()
    {
        if (!InGameCore.instance.isInGameCoreReady)
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
            case EBarrageType.Straight:
                while (addFireInterval <= barrage.fireRunningTime)
                {
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
