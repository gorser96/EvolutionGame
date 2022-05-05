using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EF.Configurations;

internal class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.ToTable("Cards");
        builder.HasKey(x => x.Uid);
        builder.Property(x => x.Uid).ValueGeneratedNever();
        builder.HasOne(x => x.Addition).WithMany(x => x.Cards).HasForeignKey(x => x.AdditionUid);
        builder.HasOne(x => x.FirstProperty).WithMany().HasForeignKey(x => x.FirstPropertyUid)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.SecondProperty).WithMany().HasForeignKey(x => x.SecondPropertyUid)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(x => x.FirstPropertyUid).IsUnique(false);
        builder.HasIndex(x => x.SecondPropertyUid).IsUnique(false);
        builder.HasIndex(x => x.AdditionUid).IsUnique(false);
    }
}
