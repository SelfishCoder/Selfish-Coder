using System.Linq;
using System.Collections.Generic;

namespace SelfishCoder.Extensions.System
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumerable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FindObjectOfType<T>(this IEnumerable<object> enumerable)
        {
            return (T) enumerable.First(entity => entity.GetType() == typeof(T));
        }
    }
}