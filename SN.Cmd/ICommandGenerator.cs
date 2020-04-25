using System;

namespace SN.Cmd
{
    public interface ICommandGenerator : IDisposable
    {
        string Generate();
    }
}
