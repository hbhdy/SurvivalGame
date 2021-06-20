using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPState : MonoBehaviour
{
    public Image uiPlayerHp;

    [HideInInspector]
    public Body body;

    private int bodyHp;

    public void LinkBody(Body playerBody)
    {
        body = playerBody;

        bodyHp = (int)body.entityStatus.useHP;
    }

    public void CalcHPState()
    {
        if (body == null)
            return;

        uiPlayerHp.fillAmount = body.GetBodyHPRate();
    }
}
