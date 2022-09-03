using UnityEngine;
using UnityEditor;

public class CharacterNameWindowEditor : EditorWindow
{
    private CharacterNameAsset nameAsset;
    private Vector2 objectlistAreaScroll = Vector2.zero;

    private string dataPath = "Assets/Resources/ScriptableObject/";

    [MenuItem("HSS/Character Name Data")]
    static void Init()
    {
        CharacterNameWindowEditor window = (CharacterNameWindowEditor)EditorWindow.GetWindow(typeof(CharacterNameWindowEditor));
        window.titleContent.text = "Character Name Editor";
    }

    private void OnEnable()
    {
        LoadCharaterNameData();
    }

    private void OnDisable()
    {
        EditorUtility.SetDirty(nameAsset);
        AssetDatabase.SaveAssets();
    }

    public void LoadCharaterNameData()
    {
        nameAsset = AssetDatabase.LoadAssetAtPath(dataPath + "CharaterNameText.Asset", typeof(CharacterNameAsset)) as CharacterNameAsset;

        if (nameAsset == null)
        {
            Debug.Log("NewAsset!");
            nameAsset = CreateInstance<CharacterNameAsset>();
            AssetDatabase.CreateAsset(nameAsset, dataPath + "CharaterNameText.Asset");
            AssetDatabase.SaveAssets();
        }
    }

    public void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUI.color = Color.green;
        GUILayout.Label("Character Name Text Editor", EditorStyles.boldLabel, GUILayout.Width(200));
        GUI.color = Color.white;

        GUILayout.Space(10);

        if (GUILayout.Button("Name Add", GUILayout.Width(100)))
        {
            nameAsset.nameData.Add(new NameTextData());
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Name Remove", GUILayout.Width(100)))
        {
            nameAsset.nameData.RemoveRange(nameAsset.nameData.Count - 1, 1);
        }

        GUILayout.EndHorizontal();

        DrawDataView();
    }

    public void DrawDataView()
    {
        objectlistAreaScroll = GUILayout.BeginScrollView(objectlistAreaScroll, "box");

        GUILayout.BeginHorizontal(GUIStyle.none);
        GUILayout.Space(40);
        GUILayout.Label("[ Key ]", GUILayout.Width(150));
        GUILayout.Label("[ " + EGameLanuage.Korean.ToString() + " ]", GUILayout.Width(150));
        GUILayout.Label("[ " + EGameLanuage.English.ToString() + " ]", GUILayout.Width(150));
        GUILayout.Label("[ " + EGameLanuage.Japanese.ToString() + " ]", GUILayout.Width(150));
        GUILayout.EndHorizontal();

        for (int i = 0; i < nameAsset.nameData.Count; i++)
        {
            GUILayout.BeginHorizontal();
            GUI.enabled = false;
            EditorGUILayout.TextField(i + "¹ø", GUILayout.Width(40));
            GUI.enabled = true;
            GUI.color = Color.yellow;
            nameAsset.nameData[i].editorName = EditorGUILayout.TextField(nameAsset.nameData[i].editorName, GUILayout.Width(150));
            GUI.color = Color.white;
            for (int j = 0; j < (int)EGameLanuage.Count; j++)
            {
                nameAsset.nameData[i].viewName[j] = EditorGUILayout.TextField(nameAsset.nameData[i].viewName[j], GUILayout.Width(150));
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(5);
        }

        GUILayout.EndScrollView();
    }
}