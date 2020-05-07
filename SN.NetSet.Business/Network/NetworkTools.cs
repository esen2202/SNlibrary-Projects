using SN.Network.Helpers;
using System.Threading.Tasks;

namespace SN.NetSet.Business.Network
{
    public class NetworkTools
    {
        public static Task<string> GetGlobalIpAsync()
        {
            Task<string> islem = Task.Run<string>(() =>
            {
                return NetworkHelper.GetGlobalIp();
            });

            return islem;
        }

        public  static void ShowNetConnections()
        {
             NetworkHelper.ShowNetConnections();
        }
    }
}
