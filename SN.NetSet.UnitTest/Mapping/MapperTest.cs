using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SN.NetSet.Business.AutMapper.Profiles;
using SN.NetSet.Business.DependencyResolvers.Ninject;
using SN.NetSet.Entities.Concrete.Network;

namespace SN.NetSet.UnitTest.Mapping
{
    [TestClass]
    public class MapperTest
    {
        [TestMethod]
        public void NetconfigMapBasetoMap()
        {
            NetConfigBase netConfigBase = new NetConfigBase
            {
                ConfigName = "net config mapping",
                Gateway = "gateway 1",
                Id = 1,
                IpAddress = "ip 1",
                SubnetMask = "sub 1"
            };

            NetConfigBase netConfigMap = new NetConfigBase
            {
                ConfigName = "net config mapping",
                IpAddress = "ip 1",
                Id = 1
            };

            var _mapper = InstanceFactory.GetInstance<IMapper>();
            var map = _mapper.Map<NetConfigBase>(netConfigBase);

            Assert.AreEqual(netConfigMap, map);
        }
    }
}
