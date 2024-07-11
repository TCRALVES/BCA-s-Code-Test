using Models.Models.DBEntities;
using Models.Models.Enums;

namespace service.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> GetVehiclesByTypeAsync(VehicleTypeEnum type);
        Task<IEnumerable<Vehicle>> GetVehiclesByListOfTypesAsync(VehicleTypeEnum[] types);
        Task<Sedan> AddSedanAsync(Sedan sedan);
        Task<SUV> AddSUVAsync(SUV suv);
        Task<Truck> AddTruckAsync(Truck truck);
        Task<Hatchback> AddHatchbackAsync(Hatchback hatchback);
        Task<Vehicle?> GetVehicleByUniqueIdentifier(string uniqueIdentifier);
        IEnumerable<Vehicle> FilterVehiclesByAttributes(IQueryable<Vehicle> vehicles, string? manufacturer, string? model, int? year);
        Task<Vehicle?> GetVehicleById(int vehicleId);
    }
}
