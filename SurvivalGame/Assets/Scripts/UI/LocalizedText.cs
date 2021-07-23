using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    public string key;

    private bool isText = false;

    private bool isReady = false;

    public void Start()
    {
        if(GetComponent<Text>() != null)
        {
            isText = true;
        }

        if (isText)
        {
            Text txt = GetComponent<Text>();

            txt.text = Core.RSS.GetLocalUIText(key);
            txt.text = txt.text.Replace("\\n", "\n");
        }
        else
        {
            TextMesh txt = GetComponent<TextMesh>();

            txt.text = Core.RSS.GetLocalUIText(key);
            txt.text = txt.text.Replace("\\n", "\n");
        }

        isReady = true;
    }

    public void UpdateText(string newKey)
    {
        key = newKey;

        if (GetComponent<Text>() != null)
            isText = true;

        if (isText)
        {
            Text txt = GetComponent<Text>();

            txt.text = Core.RSS.GetLocalUIText(key);
            txt.text = txt.text.Replace("\\n", "\n");
        }
        else
        {
            TextMesh txt = GetComponent<TextMesh>();

            txt.text = Core.RSS.GetLocalUIText(key);
            txt.text = txt.text.Replace("\\n", "\n");
        }
    }

    public void UpdateLanuageSetting()
    {
        if (!isReady)
            return;

        if (isText)
        {
            Text txt = GetComponent<Text>();

            txt.text = Core.RSS.GetLocalUIText(key);
            txt.text = txt.text.Replace("\\n", "\n");
        }
        else
        {
            TextMesh txt = GetComponent<TextMesh>();

            txt.text = Core.RSS.GetLocalUIText(key);
            txt.text = txt.text.Replace("\\n", "\n");               
        }
    }
}
