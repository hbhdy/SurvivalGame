using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItem : HSSObject
{
    public int exp = 0;

    private bool isReady = false;
    private string layerCheck = "Player";

    private void OnEnable()
    {
        isReady = false;
    }

    public override void Spawn(Transform parent = null)
    {
        base.Spawn(parent);

        isReady = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReady == true)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                PlayerSetting.instance.AddExp(exp);
                HSSObjectPoolManager.instance.SaveObject(key, gameObject);
                isReady = false;
            }
        }
    }

    //public void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (isReady == true)
    //    {
    //        if (collision.gameObject.layer.Equals(layerCheck))
    //        {
    //            HSSObjectPoolManager.instance.SaveObject(key, gameObject);
    //            isReady = false;
    //        }
    //    }
    //}

}
