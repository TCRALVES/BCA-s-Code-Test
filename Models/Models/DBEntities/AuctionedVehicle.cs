using Abp.Domain.Entities;

namespace Models.Models.DBEntities
{
    public class AuctionedVehicle : Entity, IEntity
    {
        public int VehicleId { get; set; }

        public int AuctionId { get; set; }

        public bool IsRemovedFromAuction { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public virtual Auction Auction { get; set; }
    }
}
