using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SN.Cmd
{
    public interface ICommandLine
    {
        event EventHandler ProcessCompleted;
        string OutputData { get; set; }
        void Execute(string action);
    }
}