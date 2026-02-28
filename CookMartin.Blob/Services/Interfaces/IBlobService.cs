namespace CookMartin.Blob.Services.Interfaces;

public interface IBlobService
{
    Task<Stream> StreamAsync(string fileName);
    Task<(string Url, string Path)> UploadAsync(string fileName, Stream fileStream);
}