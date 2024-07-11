using Models.Models.DBEntities;

namespace repository.Interfaces
{
    public interface IAuctionedVehicleRepository : IRepository<AuctionedVehicle>
    {
        Task<AuctionedVehicle?> GetAuctionedVehicleByVehicleIdAsync(int vehicleId);
    }
}
