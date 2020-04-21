using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SN.Cmd
{
	public class CommandLine: ICommandLine
    {
        public event EventHandler ProcessCompleted;
        public event DataReceivedEventHandler OutputDataReceived;
        public List<string> OutputData { get; set; }

        public CommandLine()
        {
            OutputData = new List<string>();
        }

        public void Execute(string action)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    Verb = "runas",
                    FileName = "cmd.exe",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    Arguments =  action
                },
                EnableRaisingEvents = true
            };

            process.Exited += Process_Exited;
            process.OutputDataReceived += Process_OutputDataReceived;
            process.Start();
            OutputData.Clear();
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            //OutputData.Add(e.Data);
            OutputDataReceived?.Invoke(sender, e);
        }

        private void Process_Exited(object sender, System.EventArgs e)
        {
            var process = (Process)sender;
            var output = process.StandardOutput.ReadToEnd();

            var messages = output.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (var message in messages)
                OutputData.Add(message);

            ProcessCompleted?.Invoke(this, e);
        }
    }
}
