using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AssetbundleHelper : EditorWindow
{
    [MenuItem("EasyFramework/Assetbundle/Build Assetbundle")]
    public static void ShowBuildWindow()
    {
        Rect rect = new Rect(0, 0, 500, 500);
        EditorWindow window = EditorWindow.GetWindowWithRect<AssetbundleHelper>(rect, true, "Build Assetbundle");
        window.Show();
    }

    

    string path;
    int option;
    int target;

    string[] options = { "LZMA", "LZ4" };
    string[] targets = { "Window64", "MacOS", "Android", "iOS" };

    GUIStyle titleStyle;
    GUIStyle h1Style;
    GUILayoutOption[] littleSpace;
    GUILayoutOption[] bigSpace;

    private void Awake()
    {
        titleStyle = new GUIStyle();
        titleStyle.fontStyle = FontStyle.Bold;
        titleStyle.fontSize = 20;

        h1Style = new GUIStyle();
        h1Style.fontStyle = FontStyle.Bold;
        h1Style.fontSize = 15;

        littleSpace = new GUILayoutOption[] { GUILayout.Height(2) };

        bigSpace = new GUILayoutOption[] { GUILayout.Height(5) };


        if (PlayerPrefs.GetString("assetbundleoutputpath") == null)
        {
            PlayerPrefs.SetString("assetbundleoutputpath", Application.streamingAssetsPath);
        }

        target = PlayerPrefs.GetInt("assetbundletarget", 0);
        option = PlayerPrefs.GetInt("assetbundleoption", 0);
        path = PlayerPrefs.GetString("assetbundleoutputpath");
    }

    private void OnGUI()
    {

        EditorGUILayout.LabelField("AssetBundle打包工具", titleStyle);
        EditorGUILayout.LabelField("", GUILayout.Height(20));

        EditorGUILayout.LabelField("目标平台", h1Style);
        EditorGUILayout.LabelField("", littleSpace);
        target = EditorGUILayout.Popup(target, targets);

        if (GUI.changed)
        {
            PlayerPrefs.SetInt("assetbundletarget", target);
            GUI.changed = false;
        }

        EditorGUILayout.LabelField("", bigSpace);

        EditorGUILayout.LabelField("压缩方式", h1Style);
        EditorGUILayout.LabelField("", littleSpace);
        option = EditorGUILayout.Popup(option, options);

        if (GUI.changed)
        {
            PlayerPrefs.SetInt("assetbundleoption", option);
            GUI.changed = false;
        }

        EditorGUILayout.LabelField("", bigSpace);



        EditorGUILayout.LabelField("输出路径", h1Style);
        EditorGUILayout.LabelField("", littleSpace);
        path = EditorGUILayout.TextArea(path);

        if (GUI.changed)
        {
            PlayerPrefs.SetString("assetbundleoutputpath", path);
            GUI.changed = false;
        }

        if (GUILayout.Button("打包"))
        {
            BuildAssetbundle(path, option, target);
        }

    }



    static void BuildAssetbundle(string path, int option, int target)
    {
        BuildAssetBundleOptions cusop = BuildAssetBundleOptions.None;
        BuildTarget custg = BuildTarget.StandaloneWindows64;

        switch (option)
        {
            case 0:
                cusop = BuildAssetBundleOptions.None;
                break;
            case 1:
                cusop = BuildAssetBundleOptions.ChunkBasedCompression;
                break;
        }

        switch (target)
        {
            case 0:
                custg = BuildTarget.StandaloneWindows64;
                break;
            case 1:
                custg = BuildTarget.StandaloneOSX;
                break;
            case 2:
                custg = BuildTarget.Android;
                break;
            case 3:
                custg = BuildTarget.iOS;
                break;
        }


        BuildPipeline.BuildAssetBundles(path, cusop, custg);
        AssetDatabase.Refresh();
    }
}
