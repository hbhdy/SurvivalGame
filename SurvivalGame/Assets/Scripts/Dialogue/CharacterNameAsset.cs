using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NameTextData
{
    public string editorName;
    public string[] viewName = new string[(int)EGameLanuage.Count];
}

public class CharacterNameAsset : ScriptableObject
{
    [HideInInspector]
    public List<NameTextData> nameData = new List<NameTextData>();

    public List<NameTextData> GetNameData()
    {
        return nameData;
    }
}
