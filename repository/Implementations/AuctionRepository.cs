using Microsoft.EntityFrameworkCore;
using Models.Models.DBEntities;
using repository.BaseClasses;
using repository.Interfaces;

namespace repository.Implementations
{
    public class AuctionRepository : RepositoryBase<Auction>, IRepository<Auction>, IAuctionRepository
    {
        private readonly DbSet<Auction> _db;

        public AuctionRepository(CASContext context) : base(context)
        {
            _db = context.Set<Auction>();
        }

        public async Task<Auction?> GetAuctionByIdWithIncludeAsync(int auctionId)
            => await _db.Where(x => x.Id == auctionId).Include(x => x.AuctionedVehicles).FirstOrDefaultAsync();

        public async Task<Auction?> GetAuctionByIdAsync(int auctionId)
            => await _db.Select(a => new Auction
            {
                Id = a.Id,
                CurrentHighestBid = a.CurrentHighestBid,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                AuctionedVehicles = a.AuctionedVehicles
                                .Select(e => new AuctionedVehicle
                                {
                                    Id = e.Id,
                                    AuctionId = e.AuctionId,
                                    IsRemovedFromAuction = e.IsRemovedFromAuction,
                                    VehicleId = e.VehicleId
                                }).ToList()
            }).FirstOrDefaultAsync();
    }
}
