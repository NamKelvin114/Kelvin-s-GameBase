using System;
using Kelvin;
using Kelvin.MasterData;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScriptableObjectArchitecture
{
    [Serializable]
    public class ScriptableVariableBase<T> : ScriptableBaseEvent<T>
    {
        [ReadOnly] [SerializeField] protected string iD;

        public T initValue;

        [SerializeField] protected bool save;

        [ShowIf(nameof(_isShowDebugValue))] [ReadOnly] [SerializeField]
        private T debugValue;

        [SerializeField] private ResetType resetOn = ResetType.SceneLoaded;

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
                debugValue = value;
                if (_onRaise != null) _onRaise.Invoke(value);
            }
        }


        private void Reset()
        {
            iD = Guid.NewGuid().ToString();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if ((resetOn == ResetType.SceneLoaded && mode == LoadSceneMode.Single) ||
                (resetOn == ResetType.AdditiveSceneLoaded && mode == LoadSceneMode.Additive))
                if (!save)
                    Value = initValue;

            debugValue = Value;
            _isShowDebugValue = true;
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