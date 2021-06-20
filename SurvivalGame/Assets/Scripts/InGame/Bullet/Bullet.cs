using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : HSSObject
{
    private EOwner eOwner;

    public float moveDistance = 10.0f;
    public float damage;
    public float moveSpeed;
    public Vector3 moveDir = Vector3.up;

    private Vector3 startPos = Vector3.zero;
    private Vector3 originStartPos = Vector3.zero;
    private bool isReady = false;

    public void OnEnable()
    {
        isReady = false;
    }

    public override void Spawn(Transform parent = null)
    {
        base.Spawn(parent);

        startPos = transform.position;
        originStartPos = startPos;

        isReady = true;
    }

    public void SetBulletDirection(Vector3 dir)
    {
        moveDir = dir;
        //transform.rotation = Quaternion.FromToRotation(Vector3.up, moveDir);
    }


    public void FixedUpdate()
    {
        if (!isReady)
            return;

        Vector3 newPos = transform.position;

        newPos += moveDir * moveSpeed * Time.deltaTime;

        if (Vector2.Distance(originStartPos, newPos) >= moveDistance)
        {
            isReady = false;

            HSSObjectPoolManager.instance.SaveObject(key, gameObject);
        }
        else
            transform.position = newPos;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D cp = collision.contacts[0];
        Vector3 pos = cp.point;

        isReady = false;

        HSSObjectPoolManager.instance.SaveObject(key, gameObject);
    }
}
