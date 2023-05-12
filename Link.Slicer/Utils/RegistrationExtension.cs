using Autofac;
using Link.Slicer.Repositories;
using Link.Slicer.Services;

namespace Link.Slicer.Utils
{
    public static class RegistrationExtension
    {
        public static void RegisterCustomServices(this ConfigureHostBuilder builder)
        {
            builder.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterType<Repository>().As<IRepository>().InstancePerLifetimeScope();
                containerBuilder.RegisterType<UrlService>().As<IUrlService>().InstancePerLifetimeScope();
            });
        }
    }
}
