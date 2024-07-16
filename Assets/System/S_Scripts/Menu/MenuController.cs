using Kelvin.ButtonUI;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private ButtonUI settingButton;

    private void OnEnable()
    {
        settingButton.onClick.AddListener(OpenSetting);
    }

    private void OnDisable()
    {
        settingButton.onClick.RemoveListener(OpenSetting);
    }

    private void OpenSetting()
    {
        PopupController.Instance.ShowPopup(typeof(PopupSetting).ToString());
    }
}