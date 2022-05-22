using System.Collections.Generic;
using Command;

namespace Network.Controller
{
    public static class CommandNextServerResponseQueue
    {
        private static readonly Dictionary<string, List<ICommand>> CommandQueue;

        static CommandNextServerResponseQueue()
        {
            CommandQueue = new Dictionary<string, List<ICommand>>();
        }

        public static void AddCommand(string eventName, ICommand command)
        {
            if (CommandQueue.ContainsKey(eventName)) CommandQueue[eventName].Add(command);
            else CommandQueue.Add(eventName, new List<ICommand>() { command });
        }

        public static void ExecuteCommands(string eventName)
        {
            if (CommandQueue.ContainsKey(eventName) == false) return;

            foreach (var command in CommandQueue[eventName])
            {
                command.Execute();
            }

            CommandQueue.Remove(eventName);
        }
    }
}