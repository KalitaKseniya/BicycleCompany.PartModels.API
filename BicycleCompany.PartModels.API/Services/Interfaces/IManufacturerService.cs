using BicycleCompany.PartModels.API.Boundary.Request;
using BicycleCompany.PartModels.API.Boundary.Responses.Manufacturer;

namespace BicycleCompany.PartModels.API.Services.Interfaces
{
    public interface IManufacturerService
    {
        Task<List<ManufacturerForReadModel>> GetListAsync();
        Task<ManufacturerForReadModel> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(ManufacturerForCreateOrUpdateModel model);
        Task DeleteAsync(Guid id);
    }
}
