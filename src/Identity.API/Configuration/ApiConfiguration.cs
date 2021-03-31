using Identity.API.Business.Interfaces;
using Identity.API.Business.Notificacoes;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.API.Configuration
{
    public static class ApiConfiguration
    {
        public static IServiceCollection ResolveDependences(this IServiceCollection services)
        {
            services.AddScoped<INotificador, Notificador>();

            return services;
        }
    }
}