using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SN.Cmd
{
    public interface ICommandLine
    {
        event EventHandler<EventArgsWithStrMessage> ProcessCompleted;
        string OutputData { get; set; }
        void Execute(string action);
    }

    public class EventArgsWithStrMessage
    {
        public string Message { get; set; }
        public List<string> MessageList { get; set; }
    }
}