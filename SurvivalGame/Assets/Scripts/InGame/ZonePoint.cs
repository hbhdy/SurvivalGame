using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SpawnPoint를 묶어서 관리함 ( 해당 스테이지마다 각 ZonePoint가 켜짐 )
public class ZonePoint : MonoBehaviour
{
    //[HideInInspector]
    public List<SpawnPoint> zoneLists = new List<SpawnPoint>();

    public bool isZoneClear = false;

    public void Start()
    {
        SpawnPoint[] zones = GetComponentsInChildren<SpawnPoint>(true);

        for (int i = 0; i < zones.Length; i++)
            zoneLists.Add(zones[i]);

        isZoneClear = false;
    }

    public void FixedUpdate()
    {
        if (!gameObject.activeSelf)
            return;

        if (isZoneClear)
            return;

        for(int i = 0; i < zoneLists.Count; i++)
        {
            if (!zoneLists[i].spawnClear)
                return;
        }

        isZoneClear = true;

        ZoneChanceRoutine();

        Debug.Log("해당 스테이지 클리어!!!");
    }

    public void ActiveZone()
    {
        gameObject.SetActive(true);
    }

    public void DeactiveZone()
    {
        gameObject.SetActive(false);
    }

    public void ZoneChanceRoutine()
    {
        Joystick.instance.SetJoystick(false);
        Joystick.instance.SetPause(true);
        Joystick.instance.OnPointerUp(Vector2.zero);

        PlayerSetting.instance.PlayerResetPosition();
        GameUI.instance.FadeEffectRoutine();
        SpawnManager.instance.NextStageRoutine();
    }
}
