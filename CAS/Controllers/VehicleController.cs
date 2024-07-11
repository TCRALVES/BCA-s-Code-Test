using CAS.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Models.Models.DTO.Requests;
using Models.Models.Enums;
using repository.Interfaces;
using service.Interfaces;

namespace CAS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IResponseService _responseService;
        private readonly IVehicleService _vehicleService;
        private readonly IVehicleTypeRepository _vehicleTypeRepository;

        public VehicleController(IResponseService responseService, IVehicleService vehicleService, IVehicleTypeRepository vehicleTypeRepository)
        {
            _responseService = responseService;
            _vehicleService = vehicleService;
            _vehicleTypeRepository = vehicleTypeRepository;
        }

        [HttpGet]
        [Route("get-vehicle-by-unique-identifier")]
        public async Task<CustomResponse> GetVehicleByUniqueIdentifier(string uniqueIdentifier)
        {
            try
            {
                var dbVehicle = await _vehicleService.GetVehicleByUniqueIdentifier(uniqueIdentifier);

                if (dbVehicle is null)
                {
                    return _responseService.Error(404, $"Vehicle not found with Unique Identifier: {uniqueIdentifier}");
                }

                return _responseService.Ok(dbVehicle);
            }
            catch (Exception ex)
            {
                return _responseService.UnknownError("Unknown Error occurred while trying to retrieve vehicle by Unique Identifier.", ex);
                //Implement Logging of exception.
            }
        }


        /// <summary>
        /// This endpoint requires an entry of the type of vehicles to be filtered out
        /// from the database.
        /// The possible values 0 = HATCHBACK, 1 = SEDAN, 2 = SUV, 3 = TRUCK
        /// Optional parameters are "manufacturer", "model", "year" which will be used to further filter the results
        /// </summary>
        /// <returns>Lista de produtos.</returns>
        [HttpGet]
        [Route("get-vehicles-by-type")]
        public async Task<CustomResponse> GetVehiclesByType([FromQuery] VehicleTypeEnum type, string? manufacturer, string? model, int? year)
        {
            try
            {
                var filteredVehicles = await _vehicleService.GetVehiclesByTypeAsync(type);

                if (!string.IsNullOrEmpty(manufacturer) || !string.IsNullOrEmpty(model) || year is not null)
                {
                    filteredVehicles = _vehicleService.FilterVehiclesByAttributes(filteredVehicles.AsQueryable(), manufacturer, model, year);
                }

                return _responseService.Ok(filteredVehicles);
            }
            catch (Exception ex)
            {
                return _responseService.UnknownError("Unknown Error occurred while trying to retrieve vehicle by type.", ex);
                //Implement Logging of exception.
            }
        }

        //[HttpGet]
        //[Route("get-vehicle-by-list-of-types")]
        //public async Task<CustomResponse> GetVehicleByListOfTypes([FromQuery] List<VehicleTypeEnum> types)
        //{
        //    try
        //    {
        //        return _responseService.Ok(await _vehicleService.GetVehiclesByListOfTypesAsync(types.ToArray()));
        //    }
        //    catch (Exception ex)
        //    {
        //        return _responseService.UnknownError("Unknown Error occurred while trying to retrieve vehicle by type.", ex);
        //        //Implement Logging of exception.
        //    }
        //}

        [HttpPost]
        [Route("add-sedan")]
        public async Task<CustomResponse> AddSedan(SedanRequestDTO sedanDto)
        {
            try
            {
                var dbVehicle = await _vehicleService.GetVehicleByUniqueIdentifier(sedanDto.UniqueIdentifier);

                if (dbVehicle is not null)
                {
                    return _responseService.Error(409, $"Vehicle with Unique Identifier: {sedanDto.UniqueIdentifier} already registered.");
                }

                var sedan = sedanDto.ToEntity();
                var vehicleType = await _vehicleTypeRepository.GetVehicleTypeId(VehicleTypeEnum.SEDAN);

                if (vehicleType is null)
                {
                    string vehicleTypeName = VehicleTypeEnum.SEDAN.ToString();
                    return _responseService.Error(500, $"No registered vehicle type {vehicleTypeName} found.");
                }

                sedan.VehicleTypeId = vehicleType.Id;

                return _responseService.Ok(await _vehicleService.AddSedanAsync(sedan));
            }
            catch (Exception ex)
            {
                return _responseService.UnknownError($"Unknown Error occurred while trying to add vehicle with Unique Identifier {sedanDto.UniqueIdentifier}", ex);
                //Implement Logging of exception.
            }
        }

        [HttpPost]
        [Route("add-suv")]
        public async Task<CustomResponse> AddSUV(SUVRequestDTO suvDto)
        {
            try
            {
                var dbVehicle = await _vehicleService.GetVehicleByUniqueIdentifier(suvDto.UniqueIdentifier);

                if (dbVehicle is not null)
                {
                    return _responseService.Error(409, $"Vehicle with Unique Identifier: {suvDto.UniqueIdentifier} already registered.");
                }

                var suv = suvDto.ToEntity();
                var vehicleType = await _vehicleTypeRepository.GetVehicleTypeId(VehicleTypeEnum.SUV);

                if (vehicleType is null)
                {
                    string vehicleTypeName = VehicleTypeEnum.SUV.ToString();
                    return _responseService.Error(500, $"No registered vehicle type {vehicleTypeName} found.");
                }

                suv.VehicleTypeId = vehicleType.Id;

                return _responseService.Ok(await _vehicleService.AddSUVAsync(suv));
            }
            catch (Exception ex)
            {
                return _responseService.UnknownError("Unknown Error occurred while trying to retrieve vehicle by type.", ex);
                //Implement Logging of exception.
            }
        }

        [HttpPost]
        [Route("add-hatchback")]
        public async Task<CustomResponse> AddHatchback(HatchbackRequestDTO hatchbackDto)
        {
            try
            {
                var dbVehicle = await _vehicleService.GetVehicleByUniqueIdentifier(hatchbackDto.UniqueIdentifier);

                if (dbVehicle is not null)
                {
                    return _responseService.Error(409, $"Vehicle with Unique Identifier: {hatchbackDto.UniqueIdentifier} already registered.");
                }

                var hatchback = hatchbackDto.ToEntity();
                var vehicleType = await _vehicleTypeRepository.GetVehicleTypeId(VehicleTypeEnum.HATCHBACK);

                if (vehicleType is null)
                {
                    string vehicleTypeName = VehicleTypeEnum.HATCHBACK.ToString();
                    return _responseService.Error(500, $"No registered vehicle type {vehicleTypeName} found.");
                }

                hatchback.VehicleTypeId = vehicleType.Id;

                return _responseService.Ok(await _vehicleService.AddHatchbackAsync(hatchback));
            }
            catch (Exception ex)
            {
                return _responseService.UnknownError("Unknown Error occurred while trying to retrieve vehicle by type.", ex);
                //Implement Logging of exception.
            }
        }

        [HttpPost]
        [Route("add-truck")]
        public async Task<CustomResponse> AddTruck(TruckRequestDTO truckDto)
        {
            try
            {
                var dbVehicle = await _vehicleService.GetVehicleByUniqueIdentifier(truckDto.UniqueIdentifier);

                if (dbVehicle is not null)
                {
                    return _responseService.Error(409, $"Vehicle with Unique Identifier: {truckDto.UniqueIdentifier} already registered.");
                }

                var truck = truckDto.ToEntity();
                var vehicleType = await _vehicleTypeRepository.GetVehicleTypeId(VehicleTypeEnum.TRUCK);

                if (vehicleType is null)
                {
                    string vehicleTypeName = VehicleTypeEnum.TRUCK.ToString();
                    return _responseService.Error(500, $"No registered vehicle type {vehicleTypeName} found.");
                }

                truck.VehicleTypeId = vehicleType.Id;

                return _responseService.Ok(await _vehicleService.AddTruckAsync(truck));
            }
            catch (Exception ex)
            {
                return _responseService.UnknownError("Unknown Error occurred while trying to retrieve vehicle by type.", ex);
                //Implement Logging of exception.
            }
        }
    }
}
