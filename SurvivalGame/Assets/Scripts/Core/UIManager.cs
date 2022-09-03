using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_UI_Type
{
    UI_Type_None,

    [UIAttrType("UI_OptionPopup")]
    UI_OptionPopup,

}

public class UIManager : HSSManager
{
    private Dictionary<E_UI_Type, UIBase> dicUI = new Dictionary<E_UI_Type, UIBase>();

    // 각 역활이 있는 캔버스로 확장할 경우, Dictionary로 처리 가능 
    private Canvas mainPopup = null;

    public override IEnumerator ManagerInitProcessing()
    {
        yield return StartCoroutine(InitManager());

        yield return StartCoroutine(base.ManagerInitProcessing());
    }

    public override IEnumerator InitManager()
    {
        yield return StartCoroutine(base.InitManager());
    }

    public void SetCanvas()
    {
        GameObject objCanvas = GameObject.Find("MainPopupPanel");

        mainPopup = objCanvas.GetComponent<Canvas>();
    }

    public void ResetDicUI()
    {
        dicUI.Clear();
    }

    public UIBase OpenUI(E_UI_Type ui_Type, params object[] param)
    {
        UIBase openUi = null;

        if (dicUI.ContainsKey(ui_Type) == true)
            openUi = dicUI[ui_Type];
        else
        {
            openUi = LoadUI<UIBase>(ui_Type);
        }

        if (openUi == null)
            return null;

        _OpenUI(openUi, param);

        return openUi;
    }

    // enum값에 따른 string 추출 및 위치할 부모 전달
    private T LoadUI<T>(E_UI_Type ui_Type) where T : MonoBehaviour
    {
        GameObject objParent = UtilFunction.Find(mainPopup.gameObject, "SafeArea");

        return LoadUI<T>(UIAttrUtil.GetUIAttributeResourceName(ui_Type), objParent);
    }

    // 프리팹 생성과 위치 처리, 해당 컴포넌트 리턴
    private T LoadUI<T>(string ui_Name, GameObject parent) where T : MonoBehaviour
    {
        string path = string.Format("Prefabs/UI/{0}", ui_Name);

        GameObject obj = Resources.Load<GameObject>(path);

        GameObject popup = GameObject.Instantiate(obj);

        popup.transform.parent = parent.transform;

        // UI RectTransform 재설정( 틀어진 위치 등 )
        UtilFunction.SetRectTransform(popup, obj);

        T findComponent = obj.GetComponent<T>();

        UIBase uiBase = findComponent as UIBase;

        if (uiBase != null)
            dicUI.Add(uiBase.e_UI_type, uiBase);

        return findComponent;
    }

    // 리플랙션으로 private 메서드 호출
    void _OpenUI(UIBase open_ui, params object[] param)
    {
        Type type = typeof(UIBase);
        System.Reflection.MethodInfo method = type.GetMethod("OpenUI", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        method.Invoke(open_ui, new object[] { param });
    }
}
