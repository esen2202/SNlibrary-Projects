using SN.Data.Entities;

namespace SN.NetSet.Entities.Concrete.Network
{
    public class NetConfig : IEntity
    {
        public int NetConfigId { get; set; }
        public string ConfigName { get; set; }
        public string IpAddress { get; set; }
        public string Gateway { get; set; }
        public string SubnetMask { get; set; }
    }
}
