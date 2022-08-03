using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetting : MonoBehaviour
{
    public static PlayerSetting instance = null;

    private GameObject objPlayer;

    public GameObject objStartMarker;

    public AssembleData dummyPlayer;

    [HideInInspector]
    public Player player;

    [HideInInspector]
    public AssembleData inGamePlayer = new AssembleData();

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public IEnumerator AssemblePlayer()
    {
        GameObject player = new GameObject();

        player.transform.position = objStartMarker.transform.position;
        player.transform.rotation = objStartMarker.transform.rotation;

        player.name = "Player";

        yield return null;
    }

    public IEnumerator AssemblePlayerDev()
    {
        GameObject obj = new GameObject();

        obj.transform.position = objStartMarker.transform.position;
        obj.transform.rotation = objStartMarker.transform.rotation;

        obj.name = "Player";

        AssembleDataToIngame(dummyPlayer);
        yield return StartCoroutine(obj.AddComponent<Player>().InitPayer(dummyPlayer));

        objPlayer = obj;
        player = objPlayer.GetComponent<Player>();

        yield return null;
    }

    public void AssembleDataToIngame(AssembleData data)
    {
        inGamePlayer.bodyData.id = data.bodyData.id;
        inGamePlayer.bodyData.key = data.bodyData.key;
        inGamePlayer.bodyData.level = data.bodyData.level;

        inGamePlayer.wheelData.id = data.wheelData.id;
        inGamePlayer.wheelData.key = data.wheelData.key;
        inGamePlayer.wheelData.level = data.wheelData.level;    
    }

    public void PlayerResetPosition()
    {
        player.objWheel.transform.position = objStartMarker.transform.position;
    }
}
