using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EUIType
{
    None,


    [UIAttrType("UI_CommonPopup")]
    UI_CommonPopup,
    [UIAttrType("UI_LevelUpPopup")]
    UI_LevelUpPopup,


    Count,
}

public class UIManager
{
    private static readonly UIManager _instance = new UIManager();

    public static UIManager Instance { get { return _instance; } }

    private Camera uiCamera = null;

    public Camera uiCam
    {
        get
        {
            if (uiCamera == null)
                uiCamera = GameObject.Find("UI Camera")?.GetComponent<Camera>();

            return uiCamera;
        }
    }

    private bool isInit = false;

}
