using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Move Status")]
    public float moveSpeed = 1.0f;
    public float rotateSpeed = 90.0f;

    [HideInInspector]
    public bool isMoving = false;
    [HideInInspector]
    public Vector3 moveDir = Vector3.zero;

    private Rigidbody2D rigid2d;

    public void Awake()
    {
        rigid2d = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        if (Joystick.instance.GetDir() != Vector3.zero)
        {
            MovePlayer(Joystick.instance.GetMoveForce());

            isMoving = true;
        }
        else
        {
            if (isMoving)
            {
                BreakPlayer();
                isMoving = false;
            }
        }
    }

    public void MovePlayer(float power)
    {
        Quaternion newQt = Quaternion.FromToRotation(Vector3.up, Joystick.instance.GetDir());
        newQt.x = 0.0f;
        newQt.y = 0.0f;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, newQt, rotateSpeed * Time.deltaTime);

        moveDir = Vector3.Lerp(moveDir, Joystick.instance.GetDir(), Time.deltaTime);

        Vector3 newPos = transform.position;

        newPos += Joystick.instance.GetDir() * moveSpeed * power * Time.deltaTime;
        rigid2d.MovePosition(newPos);

        rigid2d.angularVelocity = 0.0f;
    }

    public void BreakPlayer()
    {
        rigid2d.angularVelocity = 0.0f;
        rigid2d.velocity = Vector2.zero;
    }
}
