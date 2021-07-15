using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    public void InGameStartBtn()
    {
        StartCoroutine(Core.LOADING.SceneLoadingWithAsyncUI("Chapter_1"));
    }
}
