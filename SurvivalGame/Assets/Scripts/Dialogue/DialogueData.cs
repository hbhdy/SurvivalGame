using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �����Ҷ� ���̾�α� �����͸� ���¸��� ó���� ���� �����ϴ� Ŭ����
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

        Debug.LogError("���̾�α� Ű�� ���ų� �߸� �ԷµǾ����ϴ�.");
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
