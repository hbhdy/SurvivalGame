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

        if(raderFov2D.objTarget)
        {
            if (isFireReady)
            {
                isFireReady = false;

                Vector3 dir = raderFov2D.objTarget.transform.position - gameObject.transform.position;
                dir = dir.normalized;

                StartCoroutine(BulletRoutine(dir));

                //GameObject bullet = HSSObjectPoolManager.instance.SpawnObject(bulletKey, gameObject.transform.position, gameObject.transform.rotation);
                //bullet.GetComponent<Bullet>().SetBulletDirection(dir);
                //bullet.GetComponent<Bullet>().SetBulletState(weaponData);
            }
        }

        if (!isFireReady)
        {
            addFireInterval += Time.deltaTime;
            if (addFireInterval >= barrage.firingDelay)
            {
                isFireReady = true;
                addFireInterval = 0.0f;
            }
        }
    }

    public IEnumerator BulletRoutine(Vector3 dir)
    {
        switch (barrage.eBarrageType)
        {
            case EBarrageType.Straight:
                for (int i = 0; i < barrage.bulletCount; i++)
                {
                    GameObject bullet = HSSObjectPoolManager.instance.SpawnObject(bulletKey, gameObject.transform.position, gameObject.transform.rotation);
                    bullet.GetComponent<Bullet>().SetBulletDirection(dir);
                    bullet.GetComponent<Bullet>().SetBulletState(weaponData);
                }
                break;
            case EBarrageType.Angle:
                angle = barrage.startAngle;
                for (int i = 0; i < barrage.bulletCount; i++)
                {
                    Vector3 degree = Quaternion.Euler(0, 0, angle) * dir;
                    GameObject bullet = HSSObjectPoolManager.instance.SpawnObject(bulletKey, gameObject.transform.position, gameObject.transform.rotation);
                    bullet.GetComponent<Bullet>().SetBulletDirection(degree);
                    bullet.GetComponent<Bullet>().SetBulletState(weaponData);
                    angle += barrage.addAngle;
                }
                break;
            case EBarrageType.Shot:
                for (int i = 0; i < barrage.bulletCount; i++)
                {
                    dir = new Vector3(dir.x + Random.Range(-0.15f, 0.15f), dir.y + Random.Range(-0.15f, 0.15f));
                    GameObject bullet1 = HSSObjectPoolManager.instance.SpawnObject(bulletKey, gameObject.transform.position, gameObject.transform.rotation);
                    bullet1.GetComponent<Bullet>().SetBulletDirection(dir);
                    bullet1.GetComponent<Bullet>().SetBulletState(weaponData);
                    yield return null;
                }
                break;
            case EBarrageType.Tornado:
                angle = barrage.startAngle;
                for (int i = 0; i < barrage.bulletCount; i++)
                {
                    Vector3 degree = Quaternion.Euler(0, 0, angle) * dir;
                    GameObject bullet1 = HSSObjectPoolManager.instance.SpawnObject(bulletKey, gameObject.transform.position, gameObject.transform.rotation);
                    bullet1.GetComponent<Bullet>().SetBulletDirection(degree);
                    bullet1.GetComponent<Bullet>().SetBulletState(weaponData);
                    angle += barrage.addAngle;

                    yield return new WaitForSeconds(barrage.firingTime);
                    //yield return new WaitForEndOfFrame();
                }
                break;
        }

        yield return null;
    }
}
