using UnityEngine;

namespace Kelvin
{
    public abstract class CacheGameComponent<T> : MonoBehaviour
    {
        public UnityEngine.Transform CachedTransform { get; private set; }
        public T component;

        protected virtual void Awake()
        {
            if (CachedTransform == null) CachedTransform = transform;
            GetReference();
        }

        protected virtual void Reset()
        {
            GetReference();
        }

        private void GetReference()
        {
            if (component == null) component = GetComponent<T>();
        }
    }
}