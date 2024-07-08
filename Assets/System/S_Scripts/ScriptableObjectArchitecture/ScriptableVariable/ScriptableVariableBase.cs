using System;
using MasterData;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScriptableObjectArchitecture
{
    [Serializable]
    public class ScriptableVariableBase<T> : ScriptableBase
    {
        [ReadOnly] [SerializeField] protected string iD;

        [SerializeField] protected T initValue;

        [SerializeField] protected bool save;

        private bool _isShowDebugValue;

        [ShowIf(nameof(_isShowDebugValue))] [ReadOnly] [SerializeField]
        private T DebugValue;

        [NonSerialized] protected T runtimeValue;

        public T Value
        {
            get
            {
                if (save)
                {
                    return Data.Load(iD, initValue);
                }

                return runtimeValue;
            }
            set
            {
                if (save)
                {
                    Data.Save(iD, value);
                }
                else
                {
                    runtimeValue = value;
                }
            }
        }

        protected override void DoBeforeSerialize()
        {
        }

        protected override void DoAfterDeserialize()
        {
            runtimeValue = initValue;
        }


        private void Reset()
        {
            iD = Guid.NewGuid().ToString();
        }
    }
}