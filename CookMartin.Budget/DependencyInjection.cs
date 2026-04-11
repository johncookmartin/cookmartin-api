using CookMartin.Budget.Services;
using CookMartin.Budget.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CookMartin.Budget;

public static class DependencyInjection
{
    public static IServiceCollection AddBudgetServices(this IServiceCollection services)
    {
        services.AddScoped<IBudgetCollectionService, BudgetCollectionService>();
        services.AddScoped<IBudgetService, BudgetService>();
        services.AddScoped<IBudgetItemService, BudgetItemService>();
        services.AddScoped<IRecurringItemService, RecurringItemService>();

        return services;
    }
}
