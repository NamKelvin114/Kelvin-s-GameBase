using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Level", fileName = "levelmanager")]
public class LevelManager : ScriptableObject
{
    [SerializeField] private ScriptableIntVariable currentLevel;
    [SerializeField] private List<Level> levels;
}