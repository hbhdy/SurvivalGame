using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DataInfoWeapon : DataInfoBase
{
    public string prefabPathPlayer = "Prefabs/Player/Weapon/";
    public string prefabPathEnemy = "Prefabs/Enemy/Weapon/";

    public Dictionary<string, WeaponData> dicWeaponDataList = new Dictionary<string, WeaponData>();

    public IEnumerator InitData()
    {
        EDataLoadResult type = Load();

        switch (type)
        {
            case EDataLoadResult.Complate:
                Debug.Log("DataInfoWeapon - Load Complate)");
                break;
            case EDataLoadResult.Fail:
                Debug.Log("DataInfoWeapon - Load Fail)");
                break;
            case EDataLoadResult.Skip:
                Debug.Log("DataInfoWeapon - Already have a key");
                break;
        }

        yield return true;
    }

    public EDataLoadResult Load()
    {
        WeaponData[] weaponData = UtilFunction.LoadJson<WeaponData>("DataInfoWeapon");

        if (weaponData == null)
            return EDataLoadResult.Fail;

        for (int i = 0; i < weaponData.Length; ++i)
        {
            if (weaponData[i].eOwner == EOwner.Player)
                weaponData[i].objPrefab = Resources.Load<GameObject>(prefabPathPlayer + weaponData[i].prefabName);
            //else
            //    weaponData[i].objPrefab = Resources.Load<GameObject>(prefabPathEnemy + weaponData[i].prefabName);

            if (weaponData[i].objPrefab == null)
                Debug.LogFormat("objPrefab Null -> Key: {0}", weaponData[i].prefabName);

            if (dicWeaponDataList.ContainsKey(weaponData[i].key) == false)
                dicWeaponDataList.Add(weaponData[i].key, weaponData[i]);
            else
                return EDataLoadResult.Skip;
        }

        return EDataLoadResult.Complate;      
    }

    public GameObject GetPrefabData(string key)  // itemcode
    {
        return dicWeaponDataList[key].objPrefab;
    }

    public WeaponData GetWeaponData(string key)  // itemcode
    {
        return dicWeaponDataList[key];
    }
}

[System.Serializable]
public class WeaponData
{
    public string key;
    public EOwner eOwner;
    public string prefabName;
    public string bulletKey;
    public float attackDamage = 1;
    public float criChance = 0;
    public float criDamage = 150;

    public GameObject objPrefab;
}
