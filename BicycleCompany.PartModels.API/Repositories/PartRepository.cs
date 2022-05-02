using BicycleCompany.PartModels.API.Boundary.Features;
using BicycleCompany.PartModels.API.Infrastructure;
using BicycleCompany.PartModels.API.Models;
using BicycleCompany.PartModels.API.Repositories.Extensions;
using BicycleCompany.PartModels.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BicycleCompany.PartModels.API.Repositories
{
    public class PartRepository : RepositoryBase<Part>, IPartRepository
    {
        public PartRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public Task CreatePartAsync(Part part) => CreateAsync(part);

        public Task DeletePartAsync(Part part) => DeleteAsync(part);

        public Task UpdatePartAsync(Part part) => UpdateAsync(part);

        public async Task<Part> GetPartAsync(Guid id) =>
            await FindByCondition(p => p.Id.Equals(id)).SingleOrDefaultAsync();

        public async Task<PagedList<Part>> GetPartsAsync(PartParameters partParameters)
        {
            var parts = await FindAll()
                .Search(partParameters.SearchTerm)
                .Sort(partParameters.OrderBy)
                .ToListAsync();

            return PagedList<Part>.ToPagedList(parts, partParameters.PageNumber, partParameters.PageSize);
        }

        public async Task<Part> GetPartByNameAsync(string name) =>
            await FindByCondition(p => p.Name.Equals(name)).SingleOrDefaultAsync();
    }
}
