using System.Collections.Generic;

namespace SN.Network.Abstract
{
    public interface ICommandLineService
    {
        IEnumerable<string> Execute(string action, out int exitCode);

    }
}