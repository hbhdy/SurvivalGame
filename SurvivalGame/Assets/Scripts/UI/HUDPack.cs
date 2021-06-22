using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDPack : MonoBehaviour
{
    public Image uiGage;

    public GameObject objHPBar;
    public string damageEffect;

    // 추적할 오브젝트
    private GameObject objSteering;

    public void SetGage(float gage)
    {
        if (uiGage != null)
            uiGage.fillAmount = gage;
    }

    // 추적할 오브젝트 설정
    public void SetSteeringTarget(GameObject obj)
    {
        objSteering = obj;
        Vector3 scPos = RectTransformUtility.WorldToScreenPoint(InGameCore.instance.playerCamera.GetComponent<Camera>(), objSteering.transform.position);
        Vector2 uiPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), scPos, InGameCore.instance.playerCamera.GetComponent<Camera>(), out uiPos);
        GetComponent<RectTransform>().anchoredPosition = uiPos;

        SetHPBar(true);
    }

    // 데미지 출력
    public void MakeDamageText(bool isPlayer, int damage)
    {
        Vector3 scPos = RectTransformUtility.WorldToScreenPoint(InGameCore.instance.playerCamera.GetComponent<Camera>(), objSteering.transform.position);
        Vector2 uiPos = Vector2.zero;

        if (isPlayer)
            uiPos = this.transform.position + new Vector3(0, 50, 0);
        else
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), scPos, InGameCore.instance.playerCamera.GetComponent<Camera>(), out uiPos);

        GameObject objText = HSSUIObjectPoolManager.instance.SpawnObject(damageEffect, uiPos, Quaternion.identity, this.transform);
        objText.GetComponent<DamageText>().SetText(damage);
    }

    // 플레이어 추적
    public void Following()
    {
        Vector3 scPos = RectTransformUtility.WorldToScreenPoint(InGameCore.instance.playerCamera.GetComponent<Camera>(), objSteering.transform.position);
        Vector2 uiPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), scPos, InGameCore.instance.playerCamera.GetComponent<Camera>(), out uiPos);
        GetComponent<RectTransform>().anchoredPosition = uiPos;
    }

    // 에너미 추적
    //public void Following()
    //{
    //    Vector3 scPos = RectTransformUtility.WorldToScreenPoint(InGameCore.instance.playerCamera.GetComponent<Camera>(), targetObj.transform.position);
    //    Vector2 uiPos = Vector2.zero;
    //    RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), scPos, InGameCore.instance.playerCamera.GetComponent<Camera>(), out uiPos);
    //    GetComponent<RectTransform>().anchoredPosition = uiPos;
    //}

    public void SetHPBar(bool set)
    {
        if (objHPBar != null)
            objHPBar.SetActive(set);
    }

    public void LateUpdate()
    {
        if (objSteering == null)
            return;

        Following();
    }
}
