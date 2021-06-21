using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : HSSUIObject
{
    public Text uiDamage;

    private Vector3 dir = Vector3.zero;
    private float fadeTime = 1f;
    private float time = 0;

    public override void Spawn(Transform parent = null)
    {
        base.Spawn(parent);

        StartCoroutine(DamageRoutine());
    }
    public void SetText(int damage)
    {
        if (damage == 0)
        {
            uiDamage.text = "miss";
        }
        else
        {
            uiDamage.text = damage.ToString();
        }
    }

    public IEnumerator DamageRoutine()
    {
        time = 0;
        dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        while (time <= fadeTime)
        {
            transform.Translate(dir * Time.deltaTime);

            uiDamage.color = new Color(1, 1, 1, 1f - (time / fadeTime));

            time += Time.deltaTime;
            yield return null;
        }

        SelfDestroy();
    }

    public void SelfDestroy()
    {
        uiDamage.color = Color.white;
        dir = Vector3.zero;
        transform.position = Vector3.zero;

        HSSUIObjectPoolManager.instance.SaveObject(key, gameObject);
    }
}
