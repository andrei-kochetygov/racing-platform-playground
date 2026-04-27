using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.API.Models;

namespace Platform.API.Persistence.Configurations;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> entity)
    {
        entity.Property(x => x.Id)
            .IsRequired();

        entity.HasKey(e => e.Id);

        entity.Property(x => x.UserId)
            .IsRequired();

        entity.HasIndex(x => x.UserId)
            .IsUnique();

        entity.HasOne(x => x.User)
            .WithOne(x => x.Profile)
            .HasForeignKey<UserProfile>(x => x.UserId)
            .IsRequired();

        entity.Property(x => x.FirstName)
            .IsRequired();

        entity.Property(x => x.LastName)
            .IsRequired();

        entity.HasOne(x => x.Avatar)
            .WithOne()
            .HasForeignKey<UserProfile>(x => x.AvatarId);

        entity.ToTable("UserProfiles");
    }
}
