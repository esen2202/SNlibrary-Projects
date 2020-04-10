using SN.Network.Abstract;
using System.Collections.Generic;
using System.Diagnostics;

namespace SN.Network.Cmd
{
    public class CommandLineManager : ICommandLineService
    {
        private static readonly object lockObj = new object();
        private static CommandLineManager _instance;

        public event DataReceivedEventHandler ProcessStarted;


        public static CommandLineManager GetInstance
        {
            get
            {
                if (_instance == null)
                    lock (lockObj)
                    {
                        if (_instance == null)
                            _instance = new CommandLineManager();
                    }
                return _instance;
            }
        }
        private CommandLineManager()
        {
        }


        public IEnumerable<string> Execute(string action, out int exitCode)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    FileName = "cmd.exe",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    Arguments = "/c " + action
                }
            };

            process.Exited += Process_Exited;
            process.OutputDataReceived += Process_OutputDataReceived;
            process.Start();
             
            var lines = new List<string>();
            while (!process.StandardOutput.EndOfStream)
                lines.Add(process.StandardOutput.ReadLine());

            exitCode = process.ExitCode;

            return lines;
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
             
        }

        private void Process_Exited(object sender, System.EventArgs e)
        {
           
        }
    }
}
