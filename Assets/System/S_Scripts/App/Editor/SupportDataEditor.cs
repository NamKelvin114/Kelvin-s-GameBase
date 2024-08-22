using Kelvin.MasterData;
using UnityEditor;
using UnityEngine;

public static class SupportDataEditor
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        SupportSaveDataInEditor();
    }

    private static void SupportSaveDataInEditor()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode) Data.SaveAll();
    }
}