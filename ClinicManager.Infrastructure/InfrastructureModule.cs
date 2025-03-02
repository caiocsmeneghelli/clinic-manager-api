using ClinicManager.Domain.Entities;
using ClinicManager.Domain.Repositories;
using ClinicManager.Domain.Services.Auth;
using ClinicManager.Domain.UnitOfWork;
using ClinicManager.Infrastructure.Auth;
using ClinicManager.Infrastructure.Persistence.Context;
using ClinicManager.Infrastructure.Persistence.Repositories;
using ClinicManager.Infrastructure.Persistence.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories()
                .AddUnitOfWork()
                .AddServices()
                .AddAuthentication(configuration)
                .AddDbContext(configuration);

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IMedicalAppointmentRepository, MedicalAppointmentRepository>();

            return services;
        }

        private static IServiceCollection AddUnitOfWork(this IServiceCollection services) {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration) {
            var connectionString = configuration.GetConnectionString("DbContextCs");
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            services.AddDbContext<ClinicManagerDbContext>(options => options.UseMySql(connectionString, serverVersion));

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services) {
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }

        private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services
               .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,

                       ValidIssuer = configuration["Jwt:Issuer"],
                       ValidAudience = configuration["Jwt:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey
                       (Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                   };
               });

            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
