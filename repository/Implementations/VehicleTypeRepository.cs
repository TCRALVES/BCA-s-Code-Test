using Microsoft.EntityFrameworkCore;
using Models.Models.DBEntities;
using Models.Models.Enums;
using repository.BaseClasses;
using repository.Interfaces;

namespace repository.Implementations
{
    public class VehicleTypeRepository : RepositoryBase<VehicleType>, IRepository<VehicleType>, IVehicleTypeRepository
    {
        private readonly DbSet<VehicleType> _db;

        public VehicleTypeRepository(CASContext context) : base(context)
        {
            _db = context.Set<VehicleType>();
        }

        public async Task<VehicleType?> GetVehicleTypeId(VehicleTypeEnum type)
        {
            return await _db.Where(x => x.TypeName.ToLower() == type.ToString().ToLower()).FirstOrDefaultAsync();
        }
    }
}
