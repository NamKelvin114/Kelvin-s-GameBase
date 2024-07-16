using System;
using ScriptableObjectArchitecture;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableVariable/FloatVariable", fileName = "float_variable")]
[Serializable]
public class ScriptableFloatVariable : ScriptableVariableBase<float>
{
}