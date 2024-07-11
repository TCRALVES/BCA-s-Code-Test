using Models.Models.DBEntities;

namespace Models.Models.DTO.Requests
{
    public class BidRequestDTO
    {
        public int UserId { get; set; }

        public int AuctionedVehicleId { get; set; }

        public decimal OfferedBid { get; set; }

        public Bid ToEntity()
        {
            return new Bid
            {
                AuctionedVehicleId = AuctionedVehicleId,
                OfferedBid = OfferedBid,
                UserId = UserId
            };
        }
    }
}
