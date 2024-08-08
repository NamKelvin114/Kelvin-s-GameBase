using System;
using Kelvin;
using Kelvin.MasterData;
using NaughtyAttributes;
using UnityEditor;
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
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += OnPlayModeChange;
#endif
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= OnPlayModeChange;
#endif
        }

        protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if ((resetOn == ResetType.SceneLoaded && mode == LoadSceneMode.Single) ||
                (resetOn == ResetType.AdditiveSceneLoaded && mode == LoadSceneMode.Additive))
                if (!save)
                    Value = initValue;
        }

        protected override void DoBeforeSerialize()
        {
        }

        protected override void DoAfterDeserialize()
        {
            runtimeValue = initValue;
        }

        private void OnPlayModeChange(PlayModeStateChange playModeStateChange)
        {
            if (playModeStateChange == PlayModeStateChange.ExitingEditMode)
            {
                if (!save) debugValue = initValue;
                _isShowDebugValue = true;
            }
            else if (playModeStateChange == PlayModeStateChange.EnteredEditMode)
            {
                _isShowDebugValue = false;
            }
        }
    }
}