using Ninject;
using Ninject.Parameters;

namespace SN.NetSet.Business.DependencyResolvers.Ninject
{
    public class InstanceFactory
    {
        private static IKernel _kernel { get; set; }

        public static T GetInstance<T>(params IParameter[] parameters)
        {
            _kernel = new StandardKernel(
                new BusinessModule(),
                new AutoMapperModule(),
                new NetworkModule());

            return _kernel.Get<T>(parameters);
        }
    }
}
