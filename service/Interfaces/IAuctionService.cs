using Models.Models.DBEntities;

namespace service.Interfaces
{
    public interface IAuctionService
    {
        Task<Auction> CreateAuctionAsync(Auction auction);
        Task<Auction?> GetAuctionByIdWithIncludeAsync(int auctionId);
        Task<AuctionedVehicle?> GetAuctionedVehicleByVehicleIdAsync(int vehicleId);
        Task<decimal?> GetStartingBidForAuctionedVehicle(int auctionedVehicleId);
        Task<bool> IsVehicleAuctioned(Vehicle vehicle);
        Task<Auction> UpdateAuctionAsync(Auction auction);
        Task<AuctionedVehicle> UpdateAuctionedVehicleAsync(AuctionedVehicle auctionedVehicle);
    }
}
