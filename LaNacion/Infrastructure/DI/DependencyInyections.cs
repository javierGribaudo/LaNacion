using FluentValidation;
using LaNacionChallenge.Context;
using LaNacionChallenge.Domain;
using LaNacionChallenge.Infrastructure.Validators;
using LaNacionChallenge.Middelwares;
using LaNacionChallenge.Repository;
using LaNacionChallenge.Services;
using System.Data;
using FluentValidation.AspNetCore;

namespace LaNacionChallenge.Infrastructure.DI
{
    public static class DependencyInyections
    {
        public static void InitializeInjections(this IServiceCollection services)
        {
            services.AddScoped<DapperContext>();
            services.AddFluentValidation();

            // Agregar validación para el objeto Contact
            services.AddTransient<IValidator<Contact>, ContactValidator>();
            services.AddTransient<IValidator<PhoneNumber>, PhoneValidator>();
            services.AddScoped<ExceptionHandlerMiddleware>();
            services.AddScoped<IDbConnection>(provider => provider.GetService<DapperContext>().CreateConnection());
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IContactRepository<Contact>, ContactRepository>();
            services.AddTransient<IContactService<Contact>, ContactService>();
            services.AddTransient<IAddressRepository<Address>, AddressRepository>();
            services.AddTransient<IAddressService<Address>, AddressService>();
            services.AddTransient<IPhoneRepository<PhoneNumber>, PhoneRepository>();
            services.AddTransient<IPhoneService<PhoneNumber>, PhoneService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
