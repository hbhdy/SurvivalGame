using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : HSSManager
{
    // 조작 범위
    private Rect leftHalfRect;
    private Rect rightHalfRect;
    private Rect topHalfRect;
    private Rect bottomHalfRect;
    private Rect allRect;

    private Rect setRect;

    [HideInInspector]
    public bool isPressed = false;
    [HideInInspector]
    public bool isDrag = false;
    [HideInInspector]
    public bool isRelease = false;

    [HideInInspector]
    public Vector2 inputPosition = Vector2.zero;

    public override IEnumerator ManagerInitProcessing()
    {
        yield return StartCoroutine(InitManager());

        yield return StartCoroutine(base.ManagerInitProcessing());
    }

    public override IEnumerator InitManager()
    {
        Vector2 horizonSize = Vector2.zero;
        horizonSize.x = Screen.width * 0.5f;
        horizonSize.y = Screen.height;

        // 왼쪽 절반 화면 
        leftHalfRect = new Rect(Vector2.zero, horizonSize);

        // 오른쪽 절반 화면 
        rightHalfRect = new Rect(new Vector2(Screen.width * 0.5f, 0), horizonSize);

        Vector2 verticalSize = Vector2.zero;
        verticalSize.x = Screen.width;
        verticalSize.y = Screen.height * 0.5f;

        // 상단 절반 화면
        topHalfRect = new Rect(Vector2.zero, verticalSize);

        // 하단 절반 화면
        bottomHalfRect = new Rect(new Vector2(Screen.width * 0.5f, 0), new Vector2(Screen.width, Screen.height));

        // 전체 화면
        allRect = new Rect(Vector2.zero, new Vector2(Screen.width, Screen.height));

        setRect = allRect;

        yield return StartCoroutine(base.InitManager());
    }

    public void Update()
    {
        GameInput();
    }

    public void ClearInput()
    {
        isPressed = false;
        isDrag = false;
        isRelease = false;

        inputPosition = Vector2.zero;
    }

    public void GameInput()
    {
        ClearInput();


#if (UNITY_STANDALONE || UNITY_EDITOR)
        PCInput();
#elif (UNITY_IOS || UNITY_ANDROID)
        MobileInput();
#endif
    }

    public void PCInput()
    {
        Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        if (Input.GetMouseButtonDown(0))
        {
            if (!setRect.Contains(pos))
                return;
            isPressed = true;
        }

        if (Input.GetMouseButton(0))
        {
            isDrag = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDrag)
                isDrag = false;

            isRelease = true;
        }

        inputPosition = Input.mousePosition;
    }

    public void MobileInput()
    {
        if (Input.touchCount <= 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            if (!setRect.Contains(touch.position))
                return;

            isPressed = true;
        }

        if (touch.phase == TouchPhase.Moved)
        {
            isDrag = true;
        }

        if (touch.phase == TouchPhase.Ended)
        {
            if (isDrag)
                isDrag = false;

            isRelease = true;
        }

        inputPosition = Input.mousePosition;
    }
}
