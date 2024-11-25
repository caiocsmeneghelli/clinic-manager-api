using ClinicManager.Domain.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicManager.Domain
{
    public static class DomainModule
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddServices();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services) { 
            services.AddScoped<IUserService, UserService>();
            return services;
        }

    }

}