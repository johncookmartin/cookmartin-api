using CookMartin.API.Endpoints.NoteCard;
using CookMartin.API.Endpoints.Oscar;

namespace CookMartin.API.Endpoints;

public static class EndpointExtensions
{
    public static void MapAllEndpoints(this WebApplication app)
    {
        app.MapBlobEndpoints();
        app.MapCollectionEndpoints();
        app.MapNotecardEndpoints();
        app.MapQuizEndpoints();
        app.MapOscarEndpoints();
    }
}
