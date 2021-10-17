using System;
using UnityEngine;
using System.Collections;
using SelfishCoder.Common;

namespace SelfishCoder.Util
{
    /// <summary>
    /// Action based invoker.
    /// </summary>
    public class Invoker : Singleton<Invoker>
    {
        /// <summary>
        /// Invokes the given action
        /// </summary>
        /// <param name="action"></param>
        public void Invoke(Action action)
        {
            action?.Invoke();
        }

        /// <summary>
        /// Invokes the given action with the specified delay
        /// </summary>
        /// <param name="action"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public Coroutine Invoke(Action action, float delay)
        {
            return Current.StartCoroutine(InvokeWithDelay(action, delay));
        }

        /// <summary>
        /// Coroutine that waits for the specified delay and invokes the given action
        /// </summary>
        /// <param name="action"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        private IEnumerator InvokeWithDelay(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            Invoke(action);
        }
    }
}