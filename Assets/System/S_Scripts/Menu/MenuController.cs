using Kelvin;
using Kelvin.ButtonUI;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [Header("UI")] [SerializeField] private ButtonUI settingButton;

    [SerializeField] private ButtonUI playButton;

    [Header("Music")] [SerializeField] private MusicData menuMusic;

    [Header("Event")] [SerializeField] private ScriptableStringEvent loadSceneEvent;

    private void OnEnable()
    {
        settingButton.onClick.AddListener(OpenSetting);
        playButton.onClick.AddListener(PlayGame);
        AudioController.Instance.PlayMusic(menuMusic);
    }

    private void OnDisable()
    {
        settingButton.onClick.RemoveListener(OpenSetting);
        playButton.onClick.RemoveListener(PlayGame);
    }

    private void OpenSetting()
    {
        PopupController.Instance.ShowPopup(typeof(PopupSetting));
    }

    private void PlayGame()
    {
        loadSceneEvent?.Raise(Constant.GAMEPLAY_SCENE);
    }
}