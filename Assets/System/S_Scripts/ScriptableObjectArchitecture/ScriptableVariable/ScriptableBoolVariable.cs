using System;
using ScriptableObjectArchitecture;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableVariable/BoolVariable", fileName = "bool_variable")]
[Serializable]
public class ScriptableBoolVariable : ScriptableVariableBase<bool>
{
}