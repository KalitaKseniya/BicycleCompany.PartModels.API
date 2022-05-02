using BicycleCompany.PartModels.API.Boundary.Features;
using BicycleCompany.PartModels.API.Models;

namespace BicycleCompany.PartModels.API.Repositories.Interfaces
{
    public interface IPartModelRepository
    {
        Task<PagedList<PartModel>> GetAllAsync(PartModelParameters parameters);
        Task<PartModel> GetByIdIncludedAsync(Guid id);
        Task<PartModel> GetByNameAsync(string name);
        Task CreateAsync(PartModel partModel);
        Task DeleteAsync(PartModel partModel);
        Task UpdateAsync(PartModel partModel);
        Task<List<PartModel>> GetAllForPartAsync(Guid partId, Guid partModelId);
        Task<PartModel> GetByIdAsync(Guid id);
    }
}
