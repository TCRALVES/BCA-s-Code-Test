using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Models.DBEntities;

namespace repository.EntityConfigurations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasIndex(p => p.UniqueIdentifier)
                   .IsUnique();

            builder.Property(v => v.StartingBid);
            builder.Property(v => v.Model);
            builder.Property(v => v.Year);
            builder.Property(v => v.Manufacturer);

            builder.HasOne(v => v.VehicleType).WithOne()
                   .HasForeignKey<Vehicle>(ep => ep.VehicleTypeId);

            builder.HasQueryFilter(q => !(q is Hatchback));
            builder.HasQueryFilter(q => !(q is Sedan));
            builder.HasQueryFilter(q => !(q is SUV));
            builder.HasQueryFilter(q => !(q is Truck));
        }
    }
}
