using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Core : MonoBehaviour
{
    public static Core instance = null;

    public List<HSSManager> managerList = new List<HSSManager>();

    private Dictionary<Type, HSSManager> dicManagers = new Dictionary<Type, HSSManager>();

    [HideInInspector]
    public static SceneLoadingManager LOADING { get { return instance.Get<SceneLoadingManager>(); } }

    [HideInInspector]
    public static InputManager INPUT { get { return instance.Get<InputManager>(); } }

    [HideInInspector]
    public bool isCoreReady = false;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Init()
    {
        StartCoroutine(CoreInit());
    }

    public IEnumerator CoreInit()
    {
        for (int i = 0; i < managerList.Count; i++)
        {
            yield return StartCoroutine(managerList[i].ManagerInitProcessing());

            var type = managerList[i].GetType();
            dicManagers.Add(type, managerList[i]);
        }

        isCoreReady = true;

        yield return StartCoroutine(LOADING.SceneLoadingWithAdditive("HUD"));

        Debug.Log("CoreInit()");

        if (!GameEngine.instance.isDev)
        {
            yield return StartCoroutine(LOADING.SceneUnLoad("Start"));

            yield return StartCoroutine(LOADING.SceneLoadingWithAsync("Title"));

            Debug.Log("Call Title");
        }


        // ���� ��ġ�� �̵��� �� ���� ( ��� ���� )
        //Helper.ChangeLanguageSetting(EGameLanuage.Korean);
    }

    // ª�� �˱� ���� ����ϱ� ���� ( �������� HSSManager )
    public T Get<T>() where T : HSSManager
    {
        var type = typeof(T);

        return dicManagers.ContainsKey(type) ? (T)dicManagers[type] : null;
    }
}
