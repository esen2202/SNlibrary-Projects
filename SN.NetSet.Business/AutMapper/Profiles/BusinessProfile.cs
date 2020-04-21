using AutoMapper;
using SN.NetSet.Entities.Concrete.Network;

namespace SN.NetSet.Business.AutMapper.Profiles
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
             CreateMap<NetConfigBase, NetConfigMap>().ReverseMap();

        }
    }

    public class NetConfigMap
    {
        public virtual int Id { get; set; }
        public virtual string ConfigName { get; set; }
        public virtual string IpAddress { get; set; }
     
    }
}
