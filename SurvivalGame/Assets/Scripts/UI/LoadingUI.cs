using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    public static LoadingUI instance = null;

    public GameObject objBack;
    public GameObject objBar;
    public Image uiLoadingGage;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);

        Init();
    }

    public void Init()
    {
        SetLoadingGage(0);
    }

    public void SetLoadingGage(float gage)
    {
        uiLoadingGage.fillAmount = gage;
    }

    public void SetActiveLoadingUI(bool set)
    {
        if(set)
        {
            uiLoadingGage.fillAmount = 0;
        }

        objBack.SetActive(set);
        objBar.SetActive(set);
    }
}
