using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "WeaponDataInfo", menuName = "DataScript/WeaponDataInfo", order = 3)]
public class WeaponDataInfo : ScriptableObject
{
    public string prefabPath;

    public List<WeaponData> weaponDataList = new List<WeaponData>();

    public Dictionary<string, WeaponData> dicWeaponDataList = new Dictionary<string, WeaponData>();

    public IEnumerator InitData()
    {
        for (int i = 0; i < weaponDataList.Count; ++i)
        {
            dicWeaponDataList.Add(weaponDataList[i].itemCode, weaponDataList[i]);
        }

        yield return true;
    }

    public GameObject GetPrefabData(string key)  // itemcode
    {
        return dicWeaponDataList[key].objPrefab;
    }

    public WeaponData GetWeaponData(string key)  // itemcode
    {
        return dicWeaponDataList[key];
    }


    public void OnEnable()
    {
        EditorUtility.SetDirty(this);
    }

}

[System.Serializable]
public class WeaponData
{
    public string itemCode;

    public string prefabName;
    public GameObject objPrefab;

    public float attackDamage = 1;
    public float criChance = 0;
    public float criDamage = 150;

}
