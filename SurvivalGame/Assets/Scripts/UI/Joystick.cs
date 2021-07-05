using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// �����Ǿ� �ִ� ���̽�ƽ�� ȭ����ü�� ���� �����̴� ���̽�ƽ: 2���� Ÿ��
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

            // moveDir�� �̵����� ����
            moveDir = rt.anchoredPosition - outsideRt.anchoredPosition;
            moveDir = moveDir.normalized;
        }
    }

    // ���� �� ������
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

        // ������
        if (!isFixedStick)
        {
            isPressed = true;
            outsideRt.anchoredPosition = inputPos;
            rt.anchoredPosition = inputPos;
            objJoystickOut.SetActive(true);
            objJoystickIn.SetActive(true);
        }
        else // ����
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

        // ������
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
        else // ����
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
        // �ʱ�ȭ
        moveDir = Vector2.zero;
        inputPos = Vector2.zero;
        offset = Vector2.zero;

        isPressed = false;

        outsideRt.anchoredPosition = useOriginPos;
        rt.anchoredPosition = useOriginPos;
    }

    // �̵� üũ
    public Vector3 GetDir()
    {
        if (isPressed)
        {
            return moveDir;
        }
        else
            return Vector3.zero;
    }

    // �̵��� üũ ( �巡�� �Ŀ� )
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
