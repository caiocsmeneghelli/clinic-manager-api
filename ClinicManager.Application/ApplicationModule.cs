using ClinicManager.Application.Commands.Doctors.CreateDoctor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using FluentValidation;

namespace ClinicManager.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMediator()
                .AddValidation();
            return services;
        }

        private static IServiceCollection AddMediator(this IServiceCollection service)
        {
            service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateDoctorCommand).Assembly));
            return service;
        }

        private static IServiceCollection AddValidation(this IServiceCollection service)
        {
            service.AddValidatorsFromAssemblyContaining<CreateDoctorCommandValidator>();
            return service;
        }
    }
}