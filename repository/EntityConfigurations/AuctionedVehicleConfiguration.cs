using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Models.DBEntities;

namespace repository.EntityConfigurations
{
    public class AuctionedVehicleConfiguration : IEntityTypeConfiguration<AuctionedVehicle>
    {
        public void Configure(EntityTypeBuilder<AuctionedVehicle> builder)
        {
            builder.HasKey(av => av.Id);

            builder.Property(av => av.IsRemovedFromAuction);

            builder.HasOne(av => av.Vehicle).WithOne()
                   .HasForeignKey<AuctionedVehicle>(ep => ep.VehicleId);

            builder.Ignore(av => av.Auction);
        }
    }
}
