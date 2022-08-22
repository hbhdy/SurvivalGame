using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPState : MonoBehaviour
{
    public Image uiPlayerHp;
    public Image uiPlayerExp;

    [HideInInspector]
    public Body body;

    private int bodyHp;
    private int playerExp;

    public void LinkBody(Body playerBody)
    {
        body = playerBody;

        bodyHp = (int)body.entityStatus.useHP;
    }

    public void LinkExp()
    {
        playerExp = InGameCore.instance.playerSetting.GetExp();
    }

    public void CalcHPState()
    {
        if (body == null)
            return;

        uiPlayerHp.fillAmount = body.GetBodyHPRate();
    }

    public void CalcEXPState()
    {
        // ������ ���� ����ġ �о���� �߰� �ʿ� 
        uiPlayerExp.fillAmount = playerExp / 100;
    }
}
