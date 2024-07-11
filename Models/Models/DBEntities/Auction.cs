using Abp.Domain.Entities;

namespace Models.Models.DBEntities
{
    public class Auction : Entity, IEntity
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<AuctionedVehicle> AuctionedVehicles { get; set; }

        public decimal CurrentHighestBid { get; set; }

        public bool IsActive => EndDate == default(DateTime);
    }
}
