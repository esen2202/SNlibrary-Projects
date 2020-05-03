using SN.Network.Helpers;

namespace SN.NetSet.Business.Network
{
    public class NetworkTools
    {
        public static string GetGlobalIp() => NetworkHelper.GetGlobalIp();

        public static void ShowNetConnections()
        {
            NetworkHelper.ShowNetConnections();
        }
    }
}
