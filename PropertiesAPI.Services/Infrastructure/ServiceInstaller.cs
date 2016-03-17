using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using PropertiesAPI.Repository;

namespace PropertiesAPI.Services.Infrastructure
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
        //    container.Register(Component.For<IRepository<>))
        //                     .ImplementedBy<CachedRepository>());

            container.Register(Component.For<ICachedRepository>()
                       .ImplementedBy<CachedRepository>()
                       .LifestylePerWebRequest());


        }
    }
}
