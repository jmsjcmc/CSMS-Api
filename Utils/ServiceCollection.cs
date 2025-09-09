using csms_backend.Controllers;
using csms_backend.Models.Entities;

namespace csms_backend.Utils
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddServices(this IServiceCollection service)
        {
            service.AddScoped<UserService>();
            service.AddScoped<BusinessUnitService>();
            service.AddScoped<RoleService>();
            return service;
        }
        public static IServiceCollection AddQueries(this IServiceCollection service)
        {
            service.AddScoped<UserQuery>();
            service.AddScoped<BusinessUnitQuery>();
            service.AddScoped<RoleQuery>();
            return service;
        }
    }
}
