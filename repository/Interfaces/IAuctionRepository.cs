using Models.Models.DBEntities;

namespace repository.Interfaces
{
    public interface IAuctionRepository : IRepository<Auction>
    {
        Task<Auction?> GetAuctionByIdAsync(int auctionId);
        Task<Auction?> GetAuctionByIdWithIncludeAsync(int auctionId);
    }
}
