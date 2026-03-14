using CookMartin.Data.Interfaces;
using CookMartin.Data.SqlAccess;
using CookMartin.Data.SqlAccess.NoteCard.Interfaces;
using CookMartin.Data.SqlAccess.NoteCard.Repositories;
using CookMartin.Data.SqlAccess.Oscar;
using CookMartin.Data.SqlAccess.Oscar.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CookMartin.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDbService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IConnectionFactory, SqlConnectionFactory>();
        services.AddScoped<IUnitOfWorkFactory, SqlUnitOfWorkFactory>();
        services.AddScoped<IReadDb, SqlReadDb>();
        services.AddScoped<ITransactionRunner, SqlTransactionRunner>();

        services.AddScoped<ICollectionRepository, CollectionRepository>();
        services.AddScoped<INotecardRepository, NotecardRepository>();
        services.AddScoped<IQuizRepository, QuizRepository>();
        services.AddScoped<IOscarRepository, OscarRepository>();

        return services;
    }
}
