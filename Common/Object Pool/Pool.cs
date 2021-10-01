using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace PoolManagement
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Pool
    {
        /// <summary>
        /// 
        /// </summary>
        public MonoBehaviour Prefab { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public int InitialSize { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public int MaximumSize { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public int CurrentSize { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Expendable { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Initialized { get; protected set; }


        /// <summary>
        /// 
        /// </summary>
        public Transform Parent { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        private Pool()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefab"></param>
        protected Pool(MonoBehaviour prefab)
        {
            Prefab = prefab;
        }

        /// <summary>
        /// 
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract MonoBehaviour Request();

        /// <summary>
        /// 
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnedObject"></param>
        public abstract void Return(MonoBehaviour returnedObject);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pool<T> : Pool where T : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        public List<T> Objects { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public new T Prefab { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        private Pool(T prefab) : base(prefab)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="initialSize"></param>
        /// <param name="maximumSize"></param>
        /// <param name="extendable"></param>
        /// <param name="autoInitialize"></param>
        public Pool(T prefab, int initialSize, int maximumSize, bool extendable = true, bool autoInitialize = true) : base(prefab)
        {
            this.Prefab = prefab;
            this.InitialSize = initialSize;
            this.MaximumSize = maximumSize;
            this.Expendable = extendable;
            this.Parent = new GameObject($"{Prefab.GetType().Name} Pool").transform;
            if (autoInitialize) Init();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Init()
        {
            if (Initialized) return;

            Objects = new List<T>();
            for (int i = 0; i < InitialSize; i++)
            {
                T clone = Object.Instantiate(Prefab, Parent, true);
                clone.gameObject.SetActive(false);
                Objects.Add(clone);
                CurrentSize = InitialSize;
            }

            Initialized = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override MonoBehaviour Request()
        {
            if (!Initialized) Init();

            foreach (T poolObject in Objects.Where(poolObject => !poolObject.gameObject.activeInHierarchy))
            {
                poolObject.gameObject.SetActive(true);
                return poolObject;
            }

            if (CurrentSize == MaximumSize && !Expendable) return null;

            T newPoolObject = Extend();
            newPoolObject.gameObject.SetActive(true);
            return newPoolObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnedObject"></param>
        public override void Return(MonoBehaviour returnedObject)
        {
            returnedObject.gameObject.SetActive(false);
            returnedObject.transform.SetParent(Parent);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Clear()
        {
            foreach (T monoBehaviour in Objects)
            {
                Object.Destroy(monoBehaviour);
            }

            Objects.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private T Extend()
        {
            T poolObject = CreateObject();
            MaximumSize = CurrentSize;
            return poolObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private T CreateObject()
        {
            T poolObject = Object.Instantiate(Prefab, Parent);
            Objects.Add(poolObject);
            poolObject.gameObject.SetActive(false);
            CurrentSize++;
            return poolObject;
        }
    }
}