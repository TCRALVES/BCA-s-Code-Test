using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Models.DBEntities;

namespace repository.EntityConfigurations
{
    public class BidConfiguration : IEntityTypeConfiguration<Bid>
    {
        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.OfferedBid);

            builder.HasOne(v => v.AuctionedVehicle).WithOne()
                   .HasForeignKey<Bid>(ep => ep.AuctionedVehicleId);

            builder.HasOne(v => v.User).WithOne()
                   .HasForeignKey<Bid>(ep => ep.UserId);
        }
    }
}
