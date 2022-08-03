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
    public bool isBoss = false;

    [HideInInspector]
    public GameObject objHUD = null;

    private HUDPack hUDPack;
    private EnemyWeapon weapon;
    private Body body;
    private Wheel wheel;
    private Vector3 spawnPoint;

    [HideInInspector]
    public bool isLive = false;

    private int prevHp;

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
        wheel.SetWheelData();

        prevHp = body.entityStatus.HP;
    }

    public void FixedUpdate()
    {
        if (!isBoss)
        {
            if (SpawnManager.instance.isWaitCheck)
            {
                SaveEnemy();
            }

            if (weapon.raderFov2D.objTarget != null)
            {
                Vector3 dir = weapon.raderFov2D.objTarget.transform.position - wheel.transform.position;
                dir = dir.normalized;
                wheel.MoveEnemyWheel(dir);
            }
        }
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

            hUDPack = UtilFunction.Find<HUDPack>(objHUD.transform);
            hUDPack.SetSteeringTarget(objHPBarPoint);
            hUDPack.SetGage(1.0f);
            hUDPack.Following();

            objHUD.SetActive(true);
        }
    }

    public float GetHpRatio()
    {
        return body.entityStatus.useHP / (float)body.entityStatus.HP;
    }

    public void HitProgress()
    {
        int totalDamage = prevHp - (int)body.entityStatus.useHP;
        prevHp = (int)body.entityStatus.useHP;

        float rate = body.entityStatus.useHP / (float)body.entityStatus.HP;

        hUDPack.MakeDamageText(false, totalDamage);
        hUDPack.SetGage(rate);

        if(isBoss)
        {
            if(GameUI.instance.objBossHpFrame.activeSelf)
            {
                GameUI.instance.uiBossHpGage.fillAmount = rate;
            }
        }

        if (body.entityStatus.useHP <= 0)
        {
            SaveEnemy();
            return;
        }
    }

    public void SaveEnemy()
    {
        if (!isLive)
            return;

        isLive = false;

        objBody.SetActive(false);

        if (isBoss)
            GameUI.instance.objBossHpFrame.SetActive(false);

        hUDPack.SetHPBar(false);

        Invoke("SaveInPool", 3.0f);
    }

    public void SaveInPool()
    {
        HSSObjectPoolManager.instance.SaveObject(key, gameObject);

        hUDPack = null;
        Destroy(objHUD);
    }
}
