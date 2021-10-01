using System;
using UnityEngine;
using SelfishCoder.Core;
using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;

namespace PoolManagement
{
    /// <summary>
    /// 
    /// </summary>
    public class PoolManager : IManager
    {
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<Type, Pool> Pools { get; private set; } = default;
        
        /// <summary>
        /// 
        /// </summary>
        public bool Initialized { get; private set; } = default;

        /// <summary>
        /// 
        /// </summary>
        public void Init()
        {
            Pools = new Dictionary<Type, Pool>();
            Initialized = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void LateInit()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pool"></param>
        public void Add(Pool pool)
        {
            if (!Initialized) Init();
            if (IsNull(pool) || Contains(pool.GetType())) return;
            Pools.Add((pool.Prefab).GetType(), pool);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pool"></param>
        public void Remove(Pool pool)
        {
            if (Contains(pool))
            {
                Pools.Remove(GetPoolType(pool));
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="initialSize"></param>
        /// <param name="maxSize"></param>
        /// <param name="extendable"></param>
        /// <param name="autoInitialize"></param>
        /// <returns></returns>
        public Pool Create(MonoBehaviour prefab, int initialSize, int maxSize, bool extendable = true, bool autoInitialize = true)
        {
            Type genericPoolType = typeof(Pool<>);
            Type[] typeArgs = new Type[] {prefab.GetType()};
            Type poolType = genericPoolType.MakeGenericType(typeArgs);
            object[] args = {prefab, initialSize, maxSize, extendable, autoInitialize};
            Pool pool = Activator.CreateInstance(poolType, args) as Pool;
            Add(pool);
            return pool;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Contains<T>() where T : MonoBehaviour
        {
            return Pools.ContainsKey(typeof(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool Contains(Type type)
        {
            return Pools.ContainsKey(type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pool"></param>
        /// <returns></returns>
        public bool Contains(Pool pool)
        {
            return Pools.ContainsValue(pool);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pool"></param>
        /// <returns></returns>
        public bool IsNull(Pool pool)
        {
            return pool == null;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pool"></param>
        /// <returns></returns>
        public bool IsNullOrEmpty(Pool pool)
        {
            return pool == null || !pool.Initialized;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pool"></param>
        /// <returns></returns>
        public Type GetPoolType(Pool pool)
        {
            return pool.Prefab.GetType();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Pool TryGetPool(Type type)
        {
            return !Contains(type) ? null : Pools[type];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Pool<T> TryGetPool<T>() where T : MonoBehaviour
        {
            if (!Contains(typeof(T))) return null;
            return (Pool<T>) Pools[typeof(T)];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pool"></param>
        public void DestroyPool(Pool pool)
        {
            pool.Clear();
            Object.Destroy(pool.Parent.gameObject);
            Remove(pool);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearAll()
        {
            foreach (Pool pool in Pools.Values)
            {
                DestroyPool(pool);
            }
            Pools.Clear();
        }
    }
}