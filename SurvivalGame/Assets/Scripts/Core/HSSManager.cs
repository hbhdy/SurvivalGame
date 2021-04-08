using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSSManager : MonoBehaviour
{
    protected bool isReady = false;

    public virtual IEnumerator InitManager()
    {
        yield return null;
    }

    public virtual IEnumerator ManagerInitProcessing()
    {
        isReady = true;

        Debug.Log(gameObject.name + " Init Complete");

        yield return null;
    }
}
