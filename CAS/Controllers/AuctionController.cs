using CAS.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Models.Models.DBEntities;
using Models.Models.DTO.Requests;
using service.Interfaces;

namespace CAS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuctionController : Controller
    {
        private readonly IResponseService _responseService;
        private readonly IAuctionService _auctionService;
        private readonly IVehicleService _vehicleService;

        public AuctionController(IAuctionService auctionService, IResponseService responseService, IVehicleService vehicleService)
        {
            _auctionService = auctionService;
            _responseService = responseService;
            _vehicleService = vehicleService;
        }

        [HttpPost("create-auction")]
        public async Task<CustomResponse> CreateAuction(AuctionRequestDTO auctionDTO)
        {
            try
            {
                if (auctionDTO.StartDate != default && auctionDTO.StartDate < DateTime.UtcNow)
                    return _responseService.Error(400, "Auction cannot be created with a start date before the current date.");

                if (auctionDTO.StartDate == default)
                {
                    auctionDTO.StartDate = DateTime.UtcNow;
                }

                var dbAuction = await _auctionService.CreateAuctionAsync(auctionDTO.ToEntity());

                return _responseService.Ok(dbAuction);
            }
            catch (Exception ex)
            {
                return _responseService.UnknownError("Unknown Error occurred while trying to create an auction", ex);
            }
        }

        [HttpPut("set-end-date-for-auction")]
        public async Task<CustomResponse> SetEndDateForAuction(DateTime endDate, int auctionId)
        {
            try
            {
                if (endDate < DateTime.UtcNow)
                    return _responseService.Error(400, "Expected end date cannot be set to earlier than the current time.");

                var dbAuction = await _auctionService.GetAuctionByIdWithIncludeAsync(auctionId);

                if (dbAuction is null)
                {
                    return _responseService.Error(404, $"Auction not found for provided Id: {auctionId}");
                }

                dbAuction.EndDate = endDate;
                await _auctionService.UpdateAuctionAsync(dbAuction);

                return _responseService.Ok(dbAuction);
            }
            catch (Exception ex)
            {
                return _responseService.UnknownError("Unknown Error occurred while trying to set end date for auction", ex);
            }
        }

        [HttpPut("close/{auctionId}")]
        public async Task<CustomResponse> CloseAuction(int auctionId)
        {
            try
            {
                var auction = await _auctionService.GetAuctionByIdWithIncludeAsync(auctionId);

                if (auction is null)
                    return _responseService.Error(404, $"Auction not found with Id: {auctionId}");

                auction.EndDate = DateTime.UtcNow;
                await _auctionService.UpdateAuctionAsync(auction);

                return _responseService.Ok(auction);
            }
            catch (Exception ex)
            {
                return _responseService.UnknownError("Unknown Error occurred while trying to close auction", ex);
            }
        }

        [HttpPost]
        [Route("add-vehicle-to-auction-inventory")]
        public async Task<CustomResponse> AddVehicleToAuctionInventory(string uniqueIdentifier, int auctionId)
        {
            try
            {
                var dbVehicle = await _vehicleService.GetVehicleByUniqueIdentifier(uniqueIdentifier);

                if (dbVehicle is null)
                {
                    return _responseService.Error(404, $"Vehicle not found with provided Unique Identifier: {uniqueIdentifier}");
                }

                bool isAuctioned = await _auctionService.IsVehicleAuctioned(dbVehicle);

                if (isAuctioned)
                    return _responseService.Error(403, $"Forbidden - Vehicle with Unique Identifier: {uniqueIdentifier} is already being auctioned");

                var dbAuction = await _auctionService.GetAuctionByIdWithIncludeAsync(auctionId);

                if (dbAuction is null)
                {
                    return _responseService.Error(404, $"Vehicle not found with provided Unique Identifier: {uniqueIdentifier}");
                }

                dbAuction.AuctionedVehicles.Add(new AuctionedVehicle
                {
                    AuctionId = dbAuction.Id,
                    VehicleId = dbVehicle.Id
                });

                await _auctionService.UpdateAuctionAsync(dbAuction);

                return _responseService.Ok(dbAuction);
            }
            catch (Exception ex)
            {
                return _responseService.UnknownError("Unknown Error occurred while trying to retrieve user by email.", ex);
                //Implement Logging of exception.
            }
        }

        [HttpPut]
        [Route("remove-vehicle-from-auction-inventory")]
        public async Task<CustomResponse> RemoveVehicleFromAuctionInventory(string uniqueIdentifier, int auctionId)
        {
            try
            {
                var dbVehicle = await _vehicleService.GetVehicleByUniqueIdentifier(uniqueIdentifier);

                if (dbVehicle is null)
                {
                    return _responseService.Error(404, $"Vehicle not found with provided Unique Identifier: {uniqueIdentifier}");
                }

                var dbAutctionedVehicle = await _auctionService.GetAuctionedVehicleByVehicleIdAsync(dbVehicle.Id);

                if (dbAutctionedVehicle is null)
                    return _responseService.Error(404, $"Vehicle not associated to auction with Id: {auctionId}");

                if (dbAutctionedVehicle.IsRemovedFromAuction)
                    return _responseService.Error(201, "Vehicle was already removed from auction");

                dbAutctionedVehicle.IsRemovedFromAuction = true;

                await _auctionService.UpdateAuctionedVehicleAsync(dbAutctionedVehicle);

                return _responseService.Ok(dbAutctionedVehicle);
            }
            catch (Exception ex)
            {
                return _responseService.UnknownError("Unknown Error occurred while trying to remove vehicle from auction inventory", ex);
                //Implement Logging of exception.
            }
        }
    }
}
