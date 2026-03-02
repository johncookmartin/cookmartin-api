namespace CookMartin.Blob.Services.Interfaces;

public interface IBlobService
{
    Task<Stream> StreamAsync(string fileName);
    Task<(string Url, string Path)> UploadReadablePdfAsync(string fileName, Stream fileStream);
    Task<(string Url, string Path, string? QrCodeBase64)> UploadReadablePdfAsync(string fileName, Stream fileStream, bool generateQrCode);
}