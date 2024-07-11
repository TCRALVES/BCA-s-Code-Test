using Microsoft.EntityFrameworkCore;
using Models.Models.DBEntities;
using repository.BaseClasses;
using repository.Interfaces;

namespace repository.Implementations
{
    public class TruckRepository : RepositoryBase<Truck>, IRepository<Truck>, ITruckRepository
    {
        private readonly DbSet<Truck> _db;

        public TruckRepository(CASContext context) : base(context)
        {
            _db = context.Set<Truck>();
        }
    }
}
