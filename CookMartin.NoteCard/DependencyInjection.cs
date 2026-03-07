using CookMartin.NoteCard.Services;
using CookMartin.NoteCard.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CookMartin.NoteCard;

public static class DependencyInjection
{
    public static IServiceCollection AddNoteCardServices(this IServiceCollection services)
    {
        services.AddScoped<INotecardService, NotecardService>();
        services.AddScoped<ICollectionService, CollectionService>();
        
        return services;
    }
}
