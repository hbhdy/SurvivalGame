using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public EOwner eOwner = EOwner.Player;

    public BodyData bodyData;

    [HideInInspector]
    public EntityStatus entityStatus = new EntityStatus();
    [HideInInspector]
    public GameObject objTransformCenter;
    [HideInInspector]
    public Player rootPlayer;
    [HideInInspector]
    public Enemy rootEnemy;

    private SpriteRenderer sprite;
    private Vector3 nowPos = Vector3.zero;
    private float flipCheck = 0;

    private bool isCenterReady = false;

    public void Init()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void SetBodyData()
    {
        bodyData = Core.RSS.GetBodyData(bodyData.key);

        if (eOwner == EOwner.Player)
            rootPlayer = GetComponentInParent<Player>();
        else
            rootEnemy = GetComponentInParent<Enemy>();

        entityStatus.HP = bodyData.hp;
        entityStatus.useHP = entityStatus.HP;
        entityStatus.DEF = bodyData.defence;
    }

    public void SetTransformCenter(GameObject obj)
    {
        objTransformCenter = obj;
        isCenterReady = true;
    }

    public float GetBodyHPRate()
    {
        return entityStatus.useHP / (float)entityStatus.HP;
    }

    public void FixedUpdate()
    {
        if (!isCenterReady)
            return;

        // Wheel 기준으로 Body가 따라붙음
        nowPos = objTransformCenter.transform.position;

        if(eOwner == EOwner.AI)
        {
            if(nowPos.x - transform.position.x >=0)
                sprite.flipX = true;
            else
                sprite.flipX = false;
        }

        nowPos.z = transform.position.z;
        transform.position = nowPos;

        if (Quaternion.Angle(transform.rotation, objTransformCenter.transform.rotation) > 0.01f)
            transform.rotation = Quaternion.Slerp(transform.rotation, objTransformCenter.transform.rotation, 360f * Time.deltaTime);
    }

    public void Hit(float hitDamage, bool critical)
    {
        hitDamage = UtilFunction.CalcDamage(hitDamage, entityStatus.DEF);

        entityStatus.useHP -= (int)hitDamage;

        if (entityStatus.useHP <= 0)       
            entityStatus.useHP = 0;

        if (eOwner == EOwner.Player)
        {
            rootPlayer.HitProgress();
        }
        else
        {
            rootEnemy.HitProgress();
        }
    }
}
