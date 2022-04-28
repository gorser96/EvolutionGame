using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EF.Configurations;

internal class GameHistoryConfiguration : IEntityTypeConfiguration<GameHistory>
{
    public void Configure(EntityTypeBuilder<GameHistory> builder)
    {
        builder.ToTable("GameHistory");
        builder.HasKey(x => x.Uid);
        builder.Property(x => x.Uid).ValueGeneratedNever();
    }
}
