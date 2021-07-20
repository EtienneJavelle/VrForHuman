using UnityEngine;

namespace Etienne {
    public abstract class Singleton<T> : MonoBehaviour where T : class {
        public static T Instance => instance;

        [Header("Singleton")]
        [SerializeField] private bool isPersistant = false;

        protected virtual void Awake() {
            if(Singleton<T>.instance != null) {
                Debug.LogError($"There are more than one \"{typeof(T).Name}\" singleton.", gameObject);
                GameObject.Destroy(this);
                return;
            }
            Singleton<T>.instance = this as T;

            if(isPersistant) {
                DontDestroyOnLoad(this);
            }
        }

        private static T instance;
    }
}