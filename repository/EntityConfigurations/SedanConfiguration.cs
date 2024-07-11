using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Models.DBEntities;

namespace repository.EntityConfigurations
{
    public class SedanConfiguration : IEntityTypeConfiguration<Sedan>
    {
        public void Configure(EntityTypeBuilder<Sedan> builder)
        {
            builder.Property(x => x.NumberOfDoors);

            builder.HasBaseType(typeof(Vehicle));
        }
    }
}
