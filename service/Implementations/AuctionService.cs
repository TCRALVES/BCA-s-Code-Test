using Models.Models.DBEntities;
using repository.Interfaces;
using service.Interfaces;

namespace service.Implementations
{
    public class AuctionService : IAuctionService
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IAuctionedVehicleRepository _auctionedVehicleRepository;
        private readonly IVehicleService _vehicleService;

        public AuctionService(IAuctionRepository auctionRepository, IAuctionedVehicleRepository auctionedVehicleRepository, IVehicleService vehicleService)
        {
            _auctionRepository = auctionRepository;
            _auctionedVehicleRepository = auctionedVehicleRepository;
            _vehicleService = vehicleService;
        }

        public async Task<Auction> CreateAuctionAsync(Auction auction)
        {
            auction.AuctionedVehicles = new List<AuctionedVehicle>();

            return await _auctionRepository.AddAsync(auction);
        }

        public async Task<Auction?> GetAuctionByIdWithIncludeAsync(int auctionId)
        {
            return await _auctionRepository.GetAuctionByIdAsync(auctionId);
        }

        public async Task<Auction> UpdateAuctionAsync(Auction auction)
            => await _auctionRepository.UpdateAsync(auction);

        public async Task<AuctionedVehicle> UpdateAuctionedVehicleAsync(AuctionedVehicle auctionedVehicle)
            => await _auctionedVehicleRepository.UpdateAsync(auctionedVehicle);

        public async Task<AuctionedVehicle?> GetAuctionedVehicleByVehicleIdAsync(int vehicleId)
            => await _auctionedVehicleRepository.GetAuctionedVehicleByVehicleIdAsync(vehicleId);

        public async Task<bool> IsVehicleAuctioned(Vehicle vehicle)
        {
            var auctionedVehicle = await _auctionedVehicleRepository.GetAuctionedVehicleByVehicleIdAsync(vehicle.Id);

            if (auctionedVehicle is null)
                return false;

            if (auctionedVehicle.IsRemovedFromAuction)
                return false;

            var dbAuction = await _auctionRepository.GetAsync(auctionedVehicle.AuctionId);

            if (dbAuction.EndDate != default && dbAuction.EndDate < DateTime.UtcNow)
                return false;

            return true;
        }

        public async Task<decimal?> GetStartingBidForAuctionedVehicle(int auctionedVehicleId)
        {
            var dbAuctionedVehicle = await _auctionedVehicleRepository.GetAsync(auctionedVehicleId);

            var dbVehicle = await _vehicleService.GetVehicleById(dbAuctionedVehicle.VehicleId);

            return dbVehicle?.StartingBid;
        }
    }
}
