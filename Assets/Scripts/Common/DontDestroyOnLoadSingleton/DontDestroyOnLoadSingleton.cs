namespace DCG.Common
{
    using UnityEngine;

    public class DontDestroyOnLoadSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; } = null;

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                DestroyImmediate(this);
                return;
            }

            transform.parent = null;

            Instance = this as T;
            DontDestroyOnLoad(this);
        }
    }
}

