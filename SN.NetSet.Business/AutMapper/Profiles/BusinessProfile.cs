using AutoMapper;
using SN.NetSet.Entities.Concrete.Network;
using SN.Network.Model;

namespace SN.NetSet.Business.AutMapper.Profiles
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMap<NetAdapterModelBase, NetAdapterModelBase>().ReverseMap();

            CreateMap<NetConfigBase, NetConfigBase>().ReverseMap();

            CreateMap<NetIpConfigModel, NetConfigBase>()
                //.ForMember(dest => dest.DnsServer1, opt => opt.MapFrom(src => src.DnsServer1))
                //.ForMember(dest => dest.DnsServer2, opt => opt.MapFrom(src => src.DnsServer2))
                .ReverseMap();
        }
    }
}
