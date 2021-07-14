using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ΰ��ӳ� �ʱ�ȭ �� ���� �帧�� �����ϴ� �κ�
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

    // �κ񿡼� �����Ƿ� �ε� ���μ����� �����ؾ���
    public IEnumerator InitGameScene()
    {
        yield return new WaitUntil(() => Core.instance.isCoreReady);
        yield return new WaitForEndOfFrame();

        // �� �ʱ�ȭ�ϸ鼭 ������ƮǮ�� ������ ������Ʈ ����
        HSSObjectPoolManager.instance.MakeObjectPool();
        // �� �ʱ�ȭ�ϸ鼭 UI ������ƮǮ�� ������ UI ����
        HSSUIObjectPoolManager.instance.MakeObjectPool();
        // ���� �ε� ���μ��� �۾� �ʿ�

        yield return null;
    }

    // �ΰ��ӿ��� �����ϹǷ� ������ �ε��� �߰����� ����
    public IEnumerator InitGameSceneDev()
    {
        yield return new WaitUntil(() => Core.instance.isCoreReady);
        yield return new WaitForEndOfFrame();

        // �� �ʱ�ȭ�ϸ鼭 ������ƮǮ�� ������ ������Ʈ ����
        HSSObjectPoolManager.instance.MakeObjectPool();
        // �� �ʱ�ȭ�ϸ鼭 UI ������ƮǮ�� ������ UI ����
        HSSUIObjectPoolManager.instance.MakeObjectPool();

        yield return StartCoroutine(playerSetting.AssemblePlayerDev());

        playerCamera.SetTrackingTarget(playerSetting.player.objWheel);
        playerCamera.SetReady(true);

        yield return null;

        isInGameCoreReady = true;
    }

    public void GamePause()
    {
        Joystick.instance.SetPause(true);
        GameUI.instance.SetPause(true);
        Time.timeScale = 0;
    }

    public void GameResume()
    {
        Joystick.instance.SetPause(false);
        GameUI.instance.SetPause(false);
        Time.timeScale = 1;
    }
}
