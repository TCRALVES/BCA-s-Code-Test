using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Models.DBEntities;

namespace repository.EntityConfigurations
{
    public class SUVConfiguration : IEntityTypeConfiguration<SUV>
    {
        public void Configure(EntityTypeBuilder<SUV> builder)
        {
            builder.Property(x => x.NumberOfSeats);

            builder.HasBaseType(typeof(Vehicle));
        }
    }
}
