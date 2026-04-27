using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.API.Models;

namespace Platform.API.Persistence.Configurations;

public class MediaFileConfiguration : IEntityTypeConfiguration<MediaFile>
{
    public void Configure(EntityTypeBuilder<MediaFile> entity)
    {
        entity.Property(x => x.Id)
            .IsRequired();

        entity.HasKey(e => e.Id);

        entity.Property(x => x.ContentType)
            .IsRequired();

        entity.Property(x => x.Size)
            .IsRequired();

        entity.Property(x => x.Storage)
            .IsRequired();

        entity.Property(x => x.StorageKey)
            .IsRequired();

        entity.ToTable("MediaFiles");
    }
}
