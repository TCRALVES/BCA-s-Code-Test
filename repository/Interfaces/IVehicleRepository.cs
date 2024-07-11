using Models.Models.DBEntities;
using Models.Models.Enums;

namespace repository.Interfaces
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        Task<IEnumerable<Vehicle>> GetHatchbacksAsync();
        Task<IEnumerable<Vehicle>> GetSedansAsync();
        Task<IEnumerable<Vehicle>> GetSUVsAsync();
        Task<IEnumerable<Vehicle>> GetTrucksAsync();
        Task<Vehicle?> GetVehicleByUniqueIdentifier(string uniqueIdentifier);
        Task<IEnumerable<Vehicle>> GetVehiclesByListOfTypesAsync(VehicleTypeEnum[] types);
    }
}
