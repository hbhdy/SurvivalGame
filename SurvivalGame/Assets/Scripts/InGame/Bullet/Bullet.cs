using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : HSSObject
{
    private EOwner eOwner;

    [Header("Bullet State")]
    public float damage = 0;
    public float criChance = 0;
    public float criDamage = 0;
    public float moveDistance = 10.0f;
    public float moveSpeed;

    [HideInInspector]
    public Vector3 moveDir = Vector3.up;
    [HideInInspector]
    public Vector3 changeDir = Vector3.up;

    private Vector3 startPos = Vector3.zero;
    private Vector3 originStartPos = Vector3.zero;
    private bool isReady = false;
    private bool isChange = false;
    private float t = 0;

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
        isChange = false;
        t = 0;
    }

    public void SetBulletDoubleDirection(Vector3 dir, Vector3 change)
    {
        moveDir = dir;
        changeDir = change;

        isChange = true;
    }

    public void SetBulletDirection(Vector3 dir)
    {
        moveDir = dir;
    }

    public void SetBulletState(WeaponData data)
    {
        damage = data.attackDamage;
        criChance = data.criChance;
        criDamage = data.criDamage;
    }

    public void FixedUpdate()
    {
        if (!isReady)
            return;

        if(SpawnManager.instance.isWaitCheck)
        {
            isReady = false;

            HSSObjectPoolManager.instance.SaveObject(key, gameObject);
        }

        Vector3 newPos = transform.position;

        if (isChange)
        {
            if (t >= 0.6f)
            {
                newPos += changeDir * moveSpeed * Time.deltaTime;
            }
            else
            {
                newPos += moveDir * moveSpeed * Time.deltaTime;
            }
            t += Time.deltaTime;
        }
        else
        {
            newPos += moveDir * moveSpeed * Time.deltaTime;
        }

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

        if (collision.collider.gameObject.GetComponent<Body>())
            collision.collider.gameObject.GetComponent<Body>().Hit(damage, false);

        isReady = false;

        HSSObjectPoolManager.instance.SaveObject(key, gameObject);
    }
}
