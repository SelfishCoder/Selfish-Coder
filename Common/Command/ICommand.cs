namespace SelfishCoder.Common
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// 
        /// </summary>
        void Execute();

        /// <summary>
        /// 
        /// </summary>
        void Undo();
    }
}