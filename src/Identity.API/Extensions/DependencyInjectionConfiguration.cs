using Identity.Business.Interfaces;
using Identity.Business.Notificacoes;
using Identity.Business.Services;
using Identity.Data;
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