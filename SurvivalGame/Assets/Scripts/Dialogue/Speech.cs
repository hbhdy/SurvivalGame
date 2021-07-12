using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���̾�α� ������ (��� �� ��)
[System.Serializable]
public class Speech
{
    public string key;

    // ū Ÿ��Ʋ �� ���� Ÿ��Ʋ
    public EActionType eActionType;
    public EDetailType eDetailType;

    public EDialogueType eDialogueType;
    public EEndDelayType eEndDelayType;

    // ĳ����
    public int c_num;
    public ECharacterName eCharacterName;
    public bool isCharacterHideOn = false;

    // ���
    public string[] textList = new string[4];

    // ĳ���� ���� �� ���� ����
    public bool isAppearCheck = false;
    public bool isExitCheck = false;
    public int[] appear_num = new int[3];
    public bool[] isPosCheck = new bool[3];
    public ECharacterName[] eAppearCharacterName = new ECharacterName[3];

    // ����Ʈ ȿ�� �� ������ ����
    public float endDeleyTime;
    public bool isFadeCheck = false;

    // ���������� ����� ����
    public string strValue;
    public float floatValue;
}
