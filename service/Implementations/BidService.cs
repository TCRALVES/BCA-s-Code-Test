using Models.Models.DBEntities;
using repository.Interfaces;
using service.Interfaces;

namespace service.Implementations
{
    public class BidService : IBidService
    {
        private readonly IBidRepository _bidRepository;
        private readonly IAuctionService _auctionService;

        public BidService(IBidRepository bidRepository, IAuctionService auctionService)
        {
            _bidRepository = bidRepository;
            _auctionService = auctionService;
        }

        public async Task<Bid?> PlaceBid(Bid bid)
        {
            var bids = await GetAllBidsForAuctionedVehicle(bid.AuctionedVehicleId);

            if (bids.Any())
            {
                if (IsHighestBid(bids, bid.OfferedBid))
                    return await _bidRepository.AddAsync(bid);

                return null;
            }

            if(bid.OfferedBid > await _auctionService.GetStartingBidForAuctionedVehicle(bid.AuctionedVehicleId))
                return await _bidRepository.AddAsync(bid);

            return null;
        }

        public async Task<IEnumerable<Bid>> GetAllBidsForAuctionedVehicle(int auctionedVehicle)
        {
            return await _bidRepository.GetAllBidsForAuctionedVehicle(auctionedVehicle);
        }

        private bool IsHighestBid(IEnumerable<Bid> bids, decimal offeredBid)
            => offeredBid > bids.Select(x => x.OfferedBid).Max();
    }
}
