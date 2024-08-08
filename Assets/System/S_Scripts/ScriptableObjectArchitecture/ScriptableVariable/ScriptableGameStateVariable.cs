using System;
using ScriptableObjectArchitecture;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableVariable/GameStateVariable", fileName = "game_state_variable")]
[Serializable]
public class ScriptableGameStateVariable : ScriptableVariableBase<GameState>
{
}

public enum GameState
{
    WinProcess,
    LoseProcess,
    LoadingLevel,
    PLayingLevel,
    Pause
}