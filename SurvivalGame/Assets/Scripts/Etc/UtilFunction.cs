using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.Linq;
using System.IO;
using UnityEngine.SceneManagement;

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
        }

        LocalizedText[] text = FindObjectInScene<LocalizedText>();

        for (int i = 0; i < text.Length; ++i)
        {
            text[i].UpdateLanuageSetting();
        }
    }

    public static Coroutine RemainTimeTimer(MonoBehaviour mono, Text objText, DateTime endTime, string format = "{0}", Action timeOverCall = null, Action closeCall = null, Action changeDayCall = null)
    {
        if (mono == null || objText == null)
            return null;

        return mono.StartCoroutine(CalculateRemainTime(objText, endTime, format, timeOverCall, closeCall, changeDayCall));
    }

    public static Coroutine RemainTimeTimer(MonoBehaviour mono, Text objText, long endTime, string format = "{0}", Action timeOverCall = null, Action closeCall = null, Action changeDayCall = null)
    {
        if (mono == null || objText == null)
            return null;

        var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        DateTime endDateTime = dtDateTime.AddMilliseconds(endTime).ToLocalTime();

        return mono.StartCoroutine(CalculateRemainTime(objText, endDateTime, format, timeOverCall, closeCall, changeDayCall));
    }

    static IEnumerator CalculateRemainTime(Text objText, DateTime endTime, string format, Action timeOverCall = null, Action closeCall = null, Action changeDayCall = null)
    {
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(1f);

        string remainTimeText = "";

        int days = (endTime - DateTime.UtcNow).Days;

        while (objText.gameObject.activeInHierarchy == true)
        {
            TimeSpan remainTime = endTime - DateTime.UtcNow;
            if (days > remainTime.Days)
            {
                days = remainTime.Days;
                changeDayCall?.Invoke();
            }

            if (remainTime.TotalSeconds < 0)
            {
                remainTimeText = string.Format("{0:D2}:{1:D2}:{2:D2}", 0, 0, 0);
                objText.text = string.Format(format, remainTimeText);

                timeOverCall?.Invoke();
                break;
            }

            if (remainTime.TotalDays >= 1)
            {
                remainTimeText = string.Format("{0}D, {1}H", Mathf.Abs(remainTime.Days), Mathf.Abs(remainTime.Hours));
                objText.text = string.Format(format, remainTimeText);
            }
            else
            {
                remainTimeText = string.Format("{0:D2}:{1:D2}:{2:D2}", remainTime.Hours, remainTime.Minutes, remainTime.Seconds);
                objText.text = string.Format(format, remainTimeText);
            }

            yield return wait;
        }

        closeCall?.Invoke();
    }

    public static string ConvertAmountUnit(long amount)
    {
        int m = 0;
        int k = 0;

        if (amount >= 10000)
        {
            while (amount >= 1000)
            {
                amount -= 1000;
                k++;

                if (k >= 1000)
                {
                    k -= 1000;
                    m++;
                }
            }
        }

        if (k > 0 || m > 0)
        {
            if (m > 0)
            {
                k = (int)(k / 100);

                if (k > 0)
                    return string.Format("{0}.{1}M", m, k);
                else
                    return string.Format("{0}M", m);
            }

            amount = (int)(amount / 100);

            if (amount > 0)
                return string.Format("{0}.{1}K", k, amount);
            else
                return string.Format("{0}K", k);
        }
        else
        {
            return string.Format("{0}", amount);
        }
    }

    public static string ConvertAmountUnit_Comma(long amount)
    {
        if (amount == 0)
        {
            return "0";
        }

        if (amount >= 10000000)
        {
            int m = 0;
            while (amount >= 1000000)
            {
                amount -= 1000000;
                m++;
            }
            return string.Format("{0}M", m);
        }

        return string.Format("{0:#,###}", amount);
    }

    public static void DeleteFile(string fileName)
    {
        try
        {
            string saveDirectory = Application.persistentDataPath;
            string filePath = Path.Combine(saveDirectory, fileName);

            if (File.Exists(filePath) == true)
                File.Delete(filePath);
        }
        catch (System.Exception ex)
        {
            //error
            Debug.LogWarningFormat("--- DeleteFile : {0}", ex);
        }
    }

    // 메모리 표시
    public static string GetReadableSize(double len)
    {
        int resultMultply = 1;

        if (len < 0)
        {
            len *= -1;
            resultMultply = -1;
        }

        string[] sizes = { "B", "KB", "MB", "GB" };
        int order = 0;

        while (len >= 1024 && order + 1 < sizes.Length)
        {
            order++;
            len = len / 1024;
        }

        return string.Format("{0:0.##} {1}", len * resultMultply, sizes[order]);
    }

    #region [ 오브젝트 탐색 ]
    public static string GetFullPath(Transform transform, string path=null)
    {
        Transform tr = transform;
        string fullPath;
        if (path != null && path.StartsWith("/"))
        {
            fullPath = path;
        }
        else
        {
            fullPath = string.Empty;
            while (tr != null)
            {
                if (string.IsNullOrEmpty(fullPath))
                    fullPath = tr.name;
                else
                    fullPath = string.Concat(tr.name, "/", fullPath);
                tr = tr.parent;
            }
            if (!string.IsNullOrEmpty(path))
            {
                fullPath += string.Concat("/", path);
            }
        }
        return fullPath;
    }

    public static GameObject Find(Transform transform, string path)
    {
        Transform tr = FindTransform(transform, path);

        return tr == null ? null : tr.gameObject;
    }

    public static T Find<T>(Transform transform) where T : Component
    {
        T component = transform.GetComponent<T>();

        if (component == null)
            Debug.LogWarningFormat("{0} not found : path={1}", typeof(T).Name, GetFullPath(transform, ""));

        return component;
    }

    public static T Find<T>(Transform transform, string path) where T : Component
    {
        Transform tr = FindTransform(transform, path);

        if (tr == null)
            return null;

        T component = tr.GetComponent<T>();

        if (component == null)
            Debug.LogWarningFormat("{0} not found : path={1}", typeof(T).Name, GetFullPath(transform, path));

        return component;
    }

    public static T Find<T>(this GameObject go, string path) where T : Component
    {
        return Find<T>(go.transform, path);
    }
    public static T Find<T>(Component component, string path) where T : Component
    {
        return Find<T>(component.transform, path);
    }

    public static GameObject Find(this GameObject obj, string path)
    {
        return Find(obj.transform, path);
    }

    public static GameObject Find(Component component, string path)
    {
        return Find(component.transform, path);
    }

    private static Transform FindTransform(Transform transform, string path)
    {
        Transform tr = transform.Find(path);

        if (tr == null)
        {
            Debug.LogWarningFormat("Child not found : Path={0}", GetFullPath(transform, path));
            return null;
        }

        return tr;
    }

    public static GameObject FindChild(GameObject obj, string strName)
    {
        if (null == obj)
        {
            return null;
        }

        if (obj.name == strName)
        {
            return obj;
        }

        Transform tranform = obj.transform.Find(strName);
        if (tranform != null)
            return tranform.gameObject;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
                continue;

            GameObject go = FindChild(child.gameObject, strName);
            if (go != null)
                return go;
        }

        return null;
    }
    #endregion
}
