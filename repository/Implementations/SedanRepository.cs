using Microsoft.EntityFrameworkCore;
using Models.Models.DBEntities;
using repository.BaseClasses;
using repository.Interfaces;

namespace repository.Implementations
{
    public class SedanRepository : RepositoryBase<Sedan>, IRepository<Sedan>, ISedanRepository
    {
        private readonly DbSet<Sedan> _db;

        public SedanRepository(CASContext context) : base(context)
        {
            _db = context.Set<Sedan>();
        }
    }
}
