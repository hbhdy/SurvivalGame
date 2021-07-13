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
    public Button nextBtn;  
    public Vector2 baseScreenRaio = Vector2.zero;
    private CanvasScaler canvasScaler;
    private float uiScreenRatio = 1.0f;


    private Dictionary<int, string> dicSequenceList = new Dictionary<int, string>();
    private Dictionary<string, GameObject> dicCharacter = new Dictionary<string, GameObject>();
    private GameObject cam;
    private bool isNextCheck;
    private bool isSkipCheck;


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
}
