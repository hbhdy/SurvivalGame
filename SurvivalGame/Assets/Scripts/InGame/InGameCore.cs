using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ΰ��ӳ� �ʱ�ȭ �� ���� �帧�� �����ϴ� �κ�
public class InGameCore : MonoBehaviour
{
    public enum E_Pause_Type
    {
        Pause_Type_Background,
        Pause_Type_Dialogue,

        Count,
    }

    public static InGameCore instance = null;

    public string currentStage = "";

    public PlayerSetting playerSetting;
    public CameraFollower playerCamera;

    [HideInInspector]
    public bool isInGameCoreReady = false;


    private Dictionary<E_Pause_Type, bool> dicPause = new Dictionary<E_Pause_Type, bool>();

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
        dicPause.Clear();

        for (int i = 0; i < (int)E_Pause_Type.Count; i++)
        {
            dicPause.Add((E_Pause_Type)i, false);
        }

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

        yield return StartCoroutine(playerSetting.AssemblePlayerDev());

        playerCamera.SetTrackingTarget(playerSetting.player.objWheel);
        playerCamera.SetReady(true);

        yield return null;

        isInGameCoreReady = true;
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

    public void GamePause(E_Pause_Type pauseType, bool pause)
    {
        dicPause[pauseType] = pause;

        bool lastPause = dicPause.ContainsValue(true);

        if (lastPause == true)
        {
            Time.timeScale = 1;
            Joystick.instance.SetPause(false);
            GameUI.instance.SetPause(false);
        }
        else
        {
            Time.timeScale = 0;
            Joystick.instance.SetPause(true);
            GameUI.instance.SetPause(true);
        }
    }
}
