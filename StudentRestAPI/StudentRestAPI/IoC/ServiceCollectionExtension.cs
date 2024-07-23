using StudentRestAPI.Business.Library.Interfaces;
using StudentRestAPI.Business.Profiles;

namespace StudentRestAPI.IoC
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterAllTypes(this IServiceCollection services, IConfiguration config, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            Business.IoC.ServiceCollectionExtension.RegisterAllTypes(services, config, new[] { typeof(IStudentLibrary).Assembly }, lifetime);
            Business.IoC.ServiceCollectionExtension.RegisterAllProfiles(services, new[] { typeof(StudentProfile).Assembly });
        }
    }
}
