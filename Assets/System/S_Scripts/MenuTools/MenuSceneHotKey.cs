using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Kelvin.Editor
{
    public static class MenuScene
    {
        [MenuItem("Tools/Open Scene/Loading #2", priority = 500)]
        [UsedImplicitly]
        private static void OpenLauncherScene()
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                EditorSceneManager.OpenScene("Assets/System/S_Scenes/Loading.unity");
        }

        [MenuItem("Tools/Open Scene/Service #5", priority = 503)]
        [UsedImplicitly]
        private static void OpenPersistentScene()
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                EditorSceneManager.OpenScene("Assets/System/S_Scenes/Service.unity");
        }

        [MenuItem("Tools/Open Scene/Menu #3", priority = 501)]
        [UsedImplicitly]
        private static void OpenMenuScene()
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                EditorSceneManager.OpenScene("Assets/System/S_Scenes/Menu.unity");
        }

        [MenuItem("Tools/Open Scene/Gameplay #4", priority = 502)]
        [UsedImplicitly]
        private static void OpenGameplayScene()
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                EditorSceneManager.OpenScene("Assets/System/S_Scenes/Gameplay.unity");
        }
    }
}