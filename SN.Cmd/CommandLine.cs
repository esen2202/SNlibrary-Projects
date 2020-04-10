using System.Collections.Generic;
using System.Diagnostics;

namespace SN.Cmd
{
	public class CommandLine: ICommandLine
    {
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

			process.Start();

			var lines = new List<string>();
			while (!process.StandardOutput.EndOfStream)
				lines.Add(process.StandardOutput.ReadLine());

			exitCode = process.ExitCode;

			return lines;
		}
	}
}
