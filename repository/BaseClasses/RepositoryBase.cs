using Abp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using repository.Interfaces;

namespace repository.BaseClasses
{
    public class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly CASContext _context;

        public RepositoryBase(CASContext context)
        {
            _context = context;
        }

        public virtual async Task<int> AddManyAsync(IEnumerable<TEntity> entities)
        {
            int added = 0;

            foreach (var entity in entities)
            {
                added += await _context.AddAsync(entity) != null ? 1 : 0;
            }

            await _context.SaveChangesAsync();

            return added;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> DeleteAsync(TEntity toRemove)
        {
            if (toRemove == null)
                return toRemove;

            await Task.Run(() => _context.Remove(toRemove));
            await _context.SaveChangesAsync();

            return toRemove;
        }

        public virtual async Task<TEntity> DeleteAsync(int id)
        {
            var toRemove = await GetAsync(id);
            return await DeleteAsync(toRemove);
        }

        public virtual async Task<TEntity> GetAsync(int id)
            => (TEntity)await _context.FindAsync(typeof(TEntity), id);


        public virtual async Task<IEnumerable<TEntity>> ListAsync()
            => await _context.Set<TEntity>().ToListAsync();


        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<int> LastIdAsync(TEntity entity)
            => await _context.Set<TEntity>()
            .AsNoTracking()
            .AsQueryable()
            .OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstAsync();

    }
}
