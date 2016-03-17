using PropertiesAPI.Services.Infrastructure;
using PropertiesAPI.Services.Services;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using PropertiesAPI.Presenters;
using System.Web.Http;

namespace PropertiesAPI.Infrastructure
{
    public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes
             .FromThisAssembly()
             .BasedOn<ApiController>().LifestylePerWebRequest());

            container.Register(
            Component.For<IPropertiesPresenter>()
                  .ImplementedBy<PropertiesPresenter>());

            container.Register(
              Component.For<IPropertiesService>()
                       .ImplementedBy<PropertiesService>());

            container.Install(new ServiceInstaller());

        }
    }
}