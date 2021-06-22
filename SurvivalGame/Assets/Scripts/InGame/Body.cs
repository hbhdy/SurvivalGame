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

    private bool isCenterReady = false;

    public void SetBodyData()
    {
        if (eOwner == EOwner.Player)
        {
            bodyData = Core.RSS.GetBodyData(bodyData.itemCode);
            rootPlayer = GetComponentInParent<Player>();
        }
        else
        {
            bodyData = Core.RSS.GetEnemyBodyData(bodyData.itemCode);
            rootEnemy = GetComponentInParent<Enemy>();
        }

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
        Vector3 newPos = objTransformCenter.transform.position;
        newPos.z = transform.position.z;
        transform.position = newPos;

        if (Quaternion.Angle(transform.rotation, objTransformCenter.transform.rotation) > 0.01f)
            transform.rotation = Quaternion.Slerp(transform.rotation, objTransformCenter.transform.rotation, 360f * Time.deltaTime);

        if (eOwner == EOwner.AI)
        {

        }
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
