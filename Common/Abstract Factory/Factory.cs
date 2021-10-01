using System;

namespace SelfishCoder.Common
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Factory<T> : IFactory<T> where T : IProduct
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public abstract T Create(Type type);
    }
}