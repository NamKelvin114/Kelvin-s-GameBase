using Kelvin.ButtonUI;
using UnityEngine;

public class PopupSetting : Popup
{
    [SerializeField] private ButtonUI buttonUI;

    private void Start()
    {
        buttonUI.OnButtonDown.AddListener(ButtonDown);
        buttonUI.OnButtonHold.AddListener(HoldButton);
    }

    private void ButtonDown()
    {
        Debug.Log("DownButton");
    }

    private void HoldButton()
    {
        Debug.Log("HoldButton");
    }
}