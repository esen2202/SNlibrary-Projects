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

        }
    }
}
