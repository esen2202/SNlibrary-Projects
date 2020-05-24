using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;

namespace SN.Network.Helpers
{
    public class NetworkHelper
    {

        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        //Method with API
        public static bool CheckForInternetConnection(out int desc)
        {
            var result = InternetGetConnectedState(out desc, 0);
            return result;
        }

        //Method with WebClient
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }

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

        public static bool ValidateIpV4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));

        }

        public static void ShowNetConnections()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("NCPA.cpl");
            startInfo.UseShellExecute = true;

            Process.Start(startInfo);
        }

        public static bool Ping(string hostName, int timeOut, out PingReply pingReply)
        {
            bool result = false;
            pingReply = null;
            try
            { 
                Ping myPing = new Ping();
                pingReply = myPing.Send(hostName, timeOut);
                if (pingReply != null)
                {
                    Console.WriteLine("Status :  " + pingReply.Status + " \n Time : " + pingReply.RoundtripTime + " \n Address : " + pingReply.Address);
                    result = true;
                }
                else
                {

                }
            }
            catch
            {
               
            }

            return result;
        }
    }
}
