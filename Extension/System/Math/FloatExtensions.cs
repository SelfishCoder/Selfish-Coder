namespace SelfishCoder.Extensions.System
{
    public static class FloatExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool LessThan(this float a, float b)
        {
            return a < b;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool LessThanOrEqual(this float a, float b)
        {
            return a <= b;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool GreaterThan(this float a, float b)
        {
            return a > b;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool GreaterThanOrEqual(this float a, float b)
        {
            return a >= b;
        }
    }
}