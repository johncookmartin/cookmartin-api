using Azure.Storage.Blobs;
using CookMartin.Blob.Services.Interfaces;

namespace CookMartin.Blob.Services;

public class BlobServices : IBlobService
{
    private readonly BlobContainerClient _containerClient;
    public BlobServices(BlobServiceClient blobServiceClient, string containerName)
    {
        _containerClient = blobServiceClient.GetBlobContainerClient(containerName);
    }

    public async Task<(string Url, string Path)> UploadAsync(string fileName, Stream fileStream)
    {
        var blob = _containerClient.GetBlobClient(fileName);
        await blob.UploadAsync(fileStream, overwrite: true);

        string blobUrl = blob.Uri.ToString();
        string blobPath = blob.Name;
        return (blobUrl, blobPath);
    }

    public async Task<Stream> StreamAsync(string fileName)
    {
        var blob = _containerClient.GetBlobClient(fileName);
        var response = await blob.DownloadAsync();
        return response.Value.Content;
    }
}
