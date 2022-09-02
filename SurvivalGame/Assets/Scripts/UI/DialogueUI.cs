using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI instance = null;

    [Header("Object")]
    public GameObject objBgFrame;
    public GameObject objBgImg1;
    public GameObject objBgImg2;
    public GameObject objScreenEffect;
    public GameObject objDialogue;
    public GameObject objDialogueWindow;
    public GameObject objDialogueName;
    public GameObject objNarration;
    public GameObject objNarrationWindow;
    public GameObject objCheckBtn;
    public GameObject objSkip;
    public List<GameObject> objCharacters = new List<GameObject>();

    [Header("Text")]
    public Text txtDialogue;
    public Text txtName;
    public Text txtNarration;

    [Header("Etc")]
    public List<Sprite> characterSprites = new List<Sprite>();
    public Button nextBtn;  
    public Vector2 baseScreenRaio = Vector2.zero;
    private CanvasScaler canvasScaler;
    private float uiScreenRatio = 1.0f;

    private Dictionary<string, Sprite> dicCharacterSprite = new Dictionary<string, Sprite>();
    private Dictionary<int, string> dicSequenceList = new Dictionary<int, string>();
    private Dictionary<string, NameTextData> dicNameList = new Dictionary<string, NameTextData>();
    private GameObject cam;
    private bool isNextCheck;
    private bool isSkipCheck;
    private bool isCutFade = false;
    private EGameLanuage currentLanuage = EGameLanuage.Korean;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (transform.parent == null)
            DontDestroyOnLoad(gameObject);

        Init();
    }

    public void Init()
    {
        ScreenCheck();

        dicSequenceList.Clear();

        for (int i = 0; i < 3; i++)
        {
            dicSequenceList.Add(i, i.ToString());
        }

        for (int i = 0; i < characterSprites.Count; i++)
        {
            dicCharacterSprite.Add(characterSprites[i].name.ToString(), characterSprites[i]);
        }

        dicNameList = Core.RSS.GetNameText();
    }

    // 스크린 비율 조정
    public void ScreenCheck()
    {
        canvasScaler = GetComponent<CanvasScaler>();

        float currentRatio = (float)Screen.width / (float)Screen.height;

        float baseRatio = baseScreenRaio.x / baseScreenRaio.y;

        if (currentRatio < baseRatio)
            uiScreenRatio = 0.0f;
        else
            uiScreenRatio = 1.0f;

        canvasScaler.matchWidthOrHeight = uiScreenRatio;
    }

    public void StartDialogue(string key)
    {
        StartCoroutine(DialogueRoutine(key));
    }

    public IEnumerator DialogueRoutine(string key)
    {
        GameUI.instance.HudsOnOff(false);
        objSkip.SetActive(false);

        cam = GameObject.FindGameObjectWithTag("MainCamera");
        if (cam != null)
            GetComponent<Canvas>().worldCamera = cam.GetComponent<Camera>();

        InGameCore.instance.GamePause(InGameCore.E_Pause_Type.Pause_Type_Dialogue, true);
        Joystick.instance.SetJoystick(false);
        Joystick.instance.OnPointerUp(Vector2.zero);

        objSkip.SetActive(true);
        yield return StartCoroutine(StoryLine(key));

        InGameCore.instance.GamePause(InGameCore.E_Pause_Type.Pause_Type_Dialogue, false);
        Joystick.instance.SetJoystick(true);
        GameUI.instance.HudsOnOff(true);
    }

    public IEnumerator StoryLine(string key)
    {
        // 키에 따른 대화 데이터 읽어옴
        List<DialogueDataSet> storyData = Core.RSS.GetDialogueKeyData(key);

        objCheckBtn.SetActive(true);
        isSkipCheck = false;
        isNextCheck = false;

        for (int i = 0; i < storyData.Count; i++)
        {
            if(isSkipCheck)
            {
                switch (storyData[i].eActionType)
                {
                    case EActionType.Dialogue:
                        for (int a = 0; a < objCharacters.Count; a++)
                        {
                            objCharacters[a].SetActive(false);
                        }

                        continue;
                    case EActionType.DisplayImage:
                        objBgFrame.SetActive(false);
                        objBgImg1.SetActive(false);
                        objBgImg2.SetActive(false);
                        continue;

                    case EActionType.ScreenEffect:
                        objScreenEffect.SetActive(false);
                        continue;
                }
            }

            switch(storyData[i].eActionType)
            {
                case EActionType.Dialogue:
                    // 대사 저장
                    string tempStr = storyData[i].dialogue.textList[(int)currentLanuage];

                    // 대사 및 나레이션 출력 + 스킵, Input 확인
                    switch (storyData[i].dialogue.dialougeType)
                    {
                        case EDialogueType.Dialogue:
                            objDialogue.SetActive(true);

                            // 등장 캐릭터 처리
                            if (storyData[i].dialogue.appearCheck)
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    if (storyData[i].dialogue.appearCharacterName[j] != ECharacterName.None)
                                    {
                                        dicSequenceList[j] = storyData[i].dialogue.appearCharacterName[j].ToString();
                                        objCharacters[j].GetComponent<Image>().sprite = dicCharacterSprite[dicSequenceList[j]];
                                        objCharacters[j].SetActive(true);
                                    }
                                }
                            }

                            // 퇴장 캐릭처 처리
                            if (storyData[i].dialogue.exitCheck)
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    if(storyData[i].dialogue.posCheck[j])
                                    {
                                        objCharacters[j].SetActive(false);
                                    }
                                }
                            }

                            if (tempStr.Length == 0)
                                objDialogueWindow.SetActive(false);
                            else
                                objDialogueWindow.SetActive(true);

                            if (storyData[i].dialogue.name != ECharacterName.None)
                            {
                                objDialogueName.SetActive(true);
                                txtName.text = dicNameList[storyData[i].dialogue.name.ToString()].viewName[(int)currentLanuage];
                            }

                            nextBtn.enabled = true;

                            for (int t = 0; t < tempStr.Length; t++)
                            {
                                txtDialogue.text = tempStr.Substring(0, t + 1);

                                if (!isNextCheck && !isSkipCheck)
                                    yield return new WaitForEndOfFrame();
                            }

                            yield return new WaitUntil(() => isNextCheck);
                            nextBtn.enabled = false;
                            isNextCheck = false;

                            break;

                        case EDialogueType.Narration:
                            objDialogue.SetActive(false);
                            objNarration.SetActive(true);

                            // 등장 캐릭터 처리
                            if (storyData[i].dialogue.appearCheck)
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    if (storyData[i].dialogue.appearCharacterName[j] != ECharacterName.None)
                                    {
                                        dicSequenceList[j] = storyData[i].dialogue.appearCharacterName[j].ToString();

                                        objCharacters[j].GetComponent<Image>().sprite = dicCharacterSprite[dicSequenceList[j]];
                                        objCharacters[j].SetActive(true);
                                    }
                                }
                            }

                            // 퇴장 캐릭처 처리
                            if (storyData[i].dialogue.exitCheck)
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    if (storyData[i].dialogue.posCheck[j])
                                    {
                                        objCharacters[j].SetActive(false);
                                    }
                                }
                            }

                            for (int t = 0; t < tempStr.Length; t++)
                            {
                                txtDialogue.text = tempStr.Substring(0, t + 1);

                                if (!isNextCheck && !isSkipCheck)
                                    yield return new WaitForEndOfFrame();
                            }
                            break;

                        case EDialogueType.None:
                            objDialogue.SetActive(false);
                            objDialogueName.SetActive(false);

                            // 등장 캐릭터 처리
                            if (storyData[i].dialogue.appearCheck)
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    if (storyData[i].dialogue.appearCharacterName[j] != ECharacterName.None)
                                    {
                                        dicSequenceList[j] = storyData[i].dialogue.appearCharacterName[j].ToString();

                                        objCharacters[j].GetComponent<Image>().sprite = dicCharacterSprite[dicSequenceList[j]];
                                        objCharacters[j].SetActive(true);
                                    }
                                }
                            }

                            // 퇴장 캐릭처 처리
                            if (storyData[i].dialogue.exitCheck)
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    if (storyData[i].dialogue.posCheck[j])
                                    {
                                        objCharacters[j].SetActive(false);
                                    }
                                }
                            }
                            break;
                           
                    }
                    break;

                case EActionType.DisplayImage:
                    switch (storyData[i].effectList.detailType)
                    {
                        case EDetailType.Background:
                            if (!storyData[i].effectList.fadeCheck)
                            {
                                if (storyData[i].effectList.strValue == "End")
                                {
                                    objBgFrame.SetActive(false);
                                    objBgImg1.SetActive(false);
                                }
                                else
                                {
                                    objBgFrame.GetComponent<Image>().color = Color.black;
                                    objBgImg1.GetComponent<Image>().color = Color.white;
                                    objBgFrame.SetActive(true);
                                    objBgImg1.SetActive(true);
                                    objBgImg1.GetComponent<Image>().sprite = Resources.Load<Sprite>("BGImage/" + storyData[i].effectList.strValue);
                                }
                            }
                            else
                            {
                                if (storyData[i].effectList.strValue == "End")
                                {
                                    yield return StartCoroutine(CutFadeCoroutine(objBgFrame, objBgImg1, objBgImg2, false, storyData[i].effectList.floatValue));
                                }
                                else
                                {
                                    if (!isCutFade)
                                    {
                                        objBgFrame.SetActive(true);
                                        objBgImg1.SetActive(true);
                                        objBgImg1.GetComponent<Image>().sprite = Resources.Load<Sprite>("BGImage/" + storyData[i].effectList.strValue);

                                        yield return StartCoroutine(CutFadeCoroutine(objBgFrame, objBgImg1, objBgImg2, true, storyData[i].effectList.floatValue));
                                    }
                                    else
                                    {
                                        objBgFrame.GetComponent<Image>().color = Color.black;
                                        objBgFrame.SetActive(true);
                                        objBgImg2.SetActive(true);
                                        objBgImg2.GetComponent<Image>().sprite = Resources.Load<Sprite>("BGImage/" + storyData[i].effectList.strValue);

                                        yield return StartCoroutine(CutFadeCoroutine(objBgFrame, objBgImg1, objBgImg2, true, storyData[i].effectList.floatValue));
                                        objBgImg1.GetComponent<Image>().sprite = Resources.Load<Sprite>("BGImage/" + storyData[i].effectList.strValue);
                                        objBgImg1.SetActive(true);
                                        objBgImg2.SetActive(false);
                                        objBgImg1.GetComponent<Image>().color = Color.white;
                                    }
                                }

                                if (!isCutFade)
                                    isCutFade = true;
                            }

                            break;
                    }

                    // 종료 딜레이 체크
                    switch (storyData[i].effectList.endDelayType)
                    {
                        case EEndDelayType.None:
                            yield return null;
                            break;
                        case EEndDelayType.WaitInput:
                            nextBtn.enabled = true;
                            yield return new WaitUntil(() => isNextCheck);
                            nextBtn.enabled = false;
                            isNextCheck = false;
                            break;
                    }
                    break;
                case EActionType.ScreenEffect:
                    objScreenEffect.GetComponent<Image>().sprite = null;

                    // 이펙트 출력
                    switch (storyData[i].effectList.detailType)
                    {
                        case EDetailType.BlackOut:
                            objScreenEffect.SetActive(true);
                            yield return StartCoroutine(EffectFadeCoroutine(storyData[i].effectList.floatValue, true, 1));
                            break;
                        case EDetailType.BlackIn:
                            yield return StartCoroutine(EffectFadeCoroutine(storyData[i].effectList.floatValue, false, 1));
                            break;

                        case EDetailType.WhiteOut:
                            objScreenEffect.SetActive(true);
                            yield return StartCoroutine(EffectFadeCoroutine(storyData[i].effectList.floatValue, true, 2));
                            break;

                        case EDetailType.WhiteIn:
                            yield return StartCoroutine(EffectFadeCoroutine(storyData[i].effectList.floatValue, false, 2));
                            break;

                        case EDetailType.Flash:
                            objScreenEffect.GetComponent<Image>().color = Color.white;
                            objScreenEffect.SetActive(true);
                            yield return new WaitForSeconds(0.1f);
                            objScreenEffect.SetActive(false);
                            break;
                    }

                    // 종료 딜레이 체크
                    switch (storyData[i].effectList.endDelayType)
                    {
                        case EEndDelayType.None:
                            yield return null;
                            break;
                        case EEndDelayType.WaitInput:
                            nextBtn.enabled = true;
                            yield return new WaitUntil(() => isNextCheck);
                            nextBtn.enabled = false;
                            isNextCheck = false;
                            break;
                    }
                    break;

            }
        }

        if (isCutFade)
            isCutFade = false;

        yield return null;

        objBgFrame.SetActive(false);
        objScreenEffect.SetActive(false);
        objDialogue.SetActive(false);
        objNarration.SetActive(false);
        objCheckBtn.SetActive(false);
        objSkip.SetActive(false);
    }

    public void NextCheck()
    {
        isNextCheck = true;
    }

    public void SkipCheck()
    {
        if (!isSkipCheck)
        {
            isSkipCheck = true;
            isNextCheck = true;
            nextBtn.enabled = false;
        }
    }

    // 배경 및 컷 페이드
    public IEnumerator CutFadeCoroutine(GameObject frame, GameObject cut1, GameObject cut2, bool fadeOut, float t)
    {
        Color colorFrame = frame.GetComponent<Image>().color;
        Color color1 = cut1.GetComponent<Image>().color;
        Color color2 = cut2.GetComponent<Image>().color;

        if (fadeOut)
        {
            if (!isCutFade)
            {
                float time = 0;
                color1.a = Mathf.Lerp(0, 1, time);
                colorFrame.a = Mathf.Lerp(0, 1, time);

                while (color1.a < 1f)
                {

                    time += 0.05f;
                    color1.a = Mathf.Lerp(0, 1, time);
                    colorFrame.a = Mathf.Lerp(0, 1, time);
                    cut1.GetComponent<Image>().color = color1;
                    frame.GetComponent<Image>().color = colorFrame;
                    yield return null;
                }
            }
            else if (isCutFade)
            {
                float time = 0;
                color2.a = Mathf.Lerp(0, 1, time);

                while (color2.a < 1f)
                {
                    time += 0.05f;
                    color2.a = Mathf.Lerp(0, 1, time);
                    cut2.GetComponent<Image>().color = color2;
                    yield return null;
                }
            }
        }
        else
        {
            float time = 0;
            color1.a = Mathf.Lerp(1, 0, time);
            colorFrame.a = Mathf.Lerp(1, 0, time);
            while (color1.a > 0)
            {
                time += 0.05f;
                color1.a = Mathf.Lerp(1, 0, time);
                colorFrame.a = Mathf.Lerp(1, 0, time);
                cut1.GetComponent<Image>().color = color1;
                frame.GetComponent<Image>().color = colorFrame;
                yield return null;
            }
            frame.SetActive(false);
            cut1.SetActive(false);
        }
    }

    // 효과 페이드 ( 블랙, 화이트, 블러드 ) [ Time.Scale = 0이 되었기에 time.deltatime이 안됨 ] 
    public IEnumerator EffectFadeCoroutine(float t, bool fadeOut, int type)
    {
        Color color = objScreenEffect.GetComponent<Image>().color;
        float time = 0;

        switch (type)
        {
            case 1: // 블랙
                color.r = 0;
                color.g = 0;
                color.b = 0;
                break;
            case 2: // 화이트
                color.r = 1;
                color.g = 1;
                color.b = 1;
                break;
            case 3: // 블러드
                color.r = 0.8f;
                color.g = 0.1f;
                color.b = 0;
                break;
        }

        if (fadeOut)
        {
            color.a = Mathf.Lerp(0, 1, time);
            while (color.a < 1f)
            {
                time += 0.05f;
                color.a = Mathf.Lerp(0, 1, time);
                objScreenEffect.GetComponent<Image>().color = color;
                yield return null;
            }
        }
        else
        {
            color.a = Mathf.Lerp(1, 0, time);

            while (color.a > 0)
            {
                time += 0.05f;
                color.a = Mathf.Lerp(1, 0, time);
                objScreenEffect.GetComponent<Image>().color = color;
                yield return null;
            }
        }
    }
}
