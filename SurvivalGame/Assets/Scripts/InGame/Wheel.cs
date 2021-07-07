using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public EOwner eOwner = EOwner.Player;

    public WheelData wheelData;

    [HideInInspector]
    public Vector3 moveDir = Vector3.zero;

    private Rigidbody2D rigid2D;


    public void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    public void SetWheelData()
    {
        if (eOwner == EOwner.Player)
        {
            wheelData = Core.RSS.GetWheelData(wheelData.itemCode);
        }
        else
        {
            wheelData = Core.RSS.GetEnemyWheelData(wheelData.itemCode);
        }
    }

    //public void FixedUpdate()
    //{
    //    if (!InGameCore.instance.isInGameCoreReady)
    //        return;

    //    if (eOwner == EOwner.AI)
    //    {

    //    }
    //    else
    //    {

    //    }
    //}

    public void MoveEnemyWheel(Vector3 dir)
    {
        Vector3 newPos = transform.position;

        newPos += dir * Time.deltaTime * wheelData.movingSpeed;

        rigid2D.MovePosition(newPos);
    }

    public void MoveWheel(float power)
    {
        Quaternion newQt = Quaternion.FromToRotation(Vector3.up, Joystick.instance.GetDir());
        newQt.x = 0.0f;
        newQt.y = 0.0f;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, newQt, wheelData.rotateSpeed * Time.deltaTime);

        moveDir = Vector3.Lerp(moveDir, Joystick.instance.GetDir(), Time.deltaTime);

        Vector3 newPos = transform.position;

        newPos += Joystick.instance.GetDir() * wheelData.movingSpeed * power * Time.deltaTime;
        rigid2D.MovePosition(newPos);

        rigid2D.angularVelocity = 0.0f;
    }

    public void BreakWheel()
    {
        rigid2D.angularVelocity = 0.0f;
        rigid2D.velocity = Vector2.zero;
    }
}
