using Cysharp.Threading.Tasks;
using Kelvin;
using NaughtyAttributes;
using PrimeTween;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    [SerializeField] private Slider circleSlider;
    [SerializeField] private Slider horizonSlider;
    [SerializeField] [Range(0.5f, 2f)] private float timeLoading;
    [SerializeField] private ScriptableBoolVariable isLoadingCompleted;
    [SerializeField] private ScriptableStringEvent loadSceneEvent;

    private void Awake()
    {
        LoadingProcess();
    }

    private async UniTask LoadingProcess()
    {
        await UniTask.WhenAll(Initialization(), HandleUI());
    }

    private async UniTask Initialization()
    {
        await Addressables.LoadSceneAsync(Constant.SERVICE_SCENE, LoadSceneMode.Additive);
    }

    private void LoadNextScene()
    {
        UniTask.WaitUntil(() => isLoadingCompleted.Value);
        loadSceneEvent.Raise(Constant.MENU_SCENE);
    }

    private async UniTask HandleUI()
    {
        if (circleSlider.gameObject.activeSelf)
            Tween.UISliderValue(circleSlider, 1f, timeLoading, Ease.OutCirc).OnComplete(LoadNextScene, false);
        else
            Tween.UISliderValue(horizonSlider, 1f, timeLoading, Ease.OutCirc).OnComplete(LoadNextScene, false);
        ;
    }

    private void UseTypeLoading(bool isHori)
    {
        circleSlider.gameObject.SetActive(!isHori);
        horizonSlider.gameObject.SetActive(isHori);
    }
#if UNITY_EDITOR
    [Button]
    public void UseCircleLoading()
    {
        UseTypeLoading(false);
    }

    [Button]
    public void UseHorizonLoading()
    {
        UseTypeLoading(true);
    }

#endif
}