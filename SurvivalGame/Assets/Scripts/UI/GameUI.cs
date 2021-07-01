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

    public Image uiBossHpGage;

    public void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void Update()
    {
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
}
