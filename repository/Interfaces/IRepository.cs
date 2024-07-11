using Abp.Domain.Entities;

namespace repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<TEntity> UpdateAsync(TEntity t);

        Task<int> AddManyAsync(IEnumerable<TEntity> t);

        Task<TEntity> AddAsync(TEntity t);

        Task<TEntity> DeleteAsync(TEntity id);

        Task<TEntity> DeleteAsync(int id);

        Task<TEntity> GetAsync(int id);

        Task<IEnumerable<TEntity>> ListAsync();

        Task<int> LastIdAsync(TEntity entity);
    }
}
