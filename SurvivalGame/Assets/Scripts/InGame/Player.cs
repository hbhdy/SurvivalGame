using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Move Status")]
    public float moveSpeed = 1.0f;
    public float rotateSpeed = 90.0f;

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
    public bool isMoving = false;
    [HideInInspector]
    public Vector3 moveDir = Vector3.zero;


    private Rigidbody2D rigid2d;
    private Vector3 originPos;
    private Quaternion originQt;

    public void Awake()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        weapon = objWeapon.GetComponent<Weapon>();
        body = objBody.GetComponent<Body>();
        wheel = objWheel.GetComponent<Wheel>();
    }

    // 플레이어 초기화
    public IEnumerator InitPayer(AssembleData data)
    {
        originPos = transform.position;
        originQt = transform.rotation;

        isMoving = false;

        yield return StartCoroutine(AssembleParts());

        yield return new WaitForEndOfFrame();
    }

    // 웨폰 바디 휠 조립
    public IEnumerator AssembleParts()
    {
        objBody = Instantiate(Core.RSS.GetBodyObject("PlayerBody_01"), transform.position, transform.rotation, transform);
        objWheel = Instantiate(Core.RSS.GetWheelObject("PlayerWheel_01"), transform.position, transform.rotation, transform);

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
