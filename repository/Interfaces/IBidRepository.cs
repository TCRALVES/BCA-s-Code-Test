using Models.Models.DBEntities;

namespace repository.Interfaces
{
    public interface IBidRepository : IRepository<Bid>
    {
        Task<IEnumerable<Bid>> GetAllBidsForAuctionedVehicle(int auctionedVehicleId);
    }
}
