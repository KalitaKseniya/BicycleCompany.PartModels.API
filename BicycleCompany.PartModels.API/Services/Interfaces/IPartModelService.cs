using BicycleCompany.PartModels.API.Boundary.Features;
using BicycleCompany.PartModels.API.Boundary.Request;
using BicycleCompany.PartModels.API.Boundary.Responses;

namespace BicycleCompany.PartModels.API.Services.Interfaces
{
    public interface IPartModelService
    {
        Task<PagedList<PartModelForReadModel>> GetListAsync(PartModelParameters parameters);
        Task<PartModelForReadModel> GetByIdAsync(Guid id);
        Task<PartModelCreatedModel> CreateAsync(PartModelForCreateOrUpdateModel model);
        Task UpdateAsync(Guid id, PartModelForCreateOrUpdateModel model);
        Task DeleteAsync(Guid id);
    }
}
