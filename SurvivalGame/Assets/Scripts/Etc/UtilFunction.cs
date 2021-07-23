using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public static class UtilFunction
{
    public static int CalcDamage(float damage, float defence, bool critical = false)
    {
        if (damage <= 0)
            damage = 0;

        float result = damage;

        result = result - defence;

        if (result <= 0)
            result = 0;

        return (int)result == 0 ? 1 : (int)result;
    }

    // 씬 내 해당 클래스 오브젝트 탐색 ( T )
    public static T[] FindObjectInScene<T>()
    {
        List<T> objList = new List<T>();
        Scene activeScene = SceneManager.GetActiveScene();
        GameObject[] objs = activeScene.GetRootGameObjects();

        for(int i = 0; i < objs.Length; i++)
        {
            T[] obj = objs[i].GetComponentsInChildren<T>(true);
            for(int j = 0; j < obj.Length; j++)
            {
                objList.Add(obj[j]);
            }
        }

        return objList.ToArray();
    }

    public static void ChangeLanguageSetting(EGameLanuage lang)
    {
        switch(lang)
        {
            case EGameLanuage.Korean:
                Core.RSS.SetCurrentLanguage(EGameLanuage.Korean);
                break;
            case EGameLanuage.English:
                Core.RSS.SetCurrentLanguage(EGameLanuage.English);
                break;
            case EGameLanuage.Japanese:
                Core.RSS.SetCurrentLanguage(EGameLanuage.Japanese);
                break;
            case EGameLanuage.Russian:
                Core.RSS.SetCurrentLanguage(EGameLanuage.Russian);
                break;
        }

        LocalizedText[] text = FindObjectInScene<LocalizedText>();

        for (int i = 0; i < text.Length; ++i)
        {
            text[i].UpdateLanuageSetting();
        }
    }
}
