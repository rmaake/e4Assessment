using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentRestAPI.DAL.Models.DbContexts;
using System.Reflection;

namespace StudentRestAPI.DAL.IoC
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterAllTypes(this IServiceCollection services, IConfiguration config, Assembly[] assemblies, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            services.AddDbContext<StudentDbContext>(opt => opt.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.IsClass));
            foreach (var type in typesFromAssemblies)
            {
                try
                {
                    if (type == null || string.IsNullOrEmpty(type.Namespace))
                    {
                        continue;
                    }
                    if (type.Name == "RepositoryBase`1")
                    {
                        continue;
                    }
                    if (type.Namespace.Contains("StudentRestAPI.DAL.Repository.Implementations"))
                    {

                        var iType = type.GetInterfaces().FirstOrDefault(opt => opt.Name.EndsWith("Repository"));
                        services.Add(new ServiceDescriptor(iType, type, lifetime));
                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
