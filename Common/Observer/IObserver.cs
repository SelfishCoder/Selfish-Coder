namespace SelfishCoder.Common
{
    /// <summary>
    /// 
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subject"></param>
        void OnNotified(ISubject subject);
    }
}