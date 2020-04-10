using System.Collections.Generic;

namespace SN.Cmd
{
    public interface ICommandLine
    {
        IEnumerable<string> Execute(string action, out int exitCode);
    }
}