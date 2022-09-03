using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DataInfoLocalized : DataInfoBase
{
    // 메인 Key - 메인 Value[언어(Key) - 단어(Value)]
    public Dictionary<string, Dictionary<string, string>> dicText = new Dictionary<string, Dictionary<string, string>>();

    [HideInInspector]
    private string currentLanguage;

    public IEnumerator InitData()
    {
        EDataLoadResult type = Load();

        switch (type)
        {
            case EDataLoadResult.Complate:
                Debug.Log("DataInfoLocalized - Load Complate");
                break;
            case EDataLoadResult.Fail:
                Debug.Log("DataInfoLocalized - Load Fail");
                break;
            case EDataLoadResult.Skip:
                Debug.Log("DataInfoLocalized - Already have a key");
                break;
        }

        yield return true;
    }

    public EDataLoadResult Load()
    {
        TextData[] textData = UtilFunction.LoadJson<TextData>("DataInfoLocalized");

        if (textData == null)
            return EDataLoadResult.Fail;

        for (int i = 0; i < textData.Length; ++i)
        {
            if (dicText.ContainsKey(textData[i].mainKey) == false)
            {
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add(EGameLanuage.Korean.ToString(), textData[i].text_ko);
                data.Add(EGameLanuage.English.ToString(), textData[i].text_en);
                data.Add(EGameLanuage.Japanese.ToString(), textData[i].text_jp);
                dicText.Add(textData[i].mainKey, data);
            }
            else
                return EDataLoadResult.Skip;
        }

        return EDataLoadResult.Complate;
    }

    public void SetCurrentLanguage(string lang)
    {
        currentLanguage = lang;
    }

    public string GetUIText(string key)
    {
        if (dicText.ContainsKey(key) == false) {
            Debug.LogFormat("Not Found Text, key: {0}", key);
            return null;
        }

        return dicText[key][currentLanguage];
    }

    //public bool ContainKey(string key)
    //{
    //    for (int i = 0; i < dataList.Count; i++)
    //    {
    //        if (dataList[i].mainKey == key)
    //            return true;
    //    }

    //    return false;
    //}

    //public void AddData(string key, string language, string text)
    //{
    //    bool hasKey = false;

    //    for(int i = 0; i < dataList.Count; i++)
    //    {
    //        if(dataList[i].mainKey == key)
    //        {
    //            dataList[i].Add(key, language, text);
    //            hasKey = true;
    //            break;
    //        }
    //    }

    //    if(!hasKey)
    //    {
    //        DoubleKeyData data = new DoubleKeyData();
    //        data.mainKey = key;
    //        data.Add(key, language, text);
    //        dataList.Add(data);
    //    }
    //}

    //public string GetData(string key, string language)
    //{
    //    if (!ContainKey(key))
    //        return "";

    //    for (int i = 0; i < dataList.Count; i++)
    //    {
    //        if (dataList[i].mainKey == key)
    //        {
    //            if (!dataList[i].ContainsKey(language))
    //                return "";

    //            return dataList[i].GetValue(language);
    //        }
    //    }

    //    return "";
    //}

    //public string GetUITextData(string key)
    //{
    //    if(!uiText.ContainsKey(key))
    //        return "";

    //    return uiText[key][currentLanguage];
    //}

    //public void RemoveData(string key)
    //{
    //    for (int i = 0; i < dataList.Count; i++)
    //    {
    //        if (dataList[i].mainKey == key)
    //        {
    //            dataList[i].RemoveData();
    //            dataList.RemoveAt(i);
    //        }
    //    }
    //}
}

[System.Serializable]
public class TextData
{
    public string mainKey;
    public string text_ko;
    public string text_en;
    public string text_jp;
}

//[System.Serializable]
//public class KeyData
//{
//    public string key;
//    public string value;
//}

//[System.Serializable]
//public class DoubleKeyData
//{
//    public string mainKey;
//    public List<KeyData> valueList = new List<KeyData>();

//    public void Add(string key, string lan, string text)
//    {
//        mainKey = key;

//        bool hasKey = false;
//        for (int i = 0; i < valueList.Count; ++i)
//        {
//            if (valueList[i].key == lan)
//            {
//                valueList[i].value = text;
//                hasKey = true;
//                break;
//            }
//        }

//        if (!hasKey)
//        {
//            KeyData data = new KeyData();
//            data.key = lan;
//            data.value = text;
//            valueList.Add(data);
//        }
//    }

//    public bool ContainsKey(string key)
//    {
//        for (int i = 0; i < valueList.Count; ++i)
//        {
//            if (valueList[i].key == key)
//            {
//                return true;
//            }
//        }

//        return false;
//    }

//    public string GetValue(string key)
//    {
//        for (int i = 0; i < valueList.Count; ++i)
//        {
//            if (valueList[i].key == key)
//            {
//                return valueList[i].value;
//            }
//        }

//        return "";
//    }

//    public void RemoveData()
//    {
//        valueList.Clear();
//    }
//}