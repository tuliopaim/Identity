using Identity.API.Business.Interfaces;
using Identity.API.Business.Notificacoes;
using Identity.API.Business.Services;
using Identity.API.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.API.Extensions
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ResolveDependences(this IServiceCollection services)
        {
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<SeedData>();

            return services;
        }
    }
}