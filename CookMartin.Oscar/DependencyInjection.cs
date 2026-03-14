using CookMartin.Oscar.Services;
using CookMartin.Oscar.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CookMartin.Oscar;

public static class DependencyInjection
{
    public static IServiceCollection AddOscarServices(this IServiceCollection services)
    {
        services.AddScoped<IOscarService, OscarService>();
        return services;
    }
}
