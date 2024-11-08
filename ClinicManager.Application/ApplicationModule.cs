using ClinicManager.Application.Commands.Doctors.CreateDoctor;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using ClinicManager.Domain.Entities;

namespace ClinicManager.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
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