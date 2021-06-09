using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{


    public void Update()
    {
        if (!Core.instance.isCoreReady)
            return;

        if (Core.INPUT.isPressed)
        {
            Joystick.instance.OnPointerDown(Core.INPUT.inputPosition);
        }

        if(Core.INPUT.isDrag)
        {
            Joystick.instance.OnDrag(Core.INPUT.inputPosition);

        }

        if (Core.INPUT.isRelease)
        {
            Joystick.instance.OnPointerUp(Core.INPUT.inputPosition);

        }
    }
}
