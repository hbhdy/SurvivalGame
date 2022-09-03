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

    // �� ��Ȱ�� �ִ� ĵ������ Ȯ���� ���, Dictionary�� ó�� ���� 
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

    // enum���� ���� string ���� �� ��ġ�� �θ� ����
    private T LoadUI<T>(E_UI_Type ui_Type) where T : MonoBehaviour
    {
        GameObject objParent = UtilFunction.Find(mainPopup.gameObject, "SafeArea");

        return LoadUI<T>(UIAttrUtil.GetUIAttributeResourceName(ui_Type), objParent);
    }

    // ������ ������ ��ġ ó��, �ش� ������Ʈ ����
    private T LoadUI<T>(string ui_Name, GameObject parent) where T : MonoBehaviour
    {
        string path = string.Format("Prefabs/UI/{0}", ui_Name);

        GameObject obj = Resources.Load<GameObject>(path);

        GameObject popup = GameObject.Instantiate(obj);

        popup.transform.parent = parent.transform;

        // UI RectTransform �缳��( Ʋ���� ��ġ �� )
        UtilFunction.SetRectTransform(popup, obj);

        T findComponent = obj.GetComponent<T>();

        UIBase uiBase = findComponent as UIBase;

        if (uiBase != null)
            dicUI.Add(uiBase.e_UI_type, uiBase);

        return findComponent;
    }

    // ���÷������� private �޼��� ȣ��
    void _OpenUI(UIBase open_ui, params object[] param)
    {
        Type type = typeof(UIBase);
        System.Reflection.MethodInfo method = type.GetMethod("OpenUI", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        method.Invoke(open_ui, new object[] { param });
    }
}
