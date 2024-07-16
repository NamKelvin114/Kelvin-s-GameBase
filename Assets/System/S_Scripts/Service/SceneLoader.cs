using Kelvin;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private ScriptableStringEvent changeSceneEvent;

    private void Awake()
    {
        changeSceneEvent.OnRaise += OnChangeScene;
    }

    private void OnDestroy()
    {
        changeSceneEvent.OnRaise -= OnChangeScene;
    }

    private void OnChangeScene(string sceneName)
    {
        foreach (var scene in GetAllLoadedScene())
            if (!scene.name.Equals(Constant.SERVICE_SCENE))
                SceneManager.UnloadSceneAsync(scene.name);

        Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive).Completed += OnAdditiveSceneLoaded;
    }

    private void OnAdditiveSceneLoaded(AsyncOperationHandle<SceneInstance> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            var sceneName = handle.Result.Scene.name;
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        }
    }

    private Scene[] GetAllLoadedScene()
    {
        var countLoaded = SceneManager.sceneCount;
        var loadedScenes = new Scene[countLoaded];
        for (var i = 0; i < countLoaded; i++) loadedScenes[i] = SceneManager.GetSceneAt(i);
        return loadedScenes;
    }
}