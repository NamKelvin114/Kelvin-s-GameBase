using Kelvin.ButtonUI;
using UnityEngine;
using UnityEngine.UI;

public class PopupSetting : Popup
{
    [SerializeField] private ButtonUI buttonUI;
    [SerializeField] private ScriptableFloatVariable musicVolume;
    [SerializeField] private ScriptableFloatVariable soundFXVolume;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundFXSlider;

    private void OnEnable()
    {
        buttonUI.onClick.AddListener(Hide);
        musicSlider.value = musicVolume.Value;
        soundFXSlider.value = soundFXVolume.Value;
    }

    private void OnDisable()
    {
        buttonUI.onClick.RemoveListener(Hide);
    }
    public void MusicVolumeChange(float value)
    {
        musicVolume.Value = value;
    }
    public void SoundFXVolumeChange(float value)
    {
        soundFXVolume.Value = value;
    }
}