using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EOwner
{
    Player,
    AI
}

public enum EBarrageType
{
    Straight, // 직선
    Angle, // 방향 연발
    Shot, // 산탄
    Tornado, // 회전
    Custom,  // 커스텀

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

[System.Serializable]
public class SpawnerInfo
{
    public GameObject objPoint;
    public float percent = 100.0f;
    public float spawnTimer = 10.0f;
    public bool isLinked = false;
    public GameObject objLinkedObject = null;

    public bool isFold = true;

    public bool canRecycle = true;
    public bool canActive = false;
    public bool isFirst = true;
    public float addTimer = 0.0f;
    public bool isUsed = false;

}

[System.Serializable]
public class SpawnKeyInfo
{
    public string spawnKey;
    public bool isRelativeSpwaner = false;
    public GameObject objRelativeSpawner;
    public bool isFold = true;
}