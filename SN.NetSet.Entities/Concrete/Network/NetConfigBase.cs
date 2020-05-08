using SN.Data.Entities;
using SQLite;

namespace SN.NetSet.Entities.Concrete.Network
{
    public class NetConfigBase : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public virtual int Id { get; set; }
        public virtual string ConfigName { get; set; }
        public virtual string IpAddress { get; set; }
        public virtual string Gateway { get; set; }
        public virtual string SubnetMask { get; set; }
        public virtual string DnsServer1 { get; set; }
        public virtual string DnsServer2 { get; set; }

        public IEntity Clone()
        {
            return (IEntity)this.MemberwiseClone();
        }
    }
}
    