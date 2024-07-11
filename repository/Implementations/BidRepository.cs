using Microsoft.EntityFrameworkCore;
using Models.Models.DBEntities;
using repository.BaseClasses;
using repository.Interfaces;

namespace repository.Implementations
{
    public class BidRepository : RepositoryBase<Bid>, IRepository<Bid>, IBidRepository
    {
        private readonly DbSet<Bid> _db;

        public BidRepository(CASContext context) : base(context)
        {
            _db = context.Set<Bid>();
        }

        public async Task<IEnumerable<Bid>> GetAllBidsForAuctionedVehicle(int auctionedVehicleId)
            => await _db.Where(x => x.AuctionedVehicleId == auctionedVehicleId).ToArrayAsync();
    }
}
