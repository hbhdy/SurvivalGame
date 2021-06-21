using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public GameObject objHudPack;
    public GameObject objPlayerHudPack;

    public GameObject MackHudPack()
    {
        GameObject pack = Instantiate(objHudPack, transform.position, transform.rotation, transform);

        pack.SetActive(true);

        return pack;
    }

    public GameObject GetPlayerHudPack()
    {
        return objPlayerHudPack;
    }
}
