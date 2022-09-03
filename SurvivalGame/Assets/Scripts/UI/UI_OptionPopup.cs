using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_OptionPopup : UIBase
{
    public override E_UI_Type e_UI_type { get { return E_UI_Type.UI_OptionPopup; } }

    protected override void _OnAwake()
    {
        base._OnAwake();

        UtilFunction.Find<Button>(transform, "Content/Left").onClick.AddListener(OnClickGoLobbyScene);
        UtilFunction.Find<Button>(transform, "Content/Right").onClick.AddListener(_CloseUI);
    }

    protected override void _OpenUI(params object[] param)
    {
        InGameCore.instance.GamePause(InGameCore.E_Pause_Type.Pause_Type_Option, true);

        base._OpenUI(param);
    }

    protected override void _CloseUI()
    {
        InGameCore.instance.GamePause(InGameCore.E_Pause_Type.Pause_Type_Option, false);

        base._CloseUI();
    }

    private void OnClickGoLobbyScene()
    {
        InGameCore.instance.GamePause(InGameCore.E_Pause_Type.Pause_Type_Option, false);
        StartCoroutine(Core.LOADING.SceneLoadingWithAsync("Lobby"));
    }
}
