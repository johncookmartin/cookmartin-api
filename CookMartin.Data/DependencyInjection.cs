using CookMartin.Data.SqlAccess;
using CookMartin.Data.SqlAccess.Interfaces;
using CookMartin.Data.SqlAccess.NoteCard.Interfaces;
using CookMartin.Data.SqlAccess.NoteCard.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CookMartin.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDbService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICookMartinUnitOfWork, CookMartinSqlUnitOfWork>();
        services.AddScoped<ICookMartinDataAccess, CookMartinSqlDataAccess>();

        services.AddScoped<ICollectionRepository, CollectionRepository>();
        services.AddScoped<INotecardRepository, NotecardRepository>();

        return services;
    }
}
