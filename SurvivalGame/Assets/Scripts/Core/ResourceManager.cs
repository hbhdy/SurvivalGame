using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// 傈眉 府家胶 包府
public class ResourceManager : HSSManager
{
    public string imagePath = "";

    public override IEnumerator ManagerInitProcessing()
    {
        yield return StartCoroutine(InitManager());

        yield return StartCoroutine(base.ManagerInitProcessing());
    }

    public override IEnumerator InitManager()
    {
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
        return null;
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
