using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : HSSObject
{
    public GameObject objHPBarPoint;

    public bool haveHUD = true;
    public bool isBoss = false;

    public string weaponKey;
    public string bodyKey;
    public string wheelKey;
    public string exKey;

    [HideInInspector]
    public GameObject objHUD = null;

    [HideInInspector]
    public bool isLive = false;

    private HUDPack hUDPack;

    private GameObject objWeapon;
    private GameObject objBody;
    private GameObject objWheel;

    private EnemyWeapon weapon;
    private Body body;
    private Wheel wheel;

    private Vector3 spawnPoint;
    private int prevHp;
    private Coroutine co_Move = null;

    public void Awake()
    {
        weapon = GetComponentInChildren<EnemyWeapon>();
        body = GetComponentInChildren<Body>();
        wheel = GetComponentInChildren<Wheel>();

        objWeapon = weapon.gameObject;
        objBody = body.gameObject;
        objWheel = wheel.gameObject;

        body.Init();
    }

    public void InitEnemy()
    {
        body.SetTransformCenter(objWheel);

        weapon.SetWeaponData(weaponKey);
        body.SetBodyData(bodyKey);
        wheel.SetWheelData(wheelKey);

        prevHp = body.entityStatus.HP;
    }

    private IEnumerator Co_Move()
    {
        while (true)
        {
            if (!isBoss)
            {
                if (SpawnManager.instance.isWaitCheck)
                {
                    SaveEnemy();
                    yield break;
                }

                if (weapon.raderFov2D.objTarget != null)
                {
                    Vector3 dir = weapon.raderFov2D.objTarget.transform.position - wheel.transform.position;
                    dir = dir.normalized;
                    wheel.MoveEnemyWheel(dir);
                }
            }

            yield return null;
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

        if (co_Move != null)
            StopCoroutine(co_Move);
        co_Move = StartCoroutine(Co_Move());
    }

    public float GetHpRatio()
    {
        return body.entityStatus.useHP / (float)body.entityStatus.HP;
    }

    public void HitProgress()
    {
        if (!isLive)
            return;

        int totalDamage = prevHp - (int)body.entityStatus.useHP;
        prevHp = (int)body.entityStatus.useHP;

        float rate = body.entityStatus.useHP / (float)body.entityStatus.HP;

        hUDPack.MakeDamageText(false, totalDamage);
        hUDPack.SetGage(rate);

        if (isBoss)
        {
            if(GameUI.instance.objBossHpFrame.activeSelf)
            {
                GameUI.instance.uiBossHpGage.fillAmount = rate;
            }
        }

        if (body.entityStatus.useHP <= 0)
        {
            HSSObjectPoolManager.instance.SpawnObject(exKey, body.transform.position, body.transform.rotation);
            SaveEnemy();
            return;
        }
    }

    private void SaveEnemy()
    {
        isLive = false;
        StopCoroutine(co_Move);
        co_Move = null;

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
