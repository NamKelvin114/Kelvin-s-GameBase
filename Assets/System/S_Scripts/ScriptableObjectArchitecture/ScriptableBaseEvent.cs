using System;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public abstract class ScriptableBaseEvent<T> : ScriptableObject, ISerializationCallbackReceiver
    {
        protected Action<T> _onRaise;

        public void OnBeforeSerialize()
        {
            DoBeforeSerialize();
        }

        public void OnAfterDeserialize()
        {
            DoAfterDeserialize();
        }

        public event Action<T> OnRaise
        {
            add => _onRaise += value;
            remove => _onRaise -= value;
        }

        public void Raise(T value)
        {
            _onRaise?.Invoke(value);
        }

        protected virtual void DoBeforeSerialize()
        {
        }

        protected virtual void DoAfterDeserialize()
        {
        }
    }
}