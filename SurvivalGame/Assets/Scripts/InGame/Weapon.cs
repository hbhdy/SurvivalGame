using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public EOwner eOwner = EOwner.Player;

    public string bulletKey;
    public WeaponData weaponData;
    public float fireInterval = 0.0f;

    private FOV2D fov2D;
    private RadarWithFOV2D raderFov2D;

    private bool isFireReady = true;
    private float addFireInterval = 0.0f;

    public void Awake()
    {
        fov2D = GetComponent<FOV2D>();
        raderFov2D = GetComponent<RadarWithFOV2D>();
    }

    public void SetWeaponData()
    {
        weaponData = Core.RSS.GetWeaponData(weaponData.itemCode);
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

                GameObject bullet = HSSObjectPoolManager.instance.SpawnObject(bulletKey, gameObject.transform.position, gameObject.transform.rotation);
                bullet.GetComponent<Bullet>().SetBulletDirection(dir);
                bullet.GetComponent<Bullet>().SetBulletState(weaponData);
            }
        }

        if (!isFireReady)
        {
            addFireInterval += Time.deltaTime;
            if (addFireInterval >= fireInterval)
            {
                isFireReady = true;
                addFireInterval = 0.0f;
            }
        }
    }
}
