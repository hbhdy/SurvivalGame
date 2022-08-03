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
    public Text gageText;

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
        gageText.text = string.Format("{0:0.00}%", gage * 100f);
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
