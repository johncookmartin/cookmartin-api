# CookMartin.Blob

Azure Blob Storage integration. Handles PDF upload, streaming, and optional QR code generation. No dependencies on other projects in this solution.

## Folder Structure

```
CookMartin.Blob/
├── DependencyInjection.cs
└── Services/
    ├── Interfaces/
    │   └── IBlobService.cs
    └── BlobServices.cs
```

## Service Interface (`IBlobService`)

```csharp
Task<Stream> StreamAsync(string fileName);
Task<(string Url, string Path)> UploadReadablePdfAsync(string fileName, Stream fileStream);
Task<(string Url, string Path, string? QrCodeBase64)> UploadReadablePdfAsync(string fileName, Stream fileStream, bool generateQrCode);
```

**`StreamAsync`** — fetches a blob and returns its stream for proxying to the client.

**`UploadReadablePdfAsync`** — uploads a PDF and sets HTTP headers so browsers display it inline (not as a download). The overload with `generateQrCode: true` also returns a Base64-encoded PNG QR code pointing to the blob URL.

## Keyed Service Registration

Multiple Azure Blob containers are supported. Each container is registered as a **keyed scoped service** using the container name as the key:

```csharp
// DependencyInjection.cs — reads "AzureBlob:Blobs" array from config
foreach (var containerName in blobNames)
{
    services.AddKeyedScoped<IBlobService>(containerName, (sp, key) =>
        new BlobService(blobServiceClient, containerName));
}
```

Endpoints resolve the correct container via:

```csharp
[FromKeyedServices("public")] IBlobService blobService
```

To add a new container: add its name to `AzureBlob:Blobs` in `appsettings.json` and use the matching key in the endpoint.

## Authentication to Azure

Uses `DefaultAzureCredential`. In development this resolves via the Azure CLI (`az login`). In production it uses the managed identity of the App Service.

A singleton `BlobServiceClient` is registered and shared across all keyed instances:

```csharp
services.AddSingleton(new BlobServiceClient(new Uri(blobUri), new DefaultAzureCredential()));
```

## QR Code

Generated via the **QRCoder** NuGet package. The result is a Base64-encoded PNG string, suitable for embedding in `<img src="data:image/png;base64,...">` directly in the API response.

## Configuration

```json
{
  "AzureBlob": {
    "Uri": "https://cookmartin.blob.core.windows.net/",
    "Blobs": ["public"]
  }
}
```

## Dependency Injection (`DependencyInjection.cs`)

```csharp
services.AddSingleton<BlobServiceClient>(...);
// per container:
services.AddKeyedScoped<IBlobService>(containerName, ...);
```

Called from `CookMartin.API` as `services.AddBlobServices()`.
