using Microsoft.EntityFrameworkCore;
using Models.Models.DBEntities;
using repository.BaseClasses;
using repository.Interfaces;

namespace repository.Implementations
{
    public class AuctionedVehicleRepository : RepositoryBase<AuctionedVehicle>, IRepository<AuctionedVehicle>, IAuctionedVehicleRepository
    {
        private readonly DbSet<AuctionedVehicle> _db;

        public AuctionedVehicleRepository(CASContext context) : base(context)
        {
            _db = context.Set<AuctionedVehicle>();
        }

        public Task<AuctionedVehicle?> GetAuctionedVehicleByVehicleIdAsync(int vehicleId)
            => _db.Where(x => x.VehicleId == vehicleId).FirstOrDefaultAsync();

    }
}
