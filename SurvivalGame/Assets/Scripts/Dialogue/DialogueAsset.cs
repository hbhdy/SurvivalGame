using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

// 대사와 이펙트를 클래스로 분할후 각각 저장 (둘중 하나는 Null로 저장)
public class DialogueDataSet
{
    public EActionType eActionType;
    public Dialogue dialogue = new Dialogue();
    public EffectDialogue effectList = new EffectDialogue();
}

// 대사 
public class Dialogue
{
    public string key;
    public EDialogueType dialougeType;
    public ECharacterName name;

    public string[] textList = new string[4];

    public float startFadeTime;
    public float endFadeTime;

    public ECharacterName[] appearCharacterName = new ECharacterName[3];
    public bool appearCheck = false;
    public bool exitCheck = false;
    public bool[] posCheck = new bool[3];

    public bool characterHideOn = false;
}

// 이펙트 
public class EffectDialogue
{
    public EDetailType detailType;

    public EEndDelayType endDelayType;
    public float endDelayTime;

    public string strValue;
    public float floatValue;

    public bool fadeCheck;
}

[System.Serializable]
public class SpeechListData
{
    public List<Speech> speech_List = new List<Speech>();
    public string listTitle;

    public void Add()
    {
        Speech speech = new Speech();
        speech.key = "";
        speech.eCharacterName = ECharacterName.None;
        speech.c_num = 0;

        for (int i = 0; i < 4; i++)
        {
            speech.textList[i] = "";
        }

        speech_List.Add(speech);
    }

    public void AddEnd()
    {
        Speech speech = new Speech();
        speech.key = "End";
        speech.eCharacterName = ECharacterName.None;

        for (int i = 0; i < 4; i++)
        {
            speech.textList[i] = "";
        }

        speech_List.Add(speech);
    }
}

public class DialogueAsset : ScriptableObject
{
    public string key;
    public bool languageView;

    public SpeechListData speech_Lists = new SpeechListData();

    // 에디터 작업할때 초기 캐릭터 설정을 담당하는 변수
    public List<ECharacterName> start_C_set = new List<ECharacterName>();
    public string[] start_C_Array;

    public string[] languageName = new string[4];

    // 키값에 따른 해당 에셋의 Dialogue를 구성하는 모든 데이터
    public Dictionary<string, List<DialogueDataSet>> assetData = new Dictionary<string, List<DialogueDataSet>>();

    // 키에 따른 해당 에셋의 데이터를 클래스에 저장하기 위함 ( 대화마다 키를 사용해 뽑아서 사용 )
    public void SpeechDataConversion()
    {
        assetData.Clear();

        List<DialogueDataSet> tempDataList = new List<DialogueDataSet>();
        DialogueDataSet tempSet = new DialogueDataSet();

        // speech 데이터를 tempSet을 통해서 필요한 부분만 저장해 tempDataList에 추가한다.   
        // Speech_List의 actionType을 통해 분류
        for (int j = 0; j < speech_Lists.speech_List.Count; j++)
        {
            if (speech_Lists.speech_List[j].eActionType == EActionType.Dialogue)
            {
                tempSet.eActionType = speech_Lists.speech_List[j].eActionType;
                tempSet.dialogue.key = speech_Lists.speech_List[j].key;
                tempSet.dialogue.dialougeType = speech_Lists.speech_List[j].eDialogueType;
                tempSet.dialogue.name = speech_Lists.speech_List[j].eCharacterName;
                tempSet.dialogue.appearCheck = speech_Lists.speech_List[j].isAppearCheck;
                tempSet.dialogue.exitCheck = speech_Lists.speech_List[j].isExitCheck;
                tempSet.dialogue.characterHideOn = speech_Lists.speech_List[j].isCharacterHideOn;

                for (int a = 0; a < 3; a++)
                {
                    tempSet.dialogue.appearCharacterName[a] = speech_Lists.speech_List[j].eAppearCharacterName[a];
                    tempSet.dialogue.posCheck[a] = speech_Lists.speech_List[j].isPosCheck[a];
                }

                for (int b = 0; b < (int)EGameLanuage.Count; b++)
                {
                    tempSet.dialogue.textList[b] = speech_Lists.speech_List[j].textList[b];
                }

                tempSet.effectList = null;
                tempDataList.Add(tempSet);
                tempSet = new DialogueDataSet();
            }
            else
            {
                tempSet.eActionType = speech_Lists.speech_List[j].eActionType;
                tempSet.effectList.detailType = speech_Lists.speech_List[j].eDetailType;

                tempSet.effectList.endDelayType = speech_Lists.speech_List[j].eEndDelayType;
                tempSet.effectList.endDelayTime = speech_Lists.speech_List[j].endDeleyTime;
                tempSet.effectList.fadeCheck = speech_Lists.speech_List[j].isFadeCheck;

                tempSet.effectList.strValue = speech_Lists.speech_List[j].strValue;
                tempSet.effectList.floatValue = speech_Lists.speech_List[j].floatValue;

                tempSet.dialogue = null;
                tempDataList.Add(tempSet);
                tempSet = new DialogueDataSet();
            }
        }

        string currentKey = "";

        // Dictionary에 넣기 위해 키값에 따라 분류, 빈 리스트 생성
        List<DialogueDataSet> tempList = new List<DialogueDataSet>();
        for (int i = 0; i < tempDataList.Count; i++)
        {
            if (tempDataList[i].dialogue != null)
            {
                // 키값이 발견될 때
                if (tempDataList[i].dialogue.key != "" && tempDataList[i].dialogue.key != null)
                {
                    // 첫 번째 행이 아닐 때
                    if (i != 0)
                    {
                        assetData.Add(currentKey, tempList);
                        currentKey = tempDataList[i].dialogue.key;
                        tempList = new List<DialogueDataSet>();
                        tempList.Add(tempDataList[i]);
                    }
                    // 첫 번째 행일 때
                    else
                    {
                        currentKey = tempDataList[i].dialogue.key;
                        tempList.Add(tempDataList[i]);
                    }
                }
                else
                {
                    tempList.Add(tempDataList[i]);
                }
            }
            else if (tempDataList[i].dialogue == null)
            {
                tempList.Add(tempDataList[i]);
            }

            // 마지막 키값과 데이터를 넣어주기 위함
            if (i == tempDataList.Count - 1)
            {
                break;
            }
        }
    }

#if UNITY_EDITOR
    public void Save()
    {
        EditorUtility.SetDirty(this);
    }

    public void AddData()
    {
        SpeechListData data = new SpeechListData();

        data.listTitle = "해당 타이틀입니다. (원하는 내용을 적으시면 됩니다)";
        data.Add();
        speech_Lists = data;
    }

    [MenuItem("Assets/Create/DialogueAsset")]
    public static void CreateMyAsset()
    {
        DialogueAsset asset = CreateInstance<DialogueAsset>();
        AssetDatabase.CreateAsset(asset, "Assets/NewDialogueAsset.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
#endif
}
