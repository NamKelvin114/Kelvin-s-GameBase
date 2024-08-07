using Kelvin;
using Kelvin.ButtonUI;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private ScriptableStringEvent loadSceneEvent;

    [Header("UI")] [SerializeField] private ButtonUI homeButton;

    private void OnEnable()
    {
        homeButton.onClick.AddListener(BackToMenu);
    }

    private void OnDisable()
    {
        homeButton.onClick.RemoveListener(BackToMenu);
    }

    public void BackToMenu()
    {
        loadSceneEvent.Raise(Constant.MENU_SCENE);
    }
}