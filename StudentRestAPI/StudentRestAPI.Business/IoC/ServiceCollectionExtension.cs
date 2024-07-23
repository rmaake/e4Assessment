using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentRestAPI.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StudentRestAPI.Business.IoC
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterAllTypes(this IServiceCollection services, IConfiguration config, Assembly[] assemblies, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            //services.AddScoped<IMessagePublisher, MessagePublisher>();
            DAL.IoC.ServiceCollectionExtension.RegisterAllTypes(services, config, new[] { typeof(IStudentRepository).Assembly }, lifetime);
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.IsClass && x.Namespace.Contains("StudentRestAPI.Business.Library.Implementations")));
            foreach (var type in typesFromAssemblies)
            {
                try
                {
                    if (type == null || string.IsNullOrEmpty(type.Namespace))
                    {
                        continue;
                    }
                    if (type.Name.EndsWith("Library"))
                    {
                        var iType = type.GetInterfaces().FirstOrDefault();
                        services.Add(new ServiceDescriptor(iType, type, lifetime));
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        public static void RegisterAllProfiles(this IServiceCollection services, Assembly[] assemblies)
        {
            services.AddAutoMapper(config =>
            {
                var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.IsClass && x.Namespace.Contains("StudentRestAPI.Business.Profiles")));
                foreach (var type in typesFromAssemblies)
                {
                    if (type == null || string.IsNullOrEmpty(type.Namespace))
                    {
                        continue;
                    }
                    if (type.Name.EndsWith("Profile"))
                    {
                        config.AddProfile(type);
                    }
                }
            });
        }
    }
}
