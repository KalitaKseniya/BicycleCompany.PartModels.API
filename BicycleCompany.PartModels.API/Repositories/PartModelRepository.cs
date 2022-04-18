using BicycleCompany.PartModels.API.Boundary.Features;
using BicycleCompany.PartModels.API.Infrastructure;
using BicycleCompany.PartModels.API.Models;
using BicycleCompany.PartModels.API.Repositories.Extensions;
using BicycleCompany.PartModels.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BicycleCompany.PartModels.API.Repositories
{
    public class PartModelRepository : RepositoryBase<PartModel>, IPartModelRepository
    {                                                                                      
        public PartModelRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<PagedList<PartModel>> GetAllAsync(PartModelParameters parameters)
        {
            var partModels = await FindAll()
               .Search(parameters.SearchTerm)
               .FilterByPrice(parameters.MinPrice, parameters.MaxPrice)
               .Sort(parameters.OrderBy)
               .Include(pm => pm.Part)
               .Include(pm => pm.Manufacturer)
               .ToListAsync();

            return PagedList<PartModel>.ToPagedList(partModels, parameters.PageNumber, parameters.PageSize);
        }
        
        public async Task<List<PartModel>> GetAllForPartAsync(Guid partId, Guid partModelId)
        {
            return await FindByCondition(pm => pm.PartId == partId && pm.Id == partModelId)
                .Include(pd => pd.Part)
                .Include(pd => pd.Manufacturer)
                .ToListAsync();
        }

        public async Task<PartModel> GetByIdAsync(Guid id)
        {
            return await FindByCondition(m => m.Id == id)
                .Include(pd => pd.Part)
                .Include(pd => pd.Manufacturer)
                .FirstOrDefaultAsync();
        }
        public async Task<PartModel> GetByIdForPartAsync(Guid partId, Guid partModelId)
        {
            return await FindByCondition(m => m.Id == partModelId && m.PartId == partId)
                .Include(pd => pd.Part)
                .Include(pd => pd.Manufacturer)
                .FirstOrDefaultAsync();
        }

        public async Task<PartModel> GetByNameAsync(string name)
        {
            return await FindByCondition(m => m.Name == name)
                .Include(pd => pd.Part)
                .Include(pd => pd.Manufacturer)
                .FirstOrDefaultAsync();
        }
    }
}
