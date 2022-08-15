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
        wheelData = Core.RSS.GetWheelData(wheelData.key);
    }

    public void MoveEnemyWheel(Vector3 dir)
    {
        Vector3 newPos = transform.position;

        newPos += dir * Time.deltaTime * wheelData.moveSpeed;

        rigid2D.MovePosition(newPos);
    }

    public void MoveWheel(float power)
    {
        Quaternion newQt = Quaternion.FromToRotation(Vector3.up, Joystick.instance.GetDir());
        newQt.x = 0.0f;
        newQt.y = 0.0f;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, newQt, wheelData.rotationSpeed * Time.deltaTime);

        moveDir = Vector3.Lerp(moveDir, Joystick.instance.GetDir(), Time.deltaTime);

        Vector3 newPos = transform.position;

        newPos += Joystick.instance.GetDir() * wheelData.moveSpeed * power * Time.deltaTime;
        rigid2D.MovePosition(newPos);

        rigid2D.angularVelocity = 0.0f;
    }

    public void BreakWheel()
    {
        rigid2D.angularVelocity = 0.0f;
        rigid2D.velocity = Vector2.zero;
    }
}
