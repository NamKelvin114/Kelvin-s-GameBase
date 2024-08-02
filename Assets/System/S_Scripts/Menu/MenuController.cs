using Kelvin.ButtonUI;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private ButtonUI settingButton;
    [SerializeField] private MusicData menuMusic;

    private void OnEnable()
    {
        settingButton.onClick.AddListener(OpenSetting);
        AudioController.Instance.PlayMusic(menuMusic);
    }

    private void OnDisable()
    {
        settingButton.onClick.RemoveListener(OpenSetting);
    }

    private void OpenSetting()
    {
        PopupController.Instance.ShowPopup(typeof(PopupSetting));
    }
}