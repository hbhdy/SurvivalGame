using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCore : MonoBehaviour
{
    public static InGameCore instance = null;

    public string currentStage = "";

    public PlayerSetting playerSetting;
    public CameraFollower playerCamera;

    [HideInInspector]
    public bool isInGameCoreReady = false;


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        isInGameCoreReady = false;
    }

    public void Start()
    {
        if (GameEngine.instance.isDev)
            StartCoroutine(InitGameSceneDev());
        else
            StartCoroutine(InitGameScene());
    }

    public IEnumerator InitGameScene()
    {
        yield return new WaitUntil(() => Core.instance.isCoreReady);
        yield return new WaitForEndOfFrame();

        // �� �ʱ�ȭ�ϸ鼭 ������ƮǮ�� ������ ������Ʈ ����
        HSSObjectPoolManager.instance.MakeObjectPool();

        yield return null;
    }

    public IEnumerator InitGameSceneDev()
    {
        yield return new WaitUntil(() => Core.instance.isCoreReady);
        yield return new WaitForEndOfFrame();

        // �� �ʱ�ȭ�ϸ鼭 ������ƮǮ�� ������ ������Ʈ ����
        HSSObjectPoolManager.instance.MakeObjectPool();

        yield return StartCoroutine(playerSetting.AssemblePlayerDev());

        playerCamera.SetTrackingTarget(playerSetting.player.objWheel);
        playerCamera.SetReady(true);

        yield return null;
    }
}
