using System;
using System.Diagnostics;
using System.Net.Http;

namespace SN.Network.Helpers
{
    public class NetworkHelper
    {
        public static string GetGlobalIp()
        {
            string uri = "http://checkip.dyndns.org/";
            string ip = String.Empty;

            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync(uri).Result.Content.ReadAsStringAsync().Result;
                    ip = result.Split(':')[1].Split('<')[0];
                }
                catch (Exception)
                {

                }

                return ip;
            }
        }
        public static void ShowNetConnections()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("NCPA.cpl");
            startInfo.UseShellExecute = true;

            Process.Start(startInfo);
        }
    }
}
