using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAnim : HSSObject
{
    public float effectTime = 0;

    private Animator anim = null;
    private Coroutine co_Anim = null;
    private WaitForSeconds waitTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        waitTime = new WaitForSeconds(effectTime);
    }

    public override void Spawn(Transform parent = null)
    {
        base.Spawn(parent);

        if (co_Anim != null)
            StopCoroutine(co_Anim);

        co_Anim = StartCoroutine(Co_EffectAnim());
    }

    private IEnumerator Co_EffectAnim()
    {
        anim.SetBool(key, true);

        yield return waitTime;

        anim.SetBool(key, false);
        HSSObjectPoolManager.instance.SaveObject(key, gameObject);
    }

    public override void Save()
    {
        base.Save();
    }
}
