using Microsoft.EntityFrameworkCore;
using Models.Models.DBEntities;
using repository.BaseClasses;
using repository.Interfaces;

namespace repository.Implementations
{
    public class SUVRepository : RepositoryBase<SUV>, IRepository<SUV>, ISUVRepository
    {
        private readonly DbSet<SUV> _db;

        public SUVRepository(CASContext context) : base(context)
        {
            _db = context.Set<SUV>();
        }
    }
}
