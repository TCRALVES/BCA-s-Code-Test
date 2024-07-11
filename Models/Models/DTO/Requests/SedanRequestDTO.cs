using Models.Models.DBEntities;

namespace Models.Models.DTO.Requests
{
    public class SedanRequestDTO : VehicleDTO
    {
        public int NumberOfDoors { get; set; }

        public Sedan ToEntity()
        {
            return new Sedan
            {
                Manufacturer = Manufacturer,
                Model = Model,
                NumberOfDoors = NumberOfDoors,
                StartingBid = StartingBid,
                UniqueIdentifier = UniqueIdentifier,
                Year = Year,
            };
        }
    }
}
