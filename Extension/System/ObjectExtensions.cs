namespace SelfishCoder.Extensions.System
{
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"></param>
        /// <param name="compared"></param>
        /// <returns></returns>
        public static bool NotEquals(this object current, object compared)
        {
            return !current.Equals(compared);
        }
    }
}