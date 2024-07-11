using Models.Models.DBEntities;

namespace Models.Models.DTO.Requests
{
    public class AuctionRequestDTO
    {
        public DateTime StartDate { get; set; }

        public Auction ToEntity()
        {
            return new Auction
            {
                StartDate = StartDate,
            };
        }
    }
}
