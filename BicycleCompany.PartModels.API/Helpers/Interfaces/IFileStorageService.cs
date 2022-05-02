namespace BicycleCompany.PartModels.API.Helpers
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(byte[] content, string extension, string containerName);
        Task<string> EditFileAsync(byte[] content, string extension, string containerName, string fileRoute);
        Task DeleteFileAsync(string fileRoute, string containerName);
    }
}