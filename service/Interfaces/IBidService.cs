using Models.Models.DBEntities;

namespace service.Interfaces
{
    public interface IBidService
    {
        Task<IEnumerable<Bid>> GetAllBidsForAuctionedVehicle(int auctionedVehicle);
        Task<Bid?> PlaceBid(Bid bid);
    }
}
