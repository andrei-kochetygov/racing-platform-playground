using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.API.Models;

namespace Platform.API.Persistence.Configurations;

public class SimulatorModelConfiguration : IEntityTypeConfiguration<SimulatorModel>
{
    public void Configure(EntityTypeBuilder<SimulatorModel> entity)
    {
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Name)
            .IsRequired();

        entity.HasIndex(x => x.Name)
            .IsUnique();

        entity.ToTable("SimulatorModels");
    }
}
