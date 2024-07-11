using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Models.DBEntities;

namespace repository.EntityConfigurations
{
    public class HatchbackConfiguration : IEntityTypeConfiguration<Hatchback>
    {
        public void Configure(EntityTypeBuilder<Hatchback> builder)
        {
            builder.Property(x => x.NumberOfDoors);

            builder.HasBaseType(typeof(Vehicle));
        }
    }
}
