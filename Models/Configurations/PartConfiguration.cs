using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace POSIndexer.Models.Configurations
{
    public class PartConfiguration
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Car)
                .WithMany(c => c.Parts)
                .HasForeignKey("Car_Id")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
