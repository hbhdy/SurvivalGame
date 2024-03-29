using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 인게임내 초기화 및 게임 흐름을 제어하는 부분
public class InGameCore : MonoBehaviour
{
    public enum E_Pause_Type
    {
        Pause_Type_Background,
        Pause_Type_Dialogue,
        Pause_Type_Option,

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

    // 로비에서 들어오므로 로딩 프로세스를 구축해야함
    public IEnumerator InitGameScene()
    {
        yield return new WaitUntil(() => Core.instance.isCoreReady);
        yield return new WaitForEndOfFrame();

        Core.UI.SetCanvas();

        // 씬 초기화하면서 오브젝트풀의 설정된 오브젝트 생성
        HSSObjectPoolManager.instance.MakeObjectPool();

        // 씬 초기화하면서 UI 오브젝트풀의 설정된 UI 생성
        HSSUIObjectPoolManager.instance.MakeObjectPool();

        yield return StartCoroutine(playerSetting.AssemblePlayerDev());

        playerCamera.SetTrackingTarget(playerSetting.player.objWheel);
        playerCamera.SetReady(true);

        yield return null;

        isInGameCoreReady = true;
    }

    // 인게임에서 실행하므로 별도의 로딩을 추가하지 않음
    public IEnumerator InitGameSceneDev()
    {
        yield return new WaitUntil(() => Core.instance.isCoreReady);
        yield return new WaitForEndOfFrame();

        Core.UI.SetCanvas();

        // 씬 초기화하면서 오브젝트풀의 설정된 오브젝트 생성
        HSSObjectPoolManager.instance.MakeObjectPool();
        // 씬 초기화하면서 UI 오브젝트풀의 설정된 UI 생성
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
            Time.timeScale = 0;
            Joystick.instance.SetPause(true);
            GameUI.instance.SetPause(true);
        }
        else
        {
            Time.timeScale = 1;
            Joystick.instance.SetPause(false);
            GameUI.instance.SetPause(false);
        }
    }
}
