using UnityEngine;

namespace Kelvin
{
    public abstract class CacheGameComponent<T> : MonoBehaviour
    {
        public T component;
        public Transform CachedTransform { get; private set; }

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