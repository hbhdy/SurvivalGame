using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EOwner
{
    Player,
    AI
}

[System.Serializable]
public class EntityStatus
{
    public int HP = 0;
    public int DEF = 0;

    public float useHP = 0;
}

[System.Serializable]
public class AssembleData
{
    public EquipData weaponData = new EquipData();
    public EquipData bodyData = new EquipData();
    public EquipData wheelData = new EquipData();
}

[System.Serializable]
public class EquipData
{
    public string key;
    public long id;
    public int level;
}