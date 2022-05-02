using BicycleCompany.PartModels.API.Infrastructure;
using BicycleCompany.PartModels.API.Models;
using BicycleCompany.PartModels.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BicycleCompany.PartModels.API.Repositories
{
    public class ManufacturerRepository : RepositoryBase<Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }
        public async Task<List<Manufacturer>> GetAllAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<Manufacturer> GetByIdAsync(Guid id)
        {
            return await FindByCondition(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Manufacturer> GetByNameAsync(string name)
        {
            return await FindByCondition(m => m.Name == name).FirstOrDefaultAsync();
        }
    }
}
