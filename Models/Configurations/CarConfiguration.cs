using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace POSIndexer.Models.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.Parts)
                   .WithOne(p => p.Car) 
                   .OnDelete(DeleteBehavior.Cascade) 
                   .IsRequired();
        }
    }
}
