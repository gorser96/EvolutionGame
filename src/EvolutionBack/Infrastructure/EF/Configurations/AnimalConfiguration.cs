using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EF.Configurations;

internal class AnimalConfiguration : IEntityTypeConfiguration<Animal>
{
    public void Configure(EntityTypeBuilder<Animal> builder)
    {
        builder.ToTable("Animals");
        builder.HasKey(x => x.Uid);
        builder.Property(x => x.Uid).ValueGeneratedNever();
        builder.HasOne(x => x.InGameUser).WithMany(x => x.Animals).HasForeignKey(x => new { x.InGameUserUserUid, x.InGameUserRoomUid })
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.InGameCard).WithOne(x => x.Animal).HasForeignKey<Animal>(x => new { x.InGameCardUid, x.InGameUserRoomUid })
            .OnDelete(DeleteBehavior.NoAction);
    }
}
