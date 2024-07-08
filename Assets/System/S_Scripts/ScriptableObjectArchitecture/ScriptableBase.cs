using System;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public abstract class ScriptableBase : ScriptableObject, ISerializationCallbackReceiver
    {
        public void OnBeforeSerialize()
        {
            DoBeforeSerialize();
        }

        public void OnAfterDeserialize()
        {
            DoAfterDeserialize();
        }

        protected abstract void DoBeforeSerialize();
        protected abstract void DoAfterDeserialize();
    }
}