using AutoMapper;
using Ninject.Modules;
using SN.Class.Helpers;

namespace SN.NetSet.Business.DependencyResolvers
{
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMapper>().ToConstant(CreateConfiguration().CreateMapper());
        }

        private MapperConfiguration CreateConfiguration()
        {
            var autoMapperProfiles = ClassHelper.GetClassListFromAssembly<Profile>("SN.NetSet.Business");

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(autoMapperProfiles);
            });
            return config;
        }
    }
}
