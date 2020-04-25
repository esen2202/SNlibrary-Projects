using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SN.Cmd
{
    public class CommandLineFake : ICommandLine
    {
        private static int ExecCount; 
        public string OutputData { get; set; }

        public event EventHandler ProcessCompleted;

        public void Execute(string action)
        {
            OutputData =
                "Output Data Count  : " + (ExecCount++).ToString() + Environment.NewLine +
                "Input Action       : " + action;
            Thread.Sleep(2000);
            ProcessCompleted?.Invoke(this, new EventArgs());
        }

    }
}
