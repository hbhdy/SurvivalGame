using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : HSSManager
{
    private float currentProgress = 0.0f;
    private float targetProgress = 0.0f;

    public void SetTargetProgress(float target)
    {
        targetProgress = target;
    }

    public override IEnumerator ManagerInitProcessing()
    {
        yield return StartCoroutine(InitManager());

        yield return StartCoroutine(base.ManagerInitProcessing());
    }

    public override IEnumerator InitManager()
    {
        yield return StartCoroutine(base.InitManager());
    }

    public void StartTargetSceneSync()
    {
        StartCoroutine(SyncLoadingBar());
    }

    // SetTargetProgress(value)를 통한 로딩바 제어 ( 로딩상태에서 데이터 처리 및 연출을 위함 )
    private IEnumerator SyncLoadingBar()
    {
        currentProgress = 0.0f;

        SetTargetProgress(0.0f);

        while (currentProgress < 0.99f)
        {
            currentProgress = Mathf.MoveTowards(currentProgress, 1.0f, 0.01f);

            LoadingUI.instance.SetLoadingGage(currentProgress);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitUntil(() => InGameCore.instance.isInGameCoreReady);

        LoadingUI.instance.SetActiveLoadingUI(false);
    }

    // 씬 전환
    public IEnumerator SceneLoadingWithAsync(string targetScene)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Single);

        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            yield return null;
        }

        async.allowSceneActivation = true;

        while (!async.isDone)
        {
            yield return null;
        }

        yield return async;
    }

    // 씬 전환 및 로딩 표시
    public IEnumerator SceneLoadingWithAsyncUI(string targetScene)
    {
        LoadingUI.instance.SetActiveLoadingUI(true);

        AsyncOperation async = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Single);

        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            yield return null;
        }

        async.allowSceneActivation = true;

        StartTargetSceneSync();

        yield return async;
    }

    // 비동기 씬 추가
    public IEnumerator SceneLoadingWithAdditive(string targetScene)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Additive);

        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            yield return null;
        }

        async.allowSceneActivation = true;

        while (!async.isDone)
        {
            yield return null;
        }

        yield return async;
    }

    // 비동기 씬 종료
    public IEnumerator SceneUnLoad(string sceneName)
    {
        AsyncOperation async = SceneManager.UnloadSceneAsync(sceneName);

        while (!async.isDone)
        {
            yield return null;
        }

        yield return true;
    }
}
