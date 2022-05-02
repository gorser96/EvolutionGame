using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EF.Configurations;

internal class InGameCardConfiguration : IEntityTypeConfiguration<InGameCard>
{
    public void Configure(EntityTypeBuilder<InGameCard> builder)
    {
        builder.ToTable("InGameCards");
        builder.HasKey(x => new { x.RoomUid, x.CardUid });
        builder.HasOne(x => x.Room).WithMany(x => x.Cards).HasForeignKey(x => x.RoomUid);

        builder.HasIndex(x => x.RoomUid).IsUnique(false);
    }
}
