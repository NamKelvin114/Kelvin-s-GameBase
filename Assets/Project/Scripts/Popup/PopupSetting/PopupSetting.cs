using Kelvin.ButtonUI;
using UnityEngine;

public class PopupSetting : Popup
{
    [SerializeField] private ButtonUI buttonUI;

    private void OnEnable()
    {
        buttonUI.onClick.AddListener(Hide);
    }

    private void OnDisable()
    {
        buttonUI.onClick.RemoveListener(Hide);
    }
}