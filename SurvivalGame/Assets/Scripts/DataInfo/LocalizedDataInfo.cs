using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "LocalizedDataInfo", menuName = "DataScript/LocalizedDataInfo", order = 5)]
public class LocalizedDataInfo : ScriptableObject
{
    [HideInInspector]
    public List<DoubleKeyData> dataList = new List<DoubleKeyData>();

    // 메인 Key - 메인 Value[언어(Key) - 단어(Value)]
    public Dictionary<string, Dictionary<string, string>> uiText = new Dictionary<string, Dictionary<string, string>>();

    [HideInInspector]
    public string currentLanguage;

    public IEnumerator InitData()
    {
        for (int i = 0; i < dataList.Count; i++)
        {
            string mainKey = dataList[i].mainKey;
            uiText[mainKey] = new Dictionary<string, string>();
            for (int j = 0; j < dataList[i].valueList.Count; j++)
            {
                uiText[mainKey].Add(dataList[i].valueList[j].key, dataList[i].valueList[j].value);
            }
        }

        yield return true;
    }

    public void SetCurrentLanguage(string language)
    {
        currentLanguage = language;
    }

    public void Save()
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }

    public bool ContainKey(string key)
    {
        for (int i = 0; i < dataList.Count; i++)
        {
            if (dataList[i].mainKey == key)
                return true;
        }

        return false;
    }

    public void AddData(string key, string language, string text)
    {
        bool hasKey = false;

        for(int i = 0; i < dataList.Count; i++)
        {
            if(dataList[i].mainKey == key)
            {
                dataList[i].Add(key, language, text);
                hasKey = true;
                break;
            }
        }

        if(!hasKey)
        {
            DoubleKeyData data = new DoubleKeyData();
            data.mainKey = key;
            data.Add(key, language, text);
            dataList.Add(data);
        }
    }

    public string GetData(string key, string language)
    {
        if (!ContainKey(key))
            return "";

        for (int i = 0; i < dataList.Count; i++)
        {
            if (dataList[i].mainKey == key)
            {
                if (!dataList[i].ContainsKey(language))
                    return "";

                return dataList[i].GetValue(language);
            }
        }

        return "";
    }

    public string GetUITextData(string key)
    {
        if(!uiText.ContainsKey(key))
            return "";

        return uiText[key][currentLanguage];
    }

    public void RemoveData(string key)
    {
        for (int i = 0; i < dataList.Count; i++)
        {
            if (dataList[i].mainKey == key)
            {
                dataList[i].RemoveData();
                dataList.RemoveAt(i);
            }
        }
    }
}

[System.Serializable]
public class KeyData
{
    public string key;
    public string value;
}

[System.Serializable]
public class DoubleKeyData
{
    public string mainKey;
    public List<KeyData> valueList = new List<KeyData>();

    public void Add(string key, string lan, string text)
    {
        mainKey = key;

        bool hasKey = false;
        for (int i = 0; i < valueList.Count; ++i)
        {
            if (valueList[i].key == lan)
            {
                valueList[i].value = text;
                hasKey = true;
                break;
            }
        }

        if (!hasKey)
        {
            KeyData data = new KeyData();
            data.key = lan;
            data.value = text;
            valueList.Add(data);
        }
    }

    public bool ContainsKey(string key)
    {
        for (int i = 0; i < valueList.Count; ++i)
        {
            if (valueList[i].key == key)
            {
                return true;
            }
        }

        return false;
    }

    public string GetValue(string key)
    {
        for (int i = 0; i < valueList.Count; ++i)
        {
            if (valueList[i].key == key)
            {
                return valueList[i].value;
            }
        }

        return "";
    }

    public void RemoveData()
    {
        valueList.Clear();
    }
}