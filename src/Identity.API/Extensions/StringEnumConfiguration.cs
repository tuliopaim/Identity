using Newtonsoft.Json.Converters;

namespace Identity.API.Extensions
{
    public static class StringEnumConfiguration
    {
        public static IServiceCollection AddControllersWithJsonConfig(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                opt.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            return services;
        }
    }
}
