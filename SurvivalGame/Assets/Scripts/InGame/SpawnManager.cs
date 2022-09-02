using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance = null;

    public int nowStageCount = 0;

    [HideInInspector]
    public bool isWaitCheck = false;

    private List<ZonePoint> zonePoints = new List<ZonePoint>();


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        ZonePoint[] zones = GetComponentsInChildren<ZonePoint>(true);

        for (int i = 0; i < zones.Length; i++)
            zonePoints.Add(zones[i]);
    }

    public void Start()
    {
        StartCoroutine(StarGameRoutine());
        //zonePoints[nowStageCount].ActiveZone();
    }

    public IEnumerator StarGameRoutine()
    {
        InGameCore.instance.GamePause(InGameCore.E_Pause_Type.Pause_Type_Dialogue, true);
        GameUI.instance.FadeEffectRoutine();

        yield return new WaitForSecondsRealtime(3.0f);

        InGameCore.instance.GamePause(InGameCore.E_Pause_Type.Pause_Type_Dialogue, false);
        zonePoints[nowStageCount].ActiveZone();

        //DialogueUI.instance.StartDialogue("Circle_Dialogue");
    }

    public void NextStageRoutine()
    {
        StartCoroutine(NextStageCoroutine());
    }

    public IEnumerator NextStageCoroutine()
    {
        isWaitCheck = true;
        if (zonePoints[nowStageCount].isZoneClear)
        {
            zonePoints[nowStageCount].DeactiveZone();

            nowStageCount++;
        }
        GameUI.instance.FadeEffectRoutine();

        yield return new WaitForSecondsRealtime(5.0f);

        nowStageCount++;
        zonePoints[nowStageCount].ActiveZone();

        isWaitCheck = false;

        Joystick.instance.SetPause(false);
        Joystick.instance.SetJoystick(true);
    }
}
