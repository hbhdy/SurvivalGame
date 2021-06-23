using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// 전체 리소스 관리
public class ResourceManager : HSSManager
{
    public WeaponDataInfo weaponDataInfo;
    public BodyDataInfo bodyDataInfo;
    public WheelDataInfo wheelDataInfo;

    public WeaponDataInfo enemyWeaponDataInfo;
    public BodyDataInfo enemyBodyDataInfo;
    public WheelDataInfo enemyWheelDataInfo;

    public BarrageData barrageData;

    public string imagePath = "";

    public override IEnumerator ManagerInitProcessing()
    {
        yield return StartCoroutine(InitManager());

        yield return StartCoroutine(base.ManagerInitProcessing());
    }

    public override IEnumerator InitManager()
    {
        // 리소스 매니저 초기화할때 해당 에셋 초기화 및 Dictionary에 할당 ( Key값에 따라 활용하기 위함 )
        yield return StartCoroutine(weaponDataInfo.InitData());
        yield return StartCoroutine(bodyDataInfo.InitData());
        yield return StartCoroutine(wheelDataInfo.InitData());

        yield return StartCoroutine(enemyWeaponDataInfo.InitData());
        yield return StartCoroutine(enemyBodyDataInfo.InitData());
        yield return StartCoroutine(enemyWheelDataInfo.InitData());

        yield return StartCoroutine(barrageData.InitData());

        yield return StartCoroutine(base.InitManager());
    }

    public GameObject LoadGameObject(string key)
    {
        return Resources.Load<GameObject>(key);
    }

    public Barrage GetBarrageData(string key)
    {
        return barrageData.GetBarrageData(key);
    }

    // Enemy------------------------------------------------------------------------------------
    public WeaponData GetEnemyWeaponData(string key)
    {
        return enemyWeaponDataInfo.GetWeaponData(key);
    }

    public BodyData GetEnemyBodyData(string key)
    {
        return enemyBodyDataInfo.GetBodyData(key);
    }

    public WheelData GetEnemyWheelData(string key)
    {
        return enemyWheelDataInfo.GetWheelData(key);
    }

    // Player------------------------------------------------------------------------------------

    public WeaponData GetWeaponData(string key)
    {
        return weaponDataInfo.GetWeaponData(key);
    }

    public BodyData GetBodyData(string key)
    {
        return bodyDataInfo.GetBodyData(key);
    }

    public WheelData GetWheelData(string key)
    {
        return wheelDataInfo.GetWheelData(key);
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
