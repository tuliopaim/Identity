﻿using Identity.Business.Interfaces;
using Identity.Business.Interfaces.Repositories;
using Identity.Business.Interfaces.Services;
using Identity.Business.Notificacoes;
using Identity.Business.Services;
using Identity.Data;
using Identity.Data.Repositories;

namespace Identity.API.Extensions
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ResolveDependences(this IServiceCollection services)
        {
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<ISeedData, SeedData>();
            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IPerfilService, PerfilService>();

            services.AddScoped<IPerfilRepository, PerfilRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            return services;
        }
    }
}