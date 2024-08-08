using System;using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Kelvin;
using Kelvin.MasterData;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Data/Level", fileName = "levelmanager")]
public class LevelManager : ScriptableObject
{
    [Header("Levels")] [SerializeField] private List<Level> levels;
    [SerializeField] private ScriptableLevelVariable currentLevel;

    [Header("Properties")] [SerializeField]
    private ScriptableIntVariable currentLevelIndex;
    [SerializeField] private LoadLevelMode loadLevelMode;
    [SerializeField] private ScriptableGameStateVariable gameState;
    private Transform _currentLevelContainer;
    
    public async void LoadCurrentLevel(Transform levelContainer)
    {
        _currentLevelContainer = levelContainer;
        gameState.Value = GameState.LoadingLevel;
        var loadAsset = await Addressables.LoadAssetAsync<GameObject>(levels[currentLevelIndex.Value-1].GameObject().name);
        currentLevel.Value = loadAsset.GetComponent<Level>();
        if (_currentLevelContainer.childCount != 0) _currentLevelContainer.RemoveAllChildren();
        Instantiate(currentLevel.Value, _currentLevelContainer);
        gameState.Value = GameState.PLayingLevel;
    }

    public void NextLevel()
    {
        currentLevelIndex.Value++;
        CheckLevel();
        Data.SaveAll();
        LoadCurrentLevel(_currentLevelContainer);
    }

    void CheckLevel()
    {
        if (currentLevelIndex.Value>=levels.Count)
        {
            switch (loadLevelMode)
            {
                case LoadLevelMode.LoopRandom:
                    var getRandomLevel = Random.Range(0, levels.Count);
                    currentLevelIndex.Value = getRandomLevel+1;
                    break;
                case LoadLevelMode.LoopSequence:
                    currentLevelIndex.Value = currentLevelIndex.initValue ;
                    break;
            }
        }
    }

    public void SetCurrentLevel(int indexLevel)
    {
        currentLevelIndex.Value = indexLevel;
    }

    public void SetCurrentLevel(Level getLevel)
    {
        for (int i = 0; i < levels.Count; i++)
        {
            if (levels[i]==getLevel)
            {
                currentLevelIndex.Value = i + 1;
                break;
            }
        }
    }

    public void CompleteLevel()
    {
    }

    public void ReplayLevel()
    {
        LoadCurrentLevel(_currentLevelContainer);
    }
}

public enum LoadLevelMode
{
    LoopSequence,
    LoopRandom,
}