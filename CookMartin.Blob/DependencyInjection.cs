using Azure.Identity;
using Azure.Storage.Blobs;
using CookMartin.Blob.Services;
using CookMartin.Blob.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CookMartin.Blob;

public static class DependencyInjection
{
    public static IServiceCollection AddServices<TMigrationAssemblyMarker>(this IServiceCollection services, IConfiguration config)
    {
        var blobStorageUri = config["AzureBlob:Uri"];
        if (string.IsNullOrEmpty(blobStorageUri))
        {
            throw new InvalidOperationException("AzureBlob:Uri is not configured.");
        }

        var blobServiceClient = new BlobServiceClient(
            new Uri(blobStorageUri),
            new DefaultAzureCredential());

        services.AddSingleton(blobServiceClient);

        List<string> blobs = config.GetSection("AzureBlob:Blobs").Get<List<string>>() ?? [];

        if (blobs.Count == 0)
        {
            blobs = blobServiceClient.GetBlobContainers()
                .Select(container => container.Name)
                .ToList();
        }

        foreach (string blob in blobs)
        {
            string containerName = blob;
            services.AddKeyedScoped<IBlobService>(containerName, (sp, key) =>
            {
                var client = sp.GetRequiredService<BlobServiceClient>();
                return new BlobServices(client, (string)key!);
            });
        }

        return services;
    }
}
