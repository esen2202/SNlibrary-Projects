using System.Collections.Generic;

namespace SN.Cmd
{
    public interface ICommandLine
    {
        List<string> OutputData { get; set; }
        void Execute(string action);
    }
}