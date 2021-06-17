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
    public Weapon weapon;
    [HideInInspector]
    public Body body;
    [HideInInspector]
    public Wheel wheel;

    [HideInInspector]
    public AssembleData assembleData;

    [HideInInspector]
    public bool isMoving = false;
    [HideInInspector]
    public Vector3 moveDir = Vector3.zero;

    private Vector3 originPos;
    private Quaternion originQt;

    // 플레이어 초기화
    public IEnumerator InitPayer(AssembleData data)
    {
        assembleData = data;

        originPos = transform.position;
        originQt = transform.rotation;

        isMoving = false;

        yield return StartCoroutine(AssembleParts());

        weapon = objWeapon.GetComponent<Weapon>();
        body = objBody.GetComponent<Body>();
        wheel = objWheel.GetComponent<Wheel>();

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
}
