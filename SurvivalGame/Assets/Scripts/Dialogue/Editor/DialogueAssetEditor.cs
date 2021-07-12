using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(DialogueAsset))]
public class DialogueAssetEditor : Editor
{
    DialogueAsset CurTarget = null;
    Speech curSpeech;

    public int viewNumber = 0;

    public string CurKey;
    public int CurIndex;        // 선택한 키의 처음 인덱스 위치
    public int LastIndex;       // 선택한 키의 마지막 인덱스 위치
    public int EndIndex;        // Asset의 키값이 End인 인덱스 위치

    private void OnEnable()
    {
        CurTarget = (DialogueAsset)target;

        CurTarget.languageName[0] = "Korean";
        CurTarget.languageName[1] = "English";
        CurTarget.languageName[2] = "Japanese";
        CurTarget.languageName[3] = "Russian";
        NameSwapArray();
    }

    private void OnDisable()
    {
        EditorUtility.SetDirty(CurTarget);
    }

    public override void OnInspectorGUI()
    {
        DialogueListAddRemove();        // SpeechListData로 이루어진 List를 관리함

        GUILayout.Space(10);

        CharacterAppearSet();           // Asset에 사용할 캐릭터를 미리 선별해서 캐릭터 팝업리스트에 사용함 (캐릭터가 수십개가 넘어가면 효율적)

        GUILayout.Space(10);

        EndIndexCheck();                // 해당 List의 최대개수를 파악함( End Key가 없을때 추가 )
        KeyListSetting();               // Speech로 이루어진 List에서 Key값으로 단락을 나누어 보여줌 (버튼 클릭 -> 단락 내용 출력) 

        GUILayout.Space(10);

        DrawEdittingMode();             // 실제 에디터를 활용하는 부분 ( 대화 및 연출을 설정 )
    }

    // SpeechListData로 이루어진 List를 관리함
    public void DialogueListAddRemove()
    {
        if (CurTarget.speech_Lists == null)
        {
            CurTarget.AddData();
        }
    }

