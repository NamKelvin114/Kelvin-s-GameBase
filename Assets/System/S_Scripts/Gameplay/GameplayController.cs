using System;
using Kelvin;
using Kelvin.ButtonUI;
using UnityEngine;
using UnityEngine.UIElements;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private ScriptableStringEvent loadSceneEvent;
    [Header("Level")]
    [SerializeField]
    private LevelManager levelManager;
    [SerializeField] private Transform levelContainer;

    [Header("UI")] [SerializeField] private ButtonUI homeButton;
    [SerializeField] private ButtonUI nextLevel;
    [SerializeField] private ButtonUI backLevel;
    [SerializeField] private ButtonUI replayLevel;
    private void Awake()
    {
        AudioController.Instance.StopMusic();
        levelManager.LoadCurrentLevel(levelContainer);
    }

    private void OnEnable()
    {
        homeButton.onClick.AddListener(BackToMenu);
        nextLevel.onClick.AddListener(NextLevel);
        backLevel.onClick.AddListener(BackLevel);
        replayLevel.onClick.AddListener(ReplayLevel);
    }

    private void OnDisable()
    {
        homeButton.onClick.RemoveListener(BackToMenu);
        nextLevel.onClick.RemoveListener(NextLevel);
        backLevel.onClick.RemoveListener(BackLevel);
        replayLevel.onClick.RemoveListener(ReplayLevel);
    }

    void BackToMenu()
    {
        loadSceneEvent.Raise(Constant.MENU_SCENE);
    }
    void NextLevel()
    {
        levelManager.NextLevel();
        
    }
    void BackLevel()
    {
        levelManager.BackLevel();
    }
    void ReplayLevel()
    {
        levelManager.ReplayLevel();
    }
}