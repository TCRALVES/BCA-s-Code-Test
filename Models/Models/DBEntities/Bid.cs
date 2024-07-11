using Abp.Domain.Entities;
using CAS.Models.DBEntities;

namespace Models.Models.DBEntities
{
    public class Bid : Entity, IEntity
    {
        public int UserId { get; set; }

        public int AuctionedVehicleId { get; set; }

        public decimal OfferedBid { get; set; }

        public UserEntity User { get; set; }

        public AuctionedVehicle AuctionedVehicle { get; set; }
    }
}
