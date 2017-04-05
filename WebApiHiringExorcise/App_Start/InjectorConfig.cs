using System.Linq;
using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace WebApiHiringExorcise
{
    public static class InjectorConfig
    {
        public static void RegisterServices()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            var assemblyTypes = typeof(InjectorConfig).Assembly.GetTypes();
            var localServices = (from itype in assemblyTypes
                                 where itype.IsInterface
                                 from type in assemblyTypes
                                 where itype.IsAssignableFrom(type)
                                 && itype.Name == "I" + type.Name
                                 select new { ContractType = itype, ImplementationType = type }).ToList();

            localServices.ForEach(svc => container.Register(svc.ContractType, svc.ImplementationType, Lifestyle.Transient));

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}