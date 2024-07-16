using System;
using UnityEngine;

public abstract class Initialize : MonoBehaviour
{
    public virtual void Init(Action<bool> loadComplete)
    {
        loadComplete?.Invoke(true);
    }
}