using Kelvin.ButtonUI;
using UnityEngine;

public class PopupSetting : Popup
{
    [SerializeField] private ButtonUI buttonUI;

    private void OnEnable()
    {
        buttonUI.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        buttonUI.onClick.RemoveListener(Close);
    }
}