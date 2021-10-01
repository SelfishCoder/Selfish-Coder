using System;
using System.Collections.Generic;

namespace SelfishCoder.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class CommandHandler
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly Stack<ICommand> commands = default;

        /// <summary>
        /// 
        /// </summary>
        public Stack<ICommand> Commands => commands;

        /// <summary>
        /// 
        /// </summary>
        public event Action<ICommand> CommandExecuted;

        /// <summary>
        /// 
        /// </summary>
        public event Action<ICommand> CommandUndid;

        /// <summary>
        /// 
        /// </summary>
        public CommandHandler()
        {
            commands = new Stack<ICommand>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        public void Execute(ICommand command)
        {
            commands.Push(command);
            command.Execute();
            CommandExecuted?.Invoke(command);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Undo()
        {
            ICommand lastCommand = commands.Peek();
            lastCommand.Undo();
            commands.Pop();
            CommandUndid?.Invoke(lastCommand);
        }
    }
}