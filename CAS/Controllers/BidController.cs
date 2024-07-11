using CAS.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Models.Models.DTO.Requests;
using repository.Interfaces;
using service.Interfaces;

namespace CAS.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class BidController : ControllerBase
    {
        private readonly IResponseService _responseService;
        private readonly IBidService _bidService;
        private readonly IAccountService _accountService;
        private readonly IAuctionedVehicleRepository _auctionedVehicleRepository;
        private readonly IAuctionService _auctionService;


        public BidController(IResponseService responseService, IBidService bidService, IAccountService accountService,
                             IAuctionedVehicleRepository auctionedVehicleRepository, IAuctionService auctionService)
        {
            _responseService = responseService;
            _bidService = bidService;
            _accountService = accountService;
            _auctionedVehicleRepository = auctionedVehicleRepository;
            _auctionService = auctionService;
        }

        [HttpPost]
        [Route("place-bid-in-auction-for-vehicle")]
        public async Task<CustomResponse> PlaceBidInAuctionForVehicle(BidRequestDTO bidDto)
        {
            try
            {
                var dbUser = await _accountService.GetAccountById(bidDto.UserId);

                if (dbUser is null)
                    return _responseService.Error(404, $"User not found with Id: {bidDto.UserId}");

                var dbAuctionedVehicle = await _auctionedVehicleRepository.GetAsync(bidDto.AuctionedVehicleId);

                if (dbAuctionedVehicle is null)
                    return _responseService.Error(404, $"AuctionedVehicle not found with Id {bidDto.AuctionedVehicleId}");

                if (dbAuctionedVehicle.IsRemovedFromAuction)
                    return _responseService.Error(403, "Forbidden - This vehicle was removed from the auction");

                var dbAuction = await _auctionService.GetAuctionByIdWithIncludeAsync(dbAuctionedVehicle.AuctionId);

                if (dbAuction is null)
                    return _responseService.Error(500, "Unable to find auction for given Auctioned Vehicle");

                if (dbAuction.EndDate != default && dbAuction.EndDate < DateTime.UtcNow)
                    return _responseService.Error(403, "Forbidden - The auction was closed");

                var placedBid = await _bidService.PlaceBid(bidDto.ToEntity());

                if (placedBid is null)
                    return _responseService.Error(400, "Bad Request - The bid to be placed do not attend the minimmum " +
                                                        "value of the car, or is smaller than the highest current bid");

                dbAuction.CurrentHighestBid = placedBid.OfferedBid;

                await _auctionService.UpdateAuctionAsync(dbAuction);

                return _responseService.Ok(placedBid);
            }
            catch (Exception ex)
            {
                return _responseService.UnknownError("Unknown Error occurred while trying to place bid", ex);
                //Implement Logging of exception.
            }
        }
    }
}
