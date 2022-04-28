using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EF.Configurations;

internal class GameHistoryUserConfiguration : IEntityTypeConfiguration<GameHistoryUser>
{
    public void Configure(EntityTypeBuilder<GameHistoryUser> builder)
    {
        builder.ToTable("GameHistoryUsers");
        builder.HasKey(x => x.Uid);
        builder.Property(x => x.Uid).ValueGeneratedNever();
        builder.HasOne(x => x.User).WithOne(x => x.GameHistoryUser).HasForeignKey<GameHistoryUser>(x => x.UserUid);
        builder.HasOne(x => x.GameHistory).WithMany(x => x.Users).HasForeignKey(x => x.GameHistoryUid);
    }
}
