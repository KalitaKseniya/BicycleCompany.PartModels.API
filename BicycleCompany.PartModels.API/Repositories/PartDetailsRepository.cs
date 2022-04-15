using BicycleCompany.PartModels.API.Infrastructure;
using BicycleCompany.PartModels.API.Models;
using BicycleCompany.PartModels.API.Repositories.Interfaces;

namespace BicycleCompany.PartModels.API.Repositories
{
    public class PartDetailsRepository : RepositoryBase<PartModel>, IPartDetailsRepository
    {
        public PartDetailsRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public Task CreatePartDetailAsync(PartModel partDetails) => CreateAsync(partDetails);

        public Task DeletePartDetailAsync(PartModel partDetails) => DeleteAsync(partDetails);

        //public async Task<PartModel> GetPartDetailAsync(Guid problemId, Guid partId) =>
        //    await FindByCondition(pd => pd.ProblemId.Equals(problemId) && pd.PartId.Equals(partId))
        //        .Include(pd => pd.Part)
        //        .SingleOrDefaultAsync();

        //public async Task<List<PartModel>> GetPartDetailListAsync(Guid problemId)
        //{
        //    var parts = await FindByCondition(pd => pd.ProblemId.Equals(problemId))
        //        .Include(pd => pd.Part)
        //        .OrderBy(pd => pd)
        //        .ToListAsync();

        //    return parts;
        //}
    }
}
