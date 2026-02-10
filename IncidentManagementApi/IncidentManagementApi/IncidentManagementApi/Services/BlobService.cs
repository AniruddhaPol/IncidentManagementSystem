using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace IncidentManagementApi.Services
{
    public class BlobService
    {
        private readonly BlobContainerClient _container;

        public BlobService(string connectionString, string containerName)
        {
            var client = new BlobServiceClient(connectionString);
            _container = client.GetBlobContainerClient(containerName);
            _container.CreateIfNotExists(PublicAccessType.None);
        }

        public async Task<string> UploadAsync(Stream content, string blobName, string contentType)
        {
            var blob = _container.GetBlobClient(blobName);
            var headers = new BlobHttpHeaders { ContentType = contentType };
            await blob.UploadAsync(content, headers);
            return blob.Uri.ToString();
        }
    }
}
