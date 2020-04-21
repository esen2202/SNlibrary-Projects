using Ninject;

namespace SN.NetSet.Business.DependencyResolvers.Ninject
{
    public class InstanceFactory
    {
        private static IKernel _kernel { get; set; }

        public static T GetInstance<T>()
        {
            _kernel = new StandardKernel(new BusinessModule(),new AutoMapperModule());
            return _kernel.Get<T>();
        }
    }
}
