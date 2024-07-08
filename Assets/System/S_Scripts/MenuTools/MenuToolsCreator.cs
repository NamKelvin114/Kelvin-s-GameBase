using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace MenuTools
{
    public static class MenuToolsCreator
    {
        [MenuItem("Tools/Open Persistent Data Path", false, 31000), UsedImplicitly]
        private static void OpenPersistentDataPath()
        {
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        }
    }
}