using BicycleCompany.Models.Request;
using BicycleCompany.Models.Response;
using BicycleCompany.PartModels.API.Boundary.Features;

namespace BicycleCompany.PartModels.API.Services.Interfaces
{
    public interface IPartService
    {
        Task<List<PartForReadModel>> GetPartListAsync(PartParameters partParameters, HttpResponse response);
        Task<PartForReadModel> GetPartAsync(Guid id);
        Task<Guid> CreatePartAsync(PartForCreateOrUpdateModel model);
        Task UpdatePartAsync(Guid id, PartForCreateOrUpdateModel model);
        Task DeletePartAsync(Guid id);
        Task<PartForCreateOrUpdateModel> GetPartForUpdateModelAsync(Guid id);
    }
}
