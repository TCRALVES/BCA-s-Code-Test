using Microsoft.EntityFrameworkCore;
using Models.Models.DBEntities;
using repository.BaseClasses;
using repository.Interfaces;

namespace repository.Implementations
{
    public class HatchbackRepository : RepositoryBase<Hatchback>, IRepository<Hatchback>, IHatchbackRepository
    {
        private readonly DbSet<Hatchback> _db;

        public HatchbackRepository(CASContext context) : base(context)
        {
            _db = context.Set<Hatchback>();
        }
    }
}
