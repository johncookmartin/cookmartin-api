using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CookMartin.Blob.Services.Interfaces;
using QRCoder;

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

    public async Task<(string Url, string Path, string? QrCodeBase64)> UploadReadablePdfAsync(string fileName, Stream fileStream, bool generateQrCode)
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

        string? qrCodeBase64 = null;
        if (generateQrCode)
        {
            qrCodeBase64 = GenerateQrCode(blobUrl);
        }

        return (blobUrl, blobPath, qrCodeBase64);
    }

    private static string GenerateQrCode(string url)
    {
        using var qrGenerator = new QRCodeGenerator();
        using var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
        using var qrCode = new PngByteQRCode(qrCodeData);
        var qrCodeImage = qrCode.GetGraphic(20);
        return Convert.ToBase64String(qrCodeImage);
    }

    public async Task<Stream> StreamAsync(string fileName)
    {
        var blob = _containerClient.GetBlobClient(fileName);
        var response = await blob.DownloadAsync();
        return response.Value.Content;
    }
}
