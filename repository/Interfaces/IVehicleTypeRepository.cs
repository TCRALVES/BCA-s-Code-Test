using Models.Models.DBEntities;
using Models.Models.Enums;

namespace repository.Interfaces
{
    public interface IVehicleTypeRepository : IRepository<VehicleType>
    {
        Task<VehicleType?> GetVehicleTypeId(VehicleTypeEnum type);
    }
}
