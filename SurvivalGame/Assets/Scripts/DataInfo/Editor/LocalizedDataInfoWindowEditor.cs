using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LocalizedDataInfoWindowEditor : EditorWindow
{
    private Vector2 objectlistAreaScroll = Vector2.zero;
    private float listAreaWidth = 200.0f;

    private List<LocalizedText> textObject = new List<LocalizedText>();

    private LocalizedText currentText;
    private DataInfoLocalized info;

    private string dataPath = "Assets/DataAsset/";

    private List<string> textKeyList = new List<string>();

    private int currentKeyIndex = 0;
    private string currentKey;
    private string newKey;
    private string[] currentValue = new string[(int)EGameLanuage.Count];

    private List<string> textValueList = new List<string>();
    private string[] keyValue;

    private bool isSceneView = false;
    //private DoubleKeyData tempKeyData = null;

    //[MenuItem("HSS/UI Text Editor")]
    //static void Init()
    //{
    //    LocalizedDataInfoWindowEditor window = (LocalizedDataInfoWindowEditor)EditorWindow.GetWindow(typeof(LocalizedDataInfoWindowEditor));
    //    window.titleContent.text = "UI Text Editor";
    //}

    //public void OnEnable()
    //{
    //    LoadLocalizedTextData();
    //}
    //public void OnDisable()
    //{
    //    //EditorUtility.SetDirty(info);
    //    AssetDatabase.SaveAssets();
    //}
    //public void LoadLocalizedTextData()
    //{
    //    //info = AssetDatabase.LoadAssetAtPath(dataPath + "LocalizedDataInfo.Asset", typeof(LocalizedDataInfo)) as LocalizedDataInfo;

    //    //if (info == null)
    //    //{
    //    //    Debug.Log("NewAsset!");
    //    //    info = CreateInstance<LocalizedDataInfo>();
    //    //    AssetDatabase.CreateAsset(info, dataPath + "LocalizedDataInfo.Asset");
    //    //    AssetDatabase.SaveAssets();
    //    //}
    //}

    //public void OnGUI()
    //{
    //    GUILayout.BeginHorizontal(GUIStyle.none);

    //    GUI.color = Color.green;
    //    GUILayout.Label("UI Text Editor", EditorStyles.boldLabel, GUILayout.Width(200));
    //    GUI.color = Color.white;

    //    if (GUILayout.Button("Add Data", GUILayout.Width(100)))
    //    {
    //        int rand = Random.Range(0, 1000);
    //        for (int i = 0; i < (int)EGameLanuage.Count; ++i)
    //        {
    //            info.AddData("New" + rand.ToString(), ((EGameLanuage)i).ToString(), "");
    //        }
    //    }

    //    GUILayout.EndHorizontal();

    //    GUILayout.BeginHorizontal();

    //    DrawDataView();

    //    GUILayout.EndHorizontal();
    //}

    //public void DrawDataView()
    //{
    //    objectlistAreaScroll = GUILayout.BeginScrollView(objectlistAreaScroll, "box");

    //    GUILayout.BeginHorizontal(GUIStyle.none);
    //    GUILayout.Space(20);
    //    GUILayout.Label("[ Key ]", GUILayout.Width(200));
    //    GUILayout.Label("[ " + EGameLanuage.Korean.ToString() + " ]", GUILayout.Width(200));
    //    GUILayout.Label("[ " + EGameLanuage.English.ToString() + " ]", GUILayout.Width(200));
    //    GUILayout.Label("[ " + EGameLanuage.Japanese.ToString() + " ]", GUILayout.Width(200));

    //    GUILayout.EndHorizontal();

    //    tempKeyData = null;

    //    for(int i = 0; i < info.dataList.Count; i++)
    //    {
    //        GUILayout.BeginHorizontal(GUIStyle.none);
    //        if(GUILayout.Button("-",GUILayout.Width(20)))
    //        {
    //            tempKeyData = info.dataList[i];
    //            break;
    //        }
    //        Color prev = GUI.color;
    //        GUI.color = Color.yellow;
    //        info.dataList[i].mainKey = EditorGUILayout.TextField(info.dataList[i].mainKey, GUILayout.Width(200));
    //        GUI.color = prev;
    //        for(int j = 0; j < (int)EGameLanuage.Count; j++)
    //        {
    //            info.dataList[i].valueList[j].value = EditorGUILayout.TextField(info.dataList[i].valueList[j].value, GUILayout.Width(200));
    //        }
    //        GUILayout.EndHorizontal();
    //    }
    //    GUILayout.EndScrollView();

    //    if (tempKeyData != null)
    //    {
    //        info.dataList.Remove(tempKeyData);
    //    }
    //}
}
