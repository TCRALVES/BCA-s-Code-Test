using Models.Models.DBEntities;

namespace Models.Models.DTO.Requests
{
    public class SUVRequestDTO : VehicleDTO
    {
        public int NumberOfSeats { get; set; }

        public SUV ToEntity()
        {
            return new SUV
            {
                Manufacturer = Manufacturer,
                Model = Model,
                NumberOfSeats = NumberOfSeats,
                StartingBid = StartingBid,
                UniqueIdentifier = UniqueIdentifier,
                Year = Year,
            };
        }
    }
}
