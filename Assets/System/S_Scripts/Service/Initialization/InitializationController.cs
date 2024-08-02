using System.Collections.Generic;
using UnityEngine;

public class InitializationController : MonoBehaviour
{
    [SerializeField] private List<Initialize> initializes;
    [SerializeField] private ScriptableBoolVariable isLoadingCompleted;
    private int _countInitComplete;

    private void Awake()
    {
        foreach (var initComponent in initializes) initComponent.Init(CheckloadComplete);
    }

    private void CheckloadComplete(bool loadCompleted)
    {
        if (loadCompleted)
        {
            _countInitComplete++;
            if (_countInitComplete == initializes.Count) isLoadingCompleted.Value = true;
        }
    }
}