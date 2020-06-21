using SN.Network.Helpers;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace SN.NetSet.Business.Network
{
    public class NetworkTools
    {
        public static Task<string> GetGlobalIpAsync()
        {
            Task<string> result = Task.Run<string>(() =>
            {
                return NetworkHelper.GetGlobalIp();
            });

            return result;
        }

        public static Task<bool> CheckForInternetConnection()
        {
            Task<bool> result = Task.Run<bool>(() =>
            {
                return NetworkHelper.CheckForInternetConnection();
            });

           return result;
        }

        public static Task<bool> CheckForInternetConnectionAPI()
        {
            Task<bool> result = Task.Run<bool>(() =>
            {
                int desc;
                return NetworkHelper.CheckForInternetConnection(out desc);
            });

            return result;
        }

        public static Task<string> Ping(string hostName, int timeOut)
        {
            Task<string> result = Task.Run<string>(() =>
            {
                return NetworkHelper.Ping( hostName,  timeOut);
            });

            return result;
        }

        public static void ShowNetConnections()
        {
            NetworkHelper.ShowNetConnections();
        }
    }
}
