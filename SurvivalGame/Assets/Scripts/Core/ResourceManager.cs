using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// ��ü ���ҽ� ����
public class ResourceManager : HSSManager
{
    private DataInfoWeapon DataInfoWeapon { get; set; } = new DataInfoWeapon();
    private DataInfoBody DataInfoBody { get; set; } = new DataInfoBody();
    private DataInfoWheel DataInfoWheel { get; set; } = new DataInfoWheel();

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
        yield return StartCoroutine(DataInfoWeapon.InitData());
        //yield return StartCoroutine(DataInfoBody.InitData());
        yield return StartCoroutine(DataInfoWheel.InitData());

        //yield return StartCoroutine(localDataInfo.InitData());
        //yield return StartCoroutine(barrageData.InitData());

        yield return StartCoroutine(base.InitManager());

        //EDataLoadResult type = DataInfoWheel.Load();

        //Debug.Log("EDataLoadResult : " + type);

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

    public WeaponData GetWeaponData(string key)
    {
        return DataInfoWeapon.GetWeaponData(key);
    }

    public BodyData GetBodyData(string key)
    {
        return DataInfoBody.GetBodyData(key);
    }

    public WheelData GetWheelData(string key)
    {
        return DataInfoWheel.GetWheelData(key);
    }

    public GameObject GetWeaponObject(string key)
    {
        return DataInfoWeapon.GetPrefabData(key);
    }

    public GameObject GetBodyObject(string key)
    {
        return DataInfoBody.GetPrefabData(key);
    }

    public GameObject GetWheelObject(string key)
    {
        return DataInfoWheel.GetPrefabData(key);
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
