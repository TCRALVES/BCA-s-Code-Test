using Models.Models.DBEntities;

namespace Models.Models.DTO.Requests
{
    public class TruckRequestDTO : VehicleDTO
    {
        public decimal LoadCapacity { get; set; }

        public Truck ToEntity()
        {
            return new Truck
            {
                LoadCapacity = LoadCapacity,
                Year = Year,
                VehicleTypeId = VehicleTypeId,
                Manufacturer = Manufacturer,
                Model = Model,
                StartingBid = StartingBid,
                UniqueIdentifier = UniqueIdentifier,
            };
        }
    }
}
