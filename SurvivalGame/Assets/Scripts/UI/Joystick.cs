using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 고정되어 있는 조이스틱과 화면전체에 따라 움직이는 조이스틱: 2가지 타입
public class Joystick : MonoBehaviour
{
    public static Joystick instance = null;

    public GameObject objJoystickOut;
    public GameObject objJoystickIn;
    public Camera uiCamera;

    public Vector2 moveDir = Vector2.zero;
    public float round = 100.0f;

    private Vector2 offset = Vector2.zero;
    private Vector2 originPos = Vector2.zero;
    private Vector2 useOriginPos = Vector2.zero;
    private Vector2 inputPos = Vector2.zero;

    public RectTransform outsideRt;
    private RectTransform rt;

    private bool isPressed = false;
    private bool isFixedStick = false;

    [HideInInspector]
    public bool isPause = false;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        rt = GetComponent<RectTransform>();
        originPos = rt.anchoredPosition;
        useOriginPos = originPos;

        objJoystickOut.SetActive(false);
        objJoystickIn.SetActive(false);
    }

    public void FixedUpdate()
    {
        if (isPause)
            return;

        if (isPressed)
        {
            if (Vector2.Distance(rt.anchoredPosition, outsideRt.anchoredPosition) < 0.1f)
            {
                return;
            }

            // moveDir로 이동방향 설정
            moveDir = rt.anchoredPosition - outsideRt.anchoredPosition;
            moveDir = moveDir.normalized;
        }
    }

    // 고정 및 움직임
    public void SetJoystickOption(bool set)
    {
        isFixedStick = set;
    }

    public void OnPointerDown(Vector2 eventData)
    {
        if (isPause)
            return;

        inputPos = eventData;
        inputPos = ConvertScreenToAnchoredPos(inputPos);

        offset = useOriginPos - inputPos;

        // 움직임
        if (!isFixedStick)
        {
            isPressed = true;
            outsideRt.anchoredPosition = inputPos;
            rt.anchoredPosition = inputPos;
            objJoystickOut.SetActive(true);
            objJoystickIn.SetActive(true);
        }
        else // 고정
        {
            if (Vector2.Distance(rt.anchoredPosition, inputPos) < 50)
            {
                isPressed = true;
            }
        }
    }

    public void OnDrag(Vector2 eventData)
    {
        if (isPause)
            return;

        if (!isPressed)
            return;

        inputPos = eventData;
        inputPos = ConvertScreenToAnchoredPos(inputPos);

        // 움직임
        if (!isFixedStick)
        {
            rt.anchoredPosition = inputPos;

            if (Vector2.Distance(rt.anchoredPosition, outsideRt.anchoredPosition) > round)
            {
                Vector2 dir = rt.anchoredPosition - outsideRt.anchoredPosition;
                dir = dir.normalized;

                outsideRt.anchoredPosition = rt.anchoredPosition - dir * round;
            }
        }
        else // 고정
        {
            rt.anchoredPosition = inputPos + offset;

            if (Vector2.Distance(inputPos, useOriginPos) > round)
            {
                Vector2 dir = inputPos - useOriginPos;
                dir = dir.normalized;

                rt.anchoredPosition = useOriginPos + dir * round + offset;
            }
        }
    }

    public void OnPointerUp(Vector2 eventData)
    {
        // 초기화
        moveDir = Vector2.zero;
        inputPos = Vector2.zero;
        offset = Vector2.zero;

        isPressed = false;

        outsideRt.anchoredPosition = useOriginPos;
        rt.anchoredPosition = useOriginPos;
    }

    // 이동 체크
    public Vector3 GetDir()
    {
        if (isPressed)
        {
            return moveDir;
        }
        else
            return Vector3.zero;
    }

    // 이동량 체크 ( 드래그 파워 )
    public float GetMoveForce()
    {
        if (isPressed)
        {
            if (Vector2.Distance(rt.anchoredPosition, outsideRt.anchoredPosition) < 0.1f)
            {
                return 0;
            }

            float force = Vector2.Distance(rt.anchoredPosition, outsideRt.anchoredPosition) / round;

            if (force > 1.0f)
                force = 1.0f;

            return 1.0f;
        }
        else
            return 0.0f;
    }

    public Vector2 ConvertScreenToAnchoredPos(RectTransform parent, Vector3 screen, Camera uiCam)
    {
        Vector2 newInputPos = Vector2.zero;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, screen, uiCam, out newInputPos);

        return newInputPos;
    }

    public Vector2 ConvertScreenToAnchoredPos(Vector3 screen)
    {
        Vector2 newInputPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), screen, uiCamera, out newInputPos);

        return newInputPos;
    }

    public void SetPause(bool set)
    {
        isPause = set;
    }

    public void SetJoystick(bool set)
    {
        objJoystickOut.SetActive(set);
        objJoystickIn.SetActive(set);
    }
}
