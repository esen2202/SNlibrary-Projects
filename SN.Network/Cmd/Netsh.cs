using SN.Cmd;
using System;

namespace SN.Network.Cmd
{
    public class Netsh : INetConfigurator
    {
        public event EventHandler ProcessCompleted;
        private ICommandLine _cmd { get; set; }
        private ICommandGenerator _commandGenerator { get; set; }
        public string ResultData { get; set; }

        public Netsh(ICommandLine cmd, ICommandGenerator commandGenerator)
        {
            _cmd = cmd;
            _commandGenerator = commandGenerator;
            _cmd.ProcessCompleted += _cmd_ProcessCompleted;
        }

        private void _cmd_ProcessCompleted(object sender, EventArgs e)
        {
            string data = "";
            _cmd.OutputData.ForEach(item =>
            {
                data += item.ToString();
            });
            ResultData = data.Trim();

            ProcessCompleted?.Invoke(sender, e);
        }

        public void Execute()
        {
            _cmd.Execute(_commandGenerator.Generate());
        }
    }
}
