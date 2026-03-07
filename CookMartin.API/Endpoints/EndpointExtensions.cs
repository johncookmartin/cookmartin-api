using CookMartin.API.Endpoints.NoteCard;

namespace CookMartin.API.Endpoints;

public static class EndpointExtensions
{
    public static void MapAllEndpoints(this WebApplication app)
    {
        app.MapBlobEndpoints();
        app.MapCollectionEndpoints();
        app.MapNotecardEndpoints();
    }
}
