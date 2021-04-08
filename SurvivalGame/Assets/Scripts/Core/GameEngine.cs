using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public static GameEngine instance = null;
    private Core gameCore;

    public int targetFrameRate = 60;

    [HideInInspector]
    public bool isDev = false;


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        StartCoroutine(EngineInit());
    }

    public IEnumerator EngineInit()
    {
        yield return StartCoroutine(DevCheck());

        yield return StartCoroutine(Init());
    }

    public IEnumerator DevCheck()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("GameEngine");

        if (obj != null)
        {
            if (obj != gameObject)
            {
                Destroy(gameObject);
                yield break;
            }
        }

        // ���� ��Ʈ�� �ƴ� ���� ������ ���� ( �ΰ��� �׽�Ʈ�� ������ )
        if (tag == "GameEngineDev")
            isDev = true;
        else
            isDev = false;

        yield return null;
    }

    public IEnumerator Init()
    {
        DontDestroyOnLoad(this);

        Application.targetFrameRate = targetFrameRate;

        gameCore = GetComponent<Core>();
        gameCore.Init();

        yield return null;
    }
}
