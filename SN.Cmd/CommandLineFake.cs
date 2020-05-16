using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SN.Cmd
{
    public class CommandLineFake : ICommandLine
    {
        private static int ExecCount;
        public string OutputData { get; set; }

        public event EventHandler<EventArgsWithStrMessage> ProcessCompleted;

        public void Execute(string action)
        {
            OutputData =
                "Output Data Count  : " + (ExecCount++).ToString() + Environment.NewLine +
                "Input Action       : " + action;
            ProcessCompleted?.Invoke(this, new EventArgsWithStrMessage
            {
                Message = OutputData,
                MessageList = OutputData.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList()
            });
        }

    }
}
