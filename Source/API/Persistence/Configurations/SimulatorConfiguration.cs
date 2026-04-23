using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.API.Models;

namespace Platform.API.Persistence.Configurations;

public class SimulatorConfiguration : IEntityTypeConfiguration<Simulator>
{
    public void Configure(EntityTypeBuilder<Simulator> entity)
    {
        entity.Property(x => x.Id)
            .IsRequired();

        entity.HasKey(e => e.Id);

        entity.HasOne(x => x.Model)
            .WithMany()
            .HasForeignKey(x => x.ModelId)
            .IsRequired();

        entity.ToTable("Simulators");
    }
}