    // Asset에 사용할 캐릭터를 미리 선별해서 캐릭터 팝업리스트에 사용함 (캐릭터가 수십개가 넘어가면 효율적)
    public void CharacterAppearSet()
    {
        EditorGUILayout.LabelField("Dialogue Use Character Colleciton");

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Character", GUILayout.Width(150)))
        {
            CurTarget.start_C_set.Add(0);
        }

        if (GUILayout.Button("Remove (Last Index)", GUILayout.Width(150)))
        {
            CurTarget.start_C_set.RemoveRange(CurTarget.start_C_set.Count - 1, 1);
        }
        GUILayout.EndHorizontal();

        if (CurTarget.start_C_set != null)
        {
            if (CurTarget.start_C_set.Count < 4)
            {
                GUILayout.BeginVertical("Box", GUILayout.Width(430));
                GUILayout.BeginHorizontal();
                for (int a = 0; a < CurTarget.start_C_set.Count; a++)
                {
                    CurTarget.start_C_set[a] = (ECharacterName)EditorGUILayout.EnumPopup(CurTarget.start_C_set[a], GUILayout.Width(100));
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
            else if (CurTarget.start_C_set.Count >= 4 && CurTarget.start_C_set.Count < 8)
            {
                GUILayout.BeginVertical("Box", GUILayout.Width(430));
                GUILayout.BeginHorizontal();
                for (int a = 0; a < 4; a++)
                {
                    CurTarget.start_C_set[a] = (ECharacterName)EditorGUILayout.EnumPopup(CurTarget.start_C_set[a], GUILayout.Width(100));
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(5);
                GUILayout.BeginHorizontal();
                for (int a = 4; a < CurTarget.start_C_set.Count; a++)
                {
                    CurTarget.start_C_set[a] = (ECharacterName)EditorGUILayout.EnumPopup(CurTarget.start_C_set[a], GUILayout.Width(100));
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
            else if (CurTarget.start_C_set.Count >= 8 && CurTarget.start_C_set.Count < 12)
            {
                GUILayout.BeginVertical("Box", GUILayout.Width(430));
                GUILayout.BeginHorizontal();
                for (int a = 0; a < 4; a++)
                {
                    CurTarget.start_C_set[a] = (ECharacterName)EditorGUILayout.EnumPopup(CurTarget.start_C_set[a], GUILayout.Width(100));
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(5);
                GUILayout.BeginHorizontal();
                for (int a = 4; a < 8; a++)
                {
                    CurTarget.start_C_set[a] = (ECharacterName)EditorGUILayout.EnumPopup(CurTarget.start_C_set[a], GUILayout.Width(100));
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(5);
                GUILayout.BeginHorizontal();
                for (int a = 8; a < CurTarget.start_C_set.Count; a++)
                {
                    CurTarget.start_C_set[a] = (ECharacterName)EditorGUILayout.EnumPopup(CurTarget.start_C_set[a], GUILayout.Width(100));
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
        }

        if (CurTarget.start_C_set.Count == 0)
            CurTarget.start_C_set.Add(0);
    }

    // 해당 List의 최대개수를 파악함( End Key가 없을때 추가 )
    public void EndIndexCheck()
    {
        if (CurTarget.speech_Lists.speech_List.Count == 0)
        {
            CurTarget.speech_Lists.AddEnd();
        }

        if (CurTarget.speech_Lists.speech_List[0].key == "")
            CurTarget.speech_Lists.speech_List[0].key = "Real New Key";

        for (int i = 0; i < CurTarget.speech_Lists.speech_List.Count; i++)
        {
            if (CurTarget.speech_Lists.speech_List[i].key == "End")
            {
                EndIndex = i;
                return;
            }
        }

        CurTarget.speech_Lists.speech_List[CurTarget.speech_Lists.speech_List.Count - 1].key = "End";
        EndIndex = CurTarget.speech_Lists.speech_List.Count - 1;
    }

    // Speech로 이루어진 List에서 Key값으로 단락을 나누어 보여줌 (버튼 클릭 -> 단락 내용 출력) 
    public void KeyListSetting()
    {
        GUILayout.BeginVertical("Box");

        for (int i = 0; i < CurTarget.speech_Lists.speech_List.Count; i++)
        {
            if (CurTarget.speech_Lists.speech_List[i].key != "" && CurTarget.speech_Lists.speech_List[i].key != null)
            {
                GUILayout.BeginVertical("Box", GUILayout.Width(400));
                GUILayout.BeginHorizontal();
                EditorGUI.BeginChangeCheck();
                if (CurTarget.speech_Lists.speech_List[i].key != "End")
                {
                    string key = EditorGUILayout.TextField(CurTarget.speech_Lists.speech_List[i].key, GUILayout.Width(200));
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(CurTarget, "Dialogue Key");
                        CurTarget.speech_Lists.speech_List[i].key = key;
                        SceneView.RepaintAll();
                    }
                }
                else
                {
                    GUI.enabled = false;
                    EditorGUILayout.TextField(CurTarget.speech_Lists.speech_List[i].key, GUILayout.Width(200));
                    GUI.enabled = true;
                }

                if (GUILayout.Button("Key Data View", GUILayout.Width(150)))
                {
                    CurKey = CurTarget.speech_Lists.speech_List[i].key;

                    CurIndex = i;

                    if (CurKey == "End")
                        LastIndex = CurIndex + 1;
                    else
                    {
                        for (int w = CurIndex; w < CurTarget.speech_Lists.speech_List.Count; w++)
                        {
                            if (CurTarget.speech_Lists.speech_List[w].key != CurKey && CurTarget.speech_Lists.speech_List[w].key != "" && CurTarget.speech_Lists.speech_List[w].key != null)
                            {
                                if (CurTarget.speech_Lists.speech_List[w].key == "End")
                                {
                                    LastIndex = w;
                                    break;
                                }
                                LastIndex = w;
                                break;
                            }
                        }
                    }
                }

                if (GUILayout.Button("X", GUILayout.Width(30)))
                {
                    CurKey = CurTarget.speech_Lists.speech_List[i].key;
                    CurIndex = i;

                    if (i < 3)
                        CurIndex = 0;

                    if (CurKey == "End")
                        LastIndex = CurIndex + 1;
                    else
                    {
                        for (int w = CurIndex; w < CurTarget.speech_Lists.speech_List.Count; w++)
                        {
                            if (CurTarget.speech_Lists.speech_List[w].key != CurKey && CurTarget.speech_Lists.speech_List[w].key != "" && CurTarget.speech_Lists.speech_List[w].key != null)
                            {
                                if (CurTarget.speech_Lists.speech_List[w].key == "End")
                                {
                                    LastIndex = w;
                                    break;
                                }
                                LastIndex = w;
                                break;
                            }
                            if (CurKey == "End")
                            {
                                LastIndex = w + 1;
                                break;
                            }
                        }
                    }

                    int count = LastIndex - CurIndex;
                    CurTarget.speech_Lists.speech_List.RemoveRange(CurIndex, count);
                    LastIndex -= count;
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
        }
        GUILayout.EndVertical();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("New Key Add", GUILayout.Width(120)))
        {
            CurTarget.speech_Lists.speech_List.Insert(EndIndex, new Speech()
            {
                key = "New Key_" + EndIndex,
            });
            CurTarget.speech_Lists.speech_List.Insert(EndIndex + 1, new Speech()
            {
                key = ""
            });
        }
        if (GUILayout.Button("Language View", GUILayout.Width(120)))
        {
            if (CurTarget.languageView)
                CurTarget.languageView = false;
            else
                CurTarget.languageView = true;
        }

        GUILayout.EndHorizontal();
    }

    // 다이얼로그 캐릭터 대사 세팅 및 출력
    public void DialogueTextSetting(int i)
    {
        if (CurTarget.languageView)
        {
            EditorGUI.BeginChangeCheck();
            string text = "";
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(CurTarget.languageName[0], GUILayout.Width(60));
            text = EditorGUILayout.TextArea(CurTarget.speech_Lists.speech_List[i].textList[0]);
            GUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(CurTarget, "Dialogue text");
                CurTarget.speech_Lists.speech_List[i].textList[0] = text;
                SceneView.RepaintAll();
            }
        }
        else
        {
            EditorGUI.BeginChangeCheck();
            string[] text = new string[4];
            for (int t = 0; t < (int)EGameLanuage.Count; t++)
            {
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(CurTarget.languageName[t], GUILayout.Width(60));
                text[t] = EditorGUILayout.TextArea(CurTarget.speech_Lists.speech_List[i].textList[t]);
                GUILayout.EndHorizontal();

            }
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(CurTarget, "Dialogue text2");
                for (int t = 0; t < (int)EGameLanuage.Count; t++)
                {
                    CurTarget.speech_Lists.speech_List[i].textList[t] = text[t];
                }
                SceneView.RepaintAll();
            }
        }
    }

    public void DrawEdittingMode()
    {
        for (int i = CurIndex; i < LastIndex; i++)
        {
            switch (CurTarget.speech_Lists.speech_List[i].eActionType)
            {
                case 0:
                    GUI.color = UnityEngine.Color.yellow;
                    GUILayout.BeginVertical(GUI.skin.box);
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("No." + i.ToString(), GUILayout.Width(50));

                    if (GUILayout.Button("Index Add", GUILayout.Width(90)))
                    {
                        CurTarget.speech_Lists.speech_List.Insert(i, new Speech()
                        {
                            key = "",
                            eActionType = 0,
                        });
                        LastIndex++;
                    }

                    if (GUILayout.Button("Up", GUILayout.Width(70)))
                    {
                        curSpeech = CurTarget.speech_Lists.speech_List[i];
                        MoveUpSpeech(curSpeech, i);
                    }
                    if (GUILayout.Button("Down", GUILayout.Width(70)))
                    {
                        curSpeech = CurTarget.speech_Lists.speech_List[i];
                        MoveDownSpeech(curSpeech, i);
                    }

                    if (GUILayout.Button("X", GUILayout.Width(70)))
                    {
                        curSpeech = CurTarget.speech_Lists.speech_List[i];
                        CurTarget.speech_Lists.speech_List.Remove(
                            curSpeech
                        );
                        curSpeech = null;
                        LastIndex--;
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.Space(3);

                    if (CurTarget.speech_Lists.speech_List[i].key != "")
                    {
                        GUI.color = UnityEngine.Color.green;
                        EditorGUI.BeginChangeCheck();
                        GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(255));
                        GUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Dialogue Key", GUILayout.Width(100));
                        string key = EditorGUILayout.TextArea(CurTarget.speech_Lists.speech_List[i].key, GUILayout.Width(255));
                        GUILayout.EndHorizontal();
                        GUILayout.EndVertical();
                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(CurTarget, "Dialogue Key");
                            CurTarget.speech_Lists.speech_List[i].key = key;
                            SceneView.RepaintAll();
                        }
                        GUI.color = UnityEngine.Color.yellow;
                    }
                    else
                    {
                        EditorGUI.BeginChangeCheck();
                        GUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Dialogue Key", GUILayout.Width(100));
                        string key = EditorGUILayout.TextArea(CurTarget.speech_Lists.speech_List[i].key, GUILayout.Width(255));
                        GUILayout.EndHorizontal();
                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(CurTarget, "Dialogue Key");
                            CurTarget.speech_Lists.speech_List[i].key = key;
                            SceneView.RepaintAll();
                        }
                    }

                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Action Type", GUILayout.Width(100));
                    CurTarget.speech_Lists.speech_List[i].eActionType = (EActionType)EditorGUILayout.EnumPopup(CurTarget.speech_Lists.speech_List[i].eActionType, GUILayout.Width(255));
                    GUILayout.EndHorizontal();

                    if (CurTarget.speech_Lists.speech_List[i].eDialogueType == EDialogueType.Dialogue)
                    {
                        GUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Dialogue Type", GUILayout.Width(100));
                        CurTarget.speech_Lists.speech_List[i].eDialogueType = (EDialogueType)EditorGUILayout.EnumPopup(CurTarget.speech_Lists.speech_List[i].eDialogueType, GUILayout.Width(255));
                        GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Name", GUILayout.Width(75));

                        CurTarget.speech_Lists.speech_List[i].c_num = EditorGUILayout.Popup(CurTarget.speech_Lists.speech_List[i].c_num, CurTarget.start_C_Array, GUILayout.Width(100));
                        CurTarget.speech_Lists.speech_List[i].eCharacterName =
                            (ECharacterName)Enum.Parse(typeof(ECharacterName), CurTarget.start_C_Array[CurTarget.speech_Lists.speech_List[i].c_num]);

                        EditorGUILayout.LabelField("Hide Img", GUILayout.Width(60));
                        CurTarget.speech_Lists.speech_List[i].isCharacterHideOn = EditorGUILayout.Toggle(CurTarget.speech_Lists.speech_List[i].isCharacterHideOn, GUILayout.Width(50));
                        GUILayout.EndHorizontal();

                        // 캐릭터 대사 세팅 및 출력 GUI
                        DialogueTextSetting(i);
                    }
                    else if (CurTarget.speech_Lists.speech_List[i].eDialogueType == EDialogueType.Narration)
                    {
                        GUI.color = UnityEngine.Color.cyan;
                        GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(330));
                        GUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Dialogue Type", GUILayout.Width(100));
                        CurTarget.speech_Lists.speech_List[i].eDialogueType = (EDialogueType)EditorGUILayout.EnumPopup(CurTarget.speech_Lists.speech_List[i].eDialogueType, GUILayout.Width(255));
                        GUILayout.EndHorizontal();
                        GUILayout.EndVertical();
                        GUI.color = UnityEngine.Color.yellow;

                        // 캐릭터 대사 세팅 및 출력 GUI
                        DialogueTextSetting(i);
                    }
                    else
                    {
                        GUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Dialogue Type", GUILayout.Width(100));
                        CurTarget.speech_Lists.speech_List[i].eDialogueType = (EDialogueType)EditorGUILayout.EnumPopup(CurTarget.speech_Lists.speech_List[i].eDialogueType, GUILayout.Width(255));
                        GUILayout.EndHorizontal();

                        GUILayout.Space(5);

                        // 캐릭터 대사 세팅 및 출력 GUI
                        DialogueTextSetting(i);
                    }

                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Character Appear Position", GUILayout.Width(160));
                    CurTarget.speech_Lists.speech_List[i].isAppearCheck = EditorGUILayout.Toggle(CurTarget.speech_Lists.speech_List[i].isAppearCheck, GUILayout.Width(50));
                    GUILayout.EndHorizontal();

                    if (CurTarget.speech_Lists.speech_List[i].isAppearCheck == true)
                    {
                        GUILayout.BeginHorizontal();
                        for (int w = 0; w < 3; w++)
                        {
                            CurTarget.speech_Lists.speech_List[i].appear_num[w] =
                                EditorGUILayout.Popup(CurTarget.speech_Lists.speech_List[i].appear_num[w], CurTarget.start_C_Array, GUILayout.Width(100));
                            CurTarget.speech_Lists.speech_List[i].eAppearCharacterName[w] =
                                (ECharacterName)Enum.Parse(typeof(ECharacterName), CurTarget.start_C_Array[CurTarget.speech_Lists.speech_List[i].appear_num[w]]);
                        }
                        GUILayout.EndHorizontal();
                    }

                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Character Exit Position", GUILayout.Width(160));
                    CurTarget.speech_Lists.speech_List[i].isExitCheck = EditorGUILayout.Toggle(CurTarget.speech_Lists.speech_List[i].isExitCheck, GUILayout.Width(50));
                    GUILayout.EndHorizontal();

                    if (CurTarget.speech_Lists.speech_List[i].isExitCheck == true)
                    {
                        GUILayout.BeginHorizontal();
                        for (int w = 0; w < 3; w++)
                        {
                            CurTarget.speech_Lists.speech_List[i].isPosCheck[w] = EditorGUILayout.Toggle(CurTarget.speech_Lists.speech_List[i].isPosCheck[w], GUILayout.Width(20));
                        }
                        GUILayout.EndHorizontal();
                    }

                    GUILayout.EndVertical();

                    break;
                case (EActionType)1:
                    GUI.color = UnityEngine.Color.white;
                    GUILayout.BeginVertical(GUI.skin.box);
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("No." + i.ToString(), GUILayout.Width(50));

                    if (GUILayout.Button("Index Add", GUILayout.Width(90)))
                    {
                        CurTarget.speech_Lists.speech_List.Insert(i, new Speech()
                        {
                            key = "",
                            eActionType = 0,
                        });
                        LastIndex++;
                    }

                    if (GUILayout.Button("Up", GUILayout.Width(70)))
                    {
                        curSpeech = CurTarget.speech_Lists.speech_List[i];
                        MoveUpSpeech(curSpeech, i);
                    }
                    if (GUILayout.Button("Down", GUILayout.Width(70)))
                    {
                        curSpeech = CurTarget.speech_Lists.speech_List[i];
                        MoveDownSpeech(curSpeech, i);
                    }

                    if (GUILayout.Button("X", GUILayout.Width(70)))
                    {
                        curSpeech = CurTarget.speech_Lists.speech_List[i];
                        CurTarget.speech_Lists.speech_List.Remove(
                            curSpeech
                        );
                        curSpeech = null;
                        LastIndex--;
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.Space(3);

                    GUILayout.BeginHorizontal();
                    GUI.color = UnityEngine.Color.cyan;
                    GUILayout.BeginVertical(GUI.skin.box);
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Action Type", GUILayout.Width(100));
                    CurTarget.speech_Lists.speech_List[i].eActionType = (EActionType)EditorGUILayout.EnumPopup(CurTarget.speech_Lists.speech_List[i].eActionType, GUILayout.Width(110));
                    GUILayout.EndHorizontal();
                    GUILayout.EndVertical();
                    GUI.color = UnityEngine.Color.white;

                    EditorGUILayout.LabelField("Effect Type", GUILayout.Width(100));
                    CurTarget.speech_Lists.speech_List[i].eDetailType = (EDetailType)EditorGUILayout.EnumPopup(CurTarget.speech_Lists.speech_List[i].eDetailType, GUILayout.Width(110));
                    GUILayout.EndHorizontal();

                    GUILayout.Space(3);

                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("End Delay Type", GUILayout.Width(100));
                    CurTarget.speech_Lists.speech_List[i].eEndDelayType = (EEndDelayType)EditorGUILayout.EnumPopup(CurTarget.speech_Lists.speech_List[i].eEndDelayType, GUILayout.Width(110));

                    EditorGUILayout.LabelField("Delay Time", GUILayout.Width(75));
                    CurTarget.speech_Lists.speech_List[i].endDeleyTime = EditorGUILayout.FloatField(CurTarget.speech_Lists.speech_List[i].endDeleyTime, GUILayout.Width(110));
                    GUILayout.EndHorizontal();

                    GUILayout.Space(3);

                    if (CurTarget.speech_Lists.speech_List[i].eDetailType != EDetailType.Negative)
                    {
                        GUI.color = UnityEngine.Color.cyan;
                        GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(300));
                        GUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Fade On/Off", GUILayout.Width(75));
                        CurTarget.speech_Lists.speech_List[i].isFadeCheck = EditorGUILayout.Toggle(CurTarget.speech_Lists.speech_List[i].isFadeCheck);
                        GUILayout.EndHorizontal();
                        GUILayout.EndVertical();
                        GUI.color = UnityEngine.Color.white;

                        GUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Image Name", GUILayout.Width(80));
                        CurTarget.speech_Lists.speech_List[i].strValue = EditorGUILayout.TextArea(CurTarget.speech_Lists.speech_List[i].strValue, GUILayout.Width(200));
                        GUILayout.EndHorizontal();
                    }
                    else
                    {
                        GUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Play Time", GUILayout.Width(75));
                        CurTarget.speech_Lists.speech_List[i].floatValue = EditorGUILayout.FloatField(CurTarget.speech_Lists.speech_List[i].floatValue, GUILayout.Width(100));
                        GUILayout.EndHorizontal();
                    }

                    GUILayout.EndVertical();
                    break;
                case (EActionType)2:
                    GUI.color = UnityEngine.Color.white;
                    GUILayout.BeginVertical(GUI.skin.box);
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("No." + i.ToString(), GUILayout.Width(50));

                    if (GUILayout.Button("Index Add", GUILayout.Width(90)))
                    {
                        CurTarget.speech_Lists.speech_List.Insert(i, new Speech()
                        {
                            key = "",
                            eActionType = 0,
                        });
                        LastIndex++;
                    }

                    if (GUILayout.Button("Up", GUILayout.Width(70)))
                    {
                        curSpeech = CurTarget.speech_Lists.speech_List[i];
                        MoveUpSpeech(curSpeech, i);
                    }
                    if (GUILayout.Button("Down", GUILayout.Width(70)))
                    {
                        curSpeech = CurTarget.speech_Lists.speech_List[i];
                        MoveDownSpeech(curSpeech, i);
                    }

                    if (GUILayout.Button("X", GUILayout.Width(70)))
                    {
                        curSpeech = CurTarget.speech_Lists.speech_List[i];
                        CurTarget.speech_Lists.speech_List.Remove(
                            curSpeech
                        );
                        curSpeech = null;
                        LastIndex--;
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.Space(3);

                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Action Type", GUILayout.Width(100));
                    CurTarget.speech_Lists.speech_List[i].eActionType = (EActionType)EditorGUILayout.EnumPopup(CurTarget.speech_Lists.speech_List[i].eActionType, GUILayout.Width(110));
                    EditorGUILayout.LabelField("Effect Type", GUILayout.Width(75));
                    CurTarget.speech_Lists.speech_List[i].eDetailType = (EDetailType)EditorGUILayout.EnumPopup(CurTarget.speech_Lists.speech_List[i].eDetailType, GUILayout.Width(110));
                    GUILayout.EndHorizontal();

                    GUILayout.Space(3);
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("End Delay Type", GUILayout.Width(100));
                    CurTarget.speech_Lists.speech_List[i].eEndDelayType = (EEndDelayType)EditorGUILayout.EnumPopup(CurTarget.speech_Lists.speech_List[i].eEndDelayType, GUILayout.Width(110));
                    GUILayout.EndHorizontal();

                    GUILayout.Space(3);

                    GUILayout.EndVertical();
                    break;
            }
        }
    }

    public void MoveUpSpeech(Speech speech, int index)
    {
        if (index == 0 || index == 1 || index == CurIndex)
            return;

        Speech prevSpeech = CurTarget.speech_Lists.speech_List[index - 1];
        CurTarget.speech_Lists.speech_List[index - 1] = CurTarget.speech_Lists.speech_List[index];
        CurTarget.speech_Lists.speech_List[index] = prevSpeech;
    }

    public void MoveDownSpeech(Speech speech, int index)
    {
        if (index == CurTarget.speech_Lists.speech_List.Count - 1)
            return;
        if (index == LastIndex - 1)
            return;

        Speech nextSpeech = CurTarget.speech_Lists.speech_List[index + 1];
        CurTarget.speech_Lists.speech_List[index + 1] = CurTarget.speech_Lists.speech_List[index];
        CurTarget.speech_Lists.speech_List[index] = nextSpeech;
    }

    // 캐릭터 선택 팝업을 위함
    public void NameSwapArray()
    {
        CurTarget.start_C_Array = new string[CurTarget.start_C_set.Count];
        for (int i = 0; i < CurTarget.start_C_set.Count; i++)
        {
            CurTarget.start_C_Array[i] = CurTarget.start_C_set[i].ToString();
        }
    }
}
