namespace Models.Models.DTO
{
    public class AuctionedVehicleDTO
    {
        public int AuctionedVehicleId { get; set; }

        public decimal HighestBid { get; set; }

        public int HighestBidderUserId { get; set; }

        public bool GotBids => HighestBid != 0;
    }
}
