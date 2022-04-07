using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EF.Configurations;

internal class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.ToTable("Properties");
        builder.HasKey(x => x.Uid);
        builder.Property(x => x.Uid).ValueGeneratedNever();
        builder.Property(x => x.AssemblyName);
        builder.Property(x => x.AssemblyName).HasMaxLength(300);
        builder.HasIndex(x => x.AssemblyName).IsUnique();
    }
}
