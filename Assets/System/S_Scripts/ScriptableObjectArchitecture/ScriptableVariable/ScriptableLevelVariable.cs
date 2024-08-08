using System;
using ScriptableObjectArchitecture;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableVariable/LevelVariable", fileName = "level_variable")]
[Serializable]
public class ScriptableLevelVariable : ScriptableVariableBase<Level>
{
}