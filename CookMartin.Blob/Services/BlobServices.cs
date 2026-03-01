using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CookMartin.Blob.Services.Interfaces;

namespace CookMartin.Blob.Services;

public class BlobServices : IBlobService
{
    private readonly BlobContainerClient _containerClient;
    public BlobServices(BlobServiceClient blobServiceClient, string containerName)
    {
        _containerClient = blobServiceClient.GetBlobContainerClient(containerName);
    }

    public async Task<(string Url, string Path)> UploadReadablePdfAsync(string fileName, Stream fileStream)
    {
        var blob = _containerClient.GetBlobClient(fileName);

        var uploadOptions = new BlobUploadOptions
        {
            HttpHeaders = new BlobHttpHeaders
            {
                ContentType = "application/pdf",
                ContentDisposition = "inline"
            },
            Conditions = null
        };


        await blob.UploadAsync(fileStream, uploadOptions);

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
