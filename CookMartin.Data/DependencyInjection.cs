using CookMartin.Data.Interfaces;
using CookMartin.Data.SqlAccess;
using CookMartin.Data.SqlAccess.Budget.Interfaces;
using CookMartin.Data.SqlAccess.Budget.Repositories;
using CookMartin.Data.SqlAccess.NoteCard.Interfaces;
using CookMartin.Data.SqlAccess.NoteCard.Repositories;
using CookMartin.Data.SqlAccess.Oscar;
using CookMartin.Data.SqlAccess.Oscar.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CookMartin.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDbService(this IServiceCollection services)
    {
        services.AddScoped<IConnectionFactory, SqlConnectionFactory>();
        services.AddScoped<IUnitOfWorkFactory, SqlUnitOfWorkFactory>();
        services.AddScoped<IReadDb, SqlReadDb>();
        services.AddScoped<ITransactionRunner, SqlTransactionRunner>();

        services.AddScoped<ICollectionRepository, CollectionRepository>();
        services.AddScoped<INotecardRepository, NotecardRepository>();
        services.AddScoped<IQuizRepository, QuizRepository>();
        services.AddScoped<IOscarRepository, OscarRepository>();

        services.AddScoped<IBudgetCollectionRepository, BudgetCollectionRepository>();
        services.AddScoped<IBudgetRepository, BudgetRepository>();
        services.AddScoped<IBudgetItemRepository, BudgetItemRepository>();
        services.AddScoped<IRecurringItemRepository, RecurringItemRepository>();

        return services;
    }
}
