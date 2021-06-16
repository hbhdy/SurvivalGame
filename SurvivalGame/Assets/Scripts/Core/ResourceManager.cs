using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// ��ü ���ҽ� ����
public class ResourceManager : HSSManager
{
    public WeaponDataInfo weaponDataInfo;
    public BodyDataInfo bodyDataInfo;
    public WheelDataInfo wheelDataInfo;

    public string imagePath = "";

    public override IEnumerator ManagerInitProcessing()
    {
        yield return StartCoroutine(InitManager());

        yield return StartCoroutine(base.ManagerInitProcessing());
    }

    public override IEnumerator InitManager()
    {
        // ���ҽ� �Ŵ��� �ʱ�ȭ�Ҷ� �ش� ���� �ʱ�ȭ �� Dictionary�� �Ҵ� ( Key���� ���� Ȱ���ϱ� ���� )
        yield return StartCoroutine(weaponDataInfo.InitData());
        yield return StartCoroutine(bodyDataInfo.InitData());
        yield return StartCoroutine(wheelDataInfo.InitData());

        yield return StartCoroutine(base.InitManager());
    }

    public GameObject LoadGameObject(string key)
    {
        return Resources.Load<GameObject>(key);
    }

    public GameObject GetWeaponObject(string key)
    {
        return weaponDataInfo.GetPrefabData(key);
    }

    public GameObject GetBodyObject(string key)
    {
        return bodyDataInfo.GetPrefabData(key);
    }

    public GameObject GetWheelObject(string key)
    {
        return wheelDataInfo.GetPrefabData(key);
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
