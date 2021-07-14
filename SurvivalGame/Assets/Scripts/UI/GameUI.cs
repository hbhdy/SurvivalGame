using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI instance = null;
    public HUDManager HUD;

    [HideInInspector]
    public HUDPack[] hudPacks;

    public PlayerHPState playerHPState;

    public GameObject objBossHpFrame;
    public GameObject objWarning;

    public Image uiBossHpGage;
    public Image uiFadeEffect;

    public Text uiStageText;

    [HideInInspector]
    public bool isPause = false;

    public void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void Update()
    {
        if (isPause)
            return;

        if (!Core.instance.isCoreReady)
            return;

        if (Core.INPUT.isPressed)
        {
            Joystick.instance.OnPointerDown(Core.INPUT.inputPosition);
        }

        if(Core.INPUT.isDrag)
        {
            Joystick.instance.OnDrag(Core.INPUT.inputPosition);
        }

        if (Core.INPUT.isRelease)
        {
            Joystick.instance.OnPointerUp(Core.INPUT.inputPosition);
        }
    }

    public void FadeEffectRoutine()
    {
        StartCoroutine(FadeEffectCoroutine());
    }

    public IEnumerator FadeEffectCoroutine()
    {
        uiStageText.text = "Stage_" + (SpawnManager.instance.nowStageCount + 1).ToString();
        objWarning.SetActive(true);
        float fadeCount = 0;
        uiFadeEffect.color = new Color(0, 0, 0, 0);

        while (fadeCount < 1f)
        {
            uiFadeEffect.color = new Color(0, 0, 0, fadeCount);
            fadeCount += 0.01f;
            yield return null;
        }

        uiFadeEffect.color = new Color(0, 0, 0, 1);
        fadeCount = 1;

        while (fadeCount > 0)
        {
            uiFadeEffect.color = new Color(0, 0, 0, fadeCount);
            fadeCount -= 0.01f;
            yield return null;
        }
        uiFadeEffect.color = new Color(0, 0, 0, 0);
        objWarning.SetActive(false);
    }

    public void HudsOnOff(bool set)
    {
        if (!set)
        {
            hudPacks = HUD.GetComponentsInChildren<HUDPack>(true);

            for (int i = 0; i < hudPacks.Length; i++)
            {
                if (hudPacks[i].gameObject != null)
                    hudPacks[i].gameObject.SetActive(false);
            }
        }
        else
        {
            hudPacks = HUD.GetComponentsInChildren<HUDPack>(true);

            for (int i = 0; i < hudPacks.Length; i++)
                hudPacks[i].gameObject.SetActive(true);
        }
    }

    public void SetPause(bool set)
    {
        isPause = set;
    }
}
