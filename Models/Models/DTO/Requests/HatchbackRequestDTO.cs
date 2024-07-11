using Models.Models.DBEntities;

namespace Models.Models.DTO.Requests
{
    public class HatchbackRequestDTO : VehicleDTO
    {
        public int NumberOfDoors { get; set; }

        public Hatchback ToEntity()
        {
            return new Hatchback
            {
                NumberOfDoors = NumberOfDoors,
                Manufacturer = Manufacturer,
                Model = Model,
                StartingBid = StartingBid,
                UniqueIdentifier = UniqueIdentifier,
                VehicleTypeId = VehicleTypeId,
                Year = Year,
            };
        }
    }
}
