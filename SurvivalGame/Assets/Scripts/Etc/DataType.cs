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