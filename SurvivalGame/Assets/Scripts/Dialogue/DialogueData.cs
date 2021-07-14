using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 시작할때 다이얼로그 데이터를 에셋마다 처리후 통합 관리하는 클래스
public class DialogueData
{
    public List<DialogueAsset> dialogueAsset = new List<DialogueAsset>();

    public CharacterNameAsset nameAsset;

    public Dictionary<string, NameTextData> dialogueName = new Dictionary<string, NameTextData>();

    public void LoadDialouge(List<string> names)
    {
        dialogueAsset.Clear();

        for (int i = 0; i < names.Count; ++i)
        {
            dialogueAsset.Add(Resources.Load<DialogueAsset>("ScriptableObject/DialogueAsset"));
        }

        nameAsset = Resources.Load<CharacterNameAsset>("ScriptableObject/CharaterNameText");

        for (int i = 0; i < dialogueAsset.Count; ++i)
        {
            dialogueAsset[i].SpeechDataConversion();
        }
    }

    public List<DialogueDataSet> GetDialogueKeyData(string key)
    {
        for (int i = 0; i < dialogueAsset.Count; ++i)
        {
            for (int j = 0; j < dialogueAsset[i].assetData.Count; ++j)
            {
                if (dialogueAsset[i].assetData.ContainsKey(key))
                    return dialogueAsset[i].assetData[key];
            }
        }

        Debug.LogError("다이얼로그 키가 없거나 잘못 입력되었습니다.");
        return null;
    }

    public Dictionary<string, NameTextData> GetNameData()
    {
        dialogueName.Clear();

        List<NameTextData> nameData = new List<NameTextData>();

        nameData = nameAsset.GetNameData();

        for (int i = 0; i < nameData.Count; i++)
        {
            dialogueName.Add(nameData[i].editorName, nameData[i]);
        }

        return dialogueName;
    }
}
