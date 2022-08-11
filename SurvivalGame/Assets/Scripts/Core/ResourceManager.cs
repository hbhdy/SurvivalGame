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

    public WeaponDataInfo enemyWeaponDataInfo;
    public BodyDataInfo enemyBodyDataInfo;
    public WheelDataInfo enemyWheelDataInfo;

    public LocalizedDataInfo localDataInfo;

    public BarrageData barrageData;

    // ���̾�α״� ������ �������� �� ���� ( ���� ���� )
    public DialogueData dialogueData = new DialogueData();

    // ���̾�α� ���� �̸� ( ��ũ )
    public List<string> dialogueNames = new List<string>();

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

        yield return StartCoroutine(enemyWeaponDataInfo.InitData());
        yield return StartCoroutine(enemyBodyDataInfo.InitData());
        yield return StartCoroutine(enemyWheelDataInfo.InitData());

        yield return StartCoroutine(localDataInfo.InitData());

        yield return StartCoroutine(barrageData.InitData());

        yield return StartCoroutine(base.InitManager());


        EDataLoadResult type = wheelDataInfo.Load();

        Debug.Log("EDataLoadResult : " + type);

        dialogueData.LoadDialouge(dialogueNames);
    }

    public void SetCurrentLanguage(EGameLanuage lang)
    {
        localDataInfo.SetCurrentLanguage(lang.ToString());
        PlayerPrefs.SetString("Language",lang.ToString());
    }

    public string GetLocalUIText(string key)
    {
        return localDataInfo.GetUITextData(key);
    }

    public List<DialogueDataSet> GetDialogueKeyData(string key)
    {
        return dialogueData.GetDialogueKeyData(key);
    }

    public Dictionary<string, NameTextData> GetNameText()
    {
        dialogueData.GetNameData();
        return dialogueData.dialogueName;
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
