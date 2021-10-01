using UnityEngine;

namespace SelfishCoder.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        protected static T current;

        /// <summary>
        /// 
        /// </summary>
        public static T Current
        {
            get
            {
                if (current) return current;
                current = FindObjectOfType<T>();
                if (!current)
                {
                    current = new GameObject(typeof(T).Name).AddComponent<T>();
                }
                return current;
            }
            protected set
            {
                if (current)
                {
                    Destroy(value.gameObject);
                    return;
                }

                current = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void Awake()
        {
            Current = this as T;
        }
    }
}