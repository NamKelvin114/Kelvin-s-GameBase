using ScriptableObjectArchitecture;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableVariable/IntVariable", fileName = "int_variable")]
[System.Serializable]
public class ScriptableIntVariable : ScriptableVariableBase<int>
{
}