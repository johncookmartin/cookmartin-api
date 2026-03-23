using CookMartin.API.Endpoints;
using CookMartin.Blob;
using CookMartin.Data;
using CookMartin.NoteCard;
using CookMartin.Oscar;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddDbService();
builder.Services.AddNoteCardServices();
builder.Services.AddOscarServices();
builder.Services.AddBlobServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        var allowedOrigins = builder.Configuration
            .GetSection("Cors:AllowedOrigins")
            .Get<string[]>() ?? [];

        policy.WithOrigins(allowedOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

//Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CookMartinApi",
        Version = "v1"
    });

});

builder.Services.AddOpenApi();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "CookMartinApi v1");
    });
    app.MapOpenApi();

    app.UseHttpsRedirection();
}

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<CookMartin.API.Hubs.OscarHub>("/hubs/oscar");

app.MapGet("/health", () => Results.Ok(new { ok = true, message = "API is healthy" }))
    .WithName("HealthCheck")
    .WithTags("Health");
app.MapAllEndpoints();

app.MapControllers();

app.Run();
