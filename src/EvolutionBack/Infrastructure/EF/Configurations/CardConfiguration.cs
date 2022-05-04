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
        builder.HasOne(x => x.FirstProperty).WithOne().HasForeignKey<Card>(x => x.FirstPropertyUid);
        builder.HasOne(x => x.SecondProperty).WithOne().HasForeignKey<Card>(x => x.SecondPropertyUid);

        builder.HasIndex(x => x.FirstPropertyUid).IsUnique(false);
        builder.HasIndex(x => x.SecondPropertyUid).IsUnique(false);
        builder.HasIndex(x => x.AdditionUid).IsUnique(false);
    }
}
