using System.Collections.Generic;

namespace ObjectMovementSystem
{
    public class CommandManager
    {
        private static CommandManager _instance;
        private Queue<ICommand> _commandsQueue = new Queue<ICommand>();
        private Stack<ICommand> _historyStack = new Stack<ICommand>();
        private ICommand _command;

        public static CommandManager Instance
        {
            get { return _instance ?? (_instance = new CommandManager()); }
        }

        public void AddCommand(ICommand command)
        {
            _commandsQueue.Enqueue(command);
            ExecuteNextCommand();
        }


        public void ExecuteNextCommand()
        {
            if (_commandsQueue.Count > 0 && (_command == null || _command.IsFinished))
            {
                if (_command != null)
                {
                    _historyStack.Push(_command);
                }

                _command = _commandsQueue.Dequeue();
                _command.Execute();
            }
        }

        public void Undo()
        {
            if (_historyStack.Count > 0 && (_command == null || _command.IsFinished))
            {
                _command = _historyStack.Pop();
                _command.Execute();
            }
        }
    }
}