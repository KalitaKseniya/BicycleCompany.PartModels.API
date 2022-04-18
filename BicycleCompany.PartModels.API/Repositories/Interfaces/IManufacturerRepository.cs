using BicycleCompany.PartModels.API.Models;
using System.Linq.Expressions;

namespace BicycleCompany.PartModels.API.Repositories.Interfaces
{
    public interface IManufacturerRepository
    {
        Task<List<Manufacturer>> GetAllAsync();
        Task<Manufacturer> GetByIdAsync(Guid id);
        Task<Manufacturer> GetByNameAsync(string name);
        Task CreateAsync(Manufacturer manufacturer);
        Task DeleteAsync(Manufacturer manufacturer);
        Task UpdateAsync(Manufacturer manufacturer);
        bool Exist(Expression<Func<Manufacturer, bool>> expression);
    }
}
