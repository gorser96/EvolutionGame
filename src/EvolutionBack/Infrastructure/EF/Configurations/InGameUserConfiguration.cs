using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EF.Configurations;

internal class InGameUserConfiguration : IEntityTypeConfiguration<InGameUser>
{
    public void Configure(EntityTypeBuilder<InGameUser> builder)
    {
        builder.ToTable("InGameUsers");
        builder.HasKey(x => new { x.UserId, x.RoomUid });
        builder.HasOne(x => x.User).WithOne(x => x.InGameUser).HasForeignKey<InGameUser>(x => x.UserId);
        builder.HasOne(x => x.Room).WithMany(x => x.InGameUsers).HasForeignKey(x => x.RoomUid).OnDelete(DeleteBehavior.Cascade);
    }
}
