using BicycleCompany.PartModels.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BicycleCompany.PartModels.API.Repositories.Interfaces
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext _repositoryContext;

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            this._repositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll() => _repositoryContext.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            _repositoryContext.Set<T>().Where(expression).AsNoTracking();

        public Task CreateAsync(T entity)
        {
            _repositoryContext.Set<T>().Add(entity);
            return _repositoryContext.SaveChangesAsync();
        }

        public Task UpdateAsync(T entity)
        {
            _repositoryContext.Set<T>().Update(entity);
            return _repositoryContext.SaveChangesAsync();
        }

        public Task DeleteAsync(T entity)
        {
            _repositoryContext.Set<T>().Remove(entity);
            return _repositoryContext.SaveChangesAsync();
        }
    }
}
