using Abp.Domain.Entities;

namespace Models.Models.DBEntities
{
    public class Vehicle : Entity, IEntity
    {
        //This property was set as a string to support VIN Number entries
        public string UniqueIdentifier { get; set; }

        public decimal StartingBid { get; set; }

        //The Model/Manufacturer could me improved by setting as separated entities
        //in the form of lists containing a seed of them.
        public string Model { get; set; }

        public string Manufacturer { get; set; }

        public int Year { get; set; }

        public int VehicleTypeId { get; set; }

        public VehicleType VehicleType { get; set; }
    }
}
