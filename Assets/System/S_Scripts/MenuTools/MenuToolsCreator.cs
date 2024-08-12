using System;
using JetBrains.Annotations;
using Kelvin.MasterData;
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
        [MenuItem("Tools/Clear Persistent Data Path", false, 31000), UsedImplicitly]
        private static void ClearPersistentDataPath()
        {
            if (EditorUtility.DisplayDialog("Clear Persistent Data Path",
                "Are you sure you wish to clear the persistent data path?\n This action cannot be reversed.",
                "Clear",
                "Cancel"))
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Application.persistentDataPath);

                foreach (var file in di.GetFiles()) file.Delete();
                foreach (var dir in di.GetDirectories()) dir.Delete(true);
                try
                {
                    Data.DeleteAll();
                }
                catch (Exception)
                {
                    //
                }
            }
        }
    }
}