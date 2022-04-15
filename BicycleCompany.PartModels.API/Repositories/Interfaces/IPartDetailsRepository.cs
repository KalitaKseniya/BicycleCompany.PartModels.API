using BicycleCompany.PartModels.API.Models;

namespace BicycleCompany.PartModels.API.Repositories.Interfaces
{
    public interface IPartDetailsRepository
    {
        //Task<List<PartModel>> GetPartDetailListAsync(Guid problemId);
        //Task<PartModel> GetPartDetailAsync(Guid problemId, Guid partId);
        Task CreatePartDetailAsync(PartModel partDetails);
        Task DeletePartDetailAsync(PartModel partDetails);
    }
}
