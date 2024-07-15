using System;
using Kelvin.MasterData;
using NaughtyAttributes;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [Serializable]
    public class ScriptableVariableBase<T> : ScriptableBase
    {
        [ReadOnly] [SerializeField] protected string iD;

        [SerializeField] protected T initValue;

        [SerializeField] protected bool save;

        [ShowIf(nameof(_isShowDebugValue))] [ReadOnly] [SerializeField]
        private T DebugValue;

        private bool _isShowDebugValue;

        [NonSerialized] protected T runtimeValue;

        public T Value
        {
            get
            {
                if (save) return Data.Load(iD, initValue);

                return runtimeValue;
            }
            set
            {
                if (save)
                    Data.Save(iD, value);
                else
                    runtimeValue = value;
            }
        }


        private void Reset()
        {
            iD = Guid.NewGuid().ToString();
        }

        protected override void DoBeforeSerialize()
        {
        }

        protected override void DoAfterDeserialize()
        {
            runtimeValue = initValue;
        }
    }
}