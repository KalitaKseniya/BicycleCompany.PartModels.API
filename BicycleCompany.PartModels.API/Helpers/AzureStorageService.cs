using Azure.Storage.Blobs;

namespace BicycleCompany.PartModels.API.Helpers
{
    public class AzureStorageService : IFileStorageService
    {
        private readonly string connectionString;
        public AzureStorageService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("AzureStorageConnection");
        }

        public async Task DeleteFileAsync(string fileRoute, string containerName)
        {
            if (string.IsNullOrWhiteSpace(fileRoute))
            {
                return;
            }
            var client = new BlobContainerClient(connectionString, containerName);
            await client.CreateIfNotExistsAsync();
            var fileName = Path.GetFileName(fileRoute);
            var blob = client.GetBlobClient(fileName);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<string> EditFileAsync(byte[] content, string extension, string containerName, string fileRoute)
        {
            await DeleteFileAsync(fileRoute, containerName);
            return await SaveFileAsync(content, extension, containerName);
        }

        // container in Az = folder
        public async Task<string> SaveFileAsync(byte[] content, string extension, string containerName)
        {
            var client = new BlobContainerClient(connectionString, containerName);
            await client.CreateIfNotExistsAsync();
            client.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var blob = client.GetBlobClient(fileName);
            using (var ms = new MemoryStream(content))
            {
                await blob.UploadAsync(ms);
            }
            return blob.Uri.ToString();
        }
    }
}
