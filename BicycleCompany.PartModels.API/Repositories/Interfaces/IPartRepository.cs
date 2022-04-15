using BicycleCompany.PartModels.API.Boundary.Features;
using BicycleCompany.PartModels.API.Models;

namespace BicycleCompany.PartModels.API.Repositories.Interfaces
{
    public interface IPartRepository
    {
        Task<PagedList<Part>> GetPartsAsync(PartParameters partParameters);
        Task<Part> GetPartAsync(Guid id);
        Task<Part> GetPartByNameAsync(string name);
        Task CreatePartAsync(Part part);
        Task DeletePartAsync(Part part);
        Task UpdatePartAsync(Part part);
    }
}
