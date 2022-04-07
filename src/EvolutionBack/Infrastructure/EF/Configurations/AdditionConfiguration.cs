using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EF.Configurations;

internal class AdditionConfiguration : IEntityTypeConfiguration<Addition>
{
    public void Configure(EntityTypeBuilder<Addition> builder)
    {
        builder.ToTable("Additions");
        builder.HasKey(x => x.Uid);
        builder.Property(x => x.Uid).ValueGeneratedNever();
        builder.Property(x => x.Name).HasMaxLength(300);
        builder.HasIndex(x => x.Name).IsUnique();
    }
}
