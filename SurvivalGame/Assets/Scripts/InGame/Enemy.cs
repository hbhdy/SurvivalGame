using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : HSSObject
{
    [Header("Enemy Object")]
    public GameObject objWeapon;
    public GameObject objBody;
    public GameObject objWheel;
    public GameObject objHPBarPoint;

    public bool haveHUD = true;

    [HideInInspector]
    public GameObject objHUD = null;

    [HideInInspector]
    public Vector3 spawnPoint;

    [HideInInspector]
    public EnemyWeapon weapon;
    [HideInInspector]
    public Body body;
    [HideInInspector]
    public Wheel wheel;

    [HideInInspector]
    public bool isLive = false;

    public void Awake()
    {
        weapon = objWeapon.GetComponent<EnemyWeapon>();
        body = objBody.GetComponent<Body>();
        wheel = objWheel.GetComponent<Wheel>();
    }

    public void InitEnemy()
    {
        body.SetTransformCenter(objWheel);

        body.SetBodyData();
        weapon.SetWeaponData();
    }

    public void FixedUpdate()
    {
        //if (objHUD != null)
        //{
        //    //objHPBarPoint.transform.position = objWheel.transform.position;
        //    objHUD.GetComponent<HUDPack>().Following();
        //}
    }

    public override void Spawn(Transform parent = null)
    {
        base.Spawn(parent);

        objBody.SetActive(true);
        objWheel.SetActive(true);

        spawnPoint = transform.position;

        InitEnemy();

        isLive = true;

        if(haveHUD)
        {
            objHUD = GameUI.instance.HUD.MackHudPack();
            objHUD.GetComponent<HUDPack>().SetSteeringTarget(objHPBarPoint);
            objHUD.GetComponent<HUDPack>().SetGage(1.0f);
            objHUD.GetComponent<HUDPack>().Following();

            objHUD.SetActive(true);
        }
    }
}
