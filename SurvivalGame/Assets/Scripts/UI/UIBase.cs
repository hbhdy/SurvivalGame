using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
    public abstract E_UI_Type e_UI_type { get; }


    private void Awake()
    {
        _OnAwake();
    }

    protected virtual void _OnAwake()
    {
    }

    void OpenUI(params object[] param)
    {
        _OpenUI(param);
    }

    protected virtual void _OpenUI(params object[] param)
    {
        this.gameObject.SetActive(true);
    }

    protected virtual void _CloseUI()
    {
        this.gameObject.SetActive(false);
    }
}
