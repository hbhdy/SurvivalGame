using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// 전체 리소스 관리
public class ResourceManager : HSSManager
{
    public BodyDataInfo bodyDataInfo;
    

    public string imagePath = "";

    public override IEnumerator ManagerInitProcessing()
    {
        yield return StartCoroutine(InitManager());

        yield return StartCoroutine(base.ManagerInitProcessing());
    }

    public override IEnumerator InitManager()
    {
        yield return StartCoroutine(bodyDataInfo.InitData());


        yield return StartCoroutine(base.InitManager());
    }

    public GameObject LoadGameObject(string key)
    {
        return Resources.Load<GameObject>(key);
    }

    public GameObject GetWeaponObject(string key)
    {
        return null;
    }

    public GameObject GetBodyObject(string key)
    {
        return bodyDataInfo.GetPrefabData(key);
    }

    public GameObject GetWheelObject(string key)
    {
        return null;
    }

    public Sprite GetUIImage(string key)
    {
        return Resources.Load<Sprite>(imagePath + key);
    }

    public AudioClip LoadAudioClip(string path, string key)
    {
        return Resources.Load<AudioClip>(path + key);
    }

    public AudioMixerGroup LoadAudioMixer(string path, string mixer, string key)
    {
        return Resources.Load<AudioMixer>(path + mixer).FindMatchingGroups(key)[0];
    }
}
