using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 다이얼로그 데이터 (대사 한 턴)
[System.Serializable]
public class Speech
{
    public string key;

    // 큰 타이틀 및 세부 타이틀
    public EActionType eActionType;
    public EDetailType eDetailType;

    public EDialogueType eDialogueType;
    public EEndDelayType eEndDelayType;

    // 캐릭터
    public int c_num;
    public ECharacterName eCharacterName;
    public bool isCharacterHideOn = false;

    // 대사
    public string[] textList = new string[4];

    // 캐릭터 등장 및 퇴장 관련
    public bool isAppearCheck = false;
    public bool isExitCheck = false;
    public int[] appear_num = new int[3];
    public bool[] isPosCheck = new bool[3];
    public ECharacterName[] eAppearCharacterName = new ECharacterName[3];

    // 이펙트 효과 및 딜레이 관련
    public float endDeleyTime;
    public bool isFadeCheck = false;

    // 범용적으로 사용할 변수
    public string strValue;
    public float floatValue;
}
