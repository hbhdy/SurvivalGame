using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Object")]
    public GameObject objWeapon;
    public GameObject objBody;
    public GameObject objWheel;

    [HideInInspector]
    public GameObject objHUD = null;

    private HUDPack hudPack;
    private Weapon weapon;
    private Body body;
    private Wheel wheel;

    private AssembleData assembleData;
    private bool isMoving = false;
    private Vector3 moveDir = Vector3.zero;

    private Vector3 originPos;
    private Quaternion originQt;
    private int prevHp;

    private int playerExp = 0;

    // 플레이어 초기화
    public IEnumerator InitPayer(AssembleData data)
    {
        playerExp = 0;

        assembleData = data;

        originPos = transform.position;
        originQt = transform.rotation;

        isMoving = false;

        yield return StartCoroutine(AssembleParts());

        weapon = objWeapon.GetComponent<Weapon>();
        body = objBody.GetComponent<Body>();
        wheel = objWheel.GetComponent<Wheel>();

        body.SetBodyData(assembleData.bodyData.key);
        weapon.SetWeaponData(assembleData.weaponData.key);
        wheel.SetWheelData(assembleData.wheelData.key);

        objHUD = GameUI.instance.HUD.GetPlayerHudPack();

        hudPack = UtilFunction.Find<HUDPack>(objHUD.transform);
        hudPack.SetSteeringTarget(objWheel);

        GameUI.instance.playerHPState.LinkBody(body);

        prevHp = body.entityStatus.HP;

        yield return new WaitForEndOfFrame();
    }

    // 웨폰 바디 휠 조립
    public IEnumerator AssembleParts()
    {
        objWeapon = Instantiate(Core.RSS.GetWeaponObject(assembleData.weaponData.key), transform.position, transform.rotation, transform);
        objBody = Instantiate(Core.RSS.GetBodyObject(assembleData.bodyData.key), transform.position, transform.rotation, transform);
        objWheel = Instantiate(Core.RSS.GetWheelObject(assembleData.wheelData.key), transform.position, transform.rotation, transform);

        objWeapon.transform.parent = objBody.transform;

        objBody.GetComponent<Body>().SetTransformCenter(objWheel);

        yield return true;
    }

    public void FixedUpdate()
    {
        if (Joystick.instance.GetDir() != Vector3.zero)
        {
            wheel.MoveWheel(Joystick.instance.GetMoveForce());

            isMoving = true;
        }
        else
        {
            if (isMoving)
            {
                wheel.BreakWheel();
                isMoving = false;
            }
        }
    }

    public void HitProgress()
    {
        int totalDamage = prevHp - (int)body.entityStatus.useHP;
        prevHp = (int)body.entityStatus.useHP;

        float rate = body.entityStatus.useHP / (float)body.entityStatus.HP;

        hudPack.MakeDamageText(true, totalDamage);

        GameUI.instance.playerHPState.CalcHPState();
    }

    public void AddExp(int amount)
    {
        playerExp += amount;
        Debug.Log("Player Exp : " + playerExp);
    }

    public int GetExp()
    {
        return playerExp;
    }
}
