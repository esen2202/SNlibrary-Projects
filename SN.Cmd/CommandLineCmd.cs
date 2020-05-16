using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SN.Cmd
{
    public class CommandLineCmd : ICommandLine
    {
        public event EventHandler<EventArgsWithStrMessage> ProcessCompleted;
        public event DataReceivedEventHandler OutputDataReceived;
        public string OutputData { get; set; }

        private void ClearOutputData()
        {
            OutputData = "";
        }

        private void AppendOutputData(string data)
        {
            OutputData += data;
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
                    Arguments = action
                },
                EnableRaisingEvents = true
            };

            process.Exited += Process_Exited;
            process.OutputDataReceived += Process_OutputDataReceived;
            process.Start();
            ClearOutputData();
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            //AppendOutputData(e.Data);
            OutputDataReceived?.Invoke(sender, e);
        }

        private void Process_Exited(object sender, System.EventArgs e)
        {
            var process = (Process)sender;
            var output = process.StandardOutput.ReadToEnd();

            var messages = output.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            AppendOutputData(output);

            ProcessCompleted?.Invoke(this, new EventArgsWithStrMessage
            {
                Message = output,
                MessageList = messages.ToList<string>()
            });
        }

        public List<string> GetOutputDataAsList()
        {
            List<string> resultList = new List<string>();

            var messages = OutputData.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (var message in messages)
                resultList.Add(message);

            return resultList;
        }
    }


}
