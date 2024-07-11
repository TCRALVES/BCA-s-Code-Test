using Models.Models.DBEntities;
using Models.Models.Enums;
using Models.Models.Extensions;
using repository.Interfaces;
using service.Interfaces;
using System.Linq.Expressions;

namespace service.Implementations
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ISedanRepository _sedanRepository;
        private readonly ISUVRepository _SUVRepository;
        private readonly ITruckRepository _truckRepository;
        private readonly IHatchbackRepository _hatchbackRepository;

        public VehicleService(IVehicleRepository vehicleRepository,
                                ISedanRepository sedanRepository,
                                ISUVRepository SUVRepository,
                                ITruckRepository truckRepository,
                                IHatchbackRepository hatchbackRepository)
        {
            _vehicleRepository = vehicleRepository;
            _sedanRepository = sedanRepository;
            _SUVRepository = SUVRepository;
            _truckRepository = truckRepository;
            _hatchbackRepository = hatchbackRepository;
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesByTypeAsync(VehicleTypeEnum type)
        {
            switch (type)
            {
                case VehicleTypeEnum.HATCHBACK:
                    return await _vehicleRepository.GetHatchbacksAsync();
                case VehicleTypeEnum.SEDAN:
                    return await _vehicleRepository.GetSedansAsync();
                case VehicleTypeEnum.SUV:
                    return await _vehicleRepository.GetSUVsAsync();
                case VehicleTypeEnum.TRUCK:
                    return await _vehicleRepository.GetTrucksAsync();
                default:
                    return Enumerable.Empty<Vehicle>();
            }
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesByListOfTypesAsync(VehicleTypeEnum[] types)
        {
            List<Vehicle> vehicles = [];

            foreach (var type in types)
            {
                vehicles.AddRange(await GetVehiclesByTypeAsync(type));
            }

            return vehicles;
        }

        public async Task<Vehicle?> GetVehicleByUniqueIdentifier(string uniqueIdentifier)
        {
            return await _vehicleRepository.GetVehicleByUniqueIdentifier(uniqueIdentifier);
        }

        public async Task<Vehicle?> GetVehicleById(int vehicleId)
        {
            return await _vehicleRepository.GetAsync(vehicleId);
        }

        public async Task<Sedan> AddSedanAsync(Sedan sedan)
        {
            return await _sedanRepository.AddAsync(sedan);
        }

        public async Task<SUV> AddSUVAsync(SUV suv)
        {
            return await _SUVRepository.AddAsync(suv);
        }

        public async Task<Truck> AddTruckAsync(Truck truck)
        {
            return await _truckRepository.AddAsync(truck);
        }

        public async Task<Hatchback> AddHatchbackAsync(Hatchback hatchback)
        {
            return await _hatchbackRepository.AddAsync(hatchback);
        }

        public IEnumerable<Vehicle> FilterVehiclesByAttributes(IQueryable<Vehicle> vehicles, string? manufacturer, string? model, int? year)
        {
            List<Expression<Func<Vehicle, bool>>> filters = new List<Expression<Func<Vehicle, bool>>>();

            if (!string.IsNullOrEmpty(manufacturer))
            {
                filters.Add(p => p.Manufacturer == manufacturer);
            }

            if (!string.IsNullOrEmpty(model))
            {
                filters.Add(p => p.Model == model);
            }

            if (year is not null && year != 0)
            {
                filters.Add(p => p.Year == year);
            }

            Expression<Func<Vehicle, bool>> aggregatePredicate = filters.Aggregate((firstExp, nextExp) => firstExp.And(nextExp));

            vehicles = vehicles.Where(aggregatePredicate);

            return vehicles;
        }
    }
}
