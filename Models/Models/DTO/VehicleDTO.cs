namespace Models.Models.DTO
{
    public class VehicleDTO
    {
        public string UniqueIdentifier { get; set; }

        public decimal StartingBid { get; set; }

        //The Model/Manufacturer could be improved by setting as separated entities
        //in the form of lists containing a seed of them.
        public string Model { get; set; }

        public string Manufacturer { get; set; }

        public int Year { get; set; }

        public int VehicleTypeId { get; set; }
    }
}
