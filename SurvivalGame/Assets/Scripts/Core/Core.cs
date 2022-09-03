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
    public static ResourceManager RSS { get { return instance.Get<ResourceManager>(); } }

    [HideInInspector]
    public static SceneLoadingManager LOADING { get { return instance.Get<SceneLoadingManager>(); } }

    [HideInInspector]
    public static InputManager INPUT { get { return instance.Get<InputManager>(); } }

    [HideInInspector]
    public static UIManager UI { get { return instance.Get<UIManager>(); } }

    [HideInInspector]
    public bool isCoreReady = false;

    // 짧고 알기 쉽게 명명하기 위함 ( 상속 조건 HSSManager )
    public T Get<T>() where T : HSSManager
    {
        var type = typeof(T);

        return dicManagers.ContainsKey(type) ? (T)dicManagers[type] : null;
    }

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
            yield return StartCoroutine(LOADING.SceneLoadingWithAsync("Lobby"));

            Debug.Log("Call Title");
        }

        UtilFunction.ChangeLanguageSetting(EGameLanuage.Korean);
    }
}
