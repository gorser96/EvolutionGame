using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EF.Configurations;

internal class InAnimalPropertyConfiguration : IEntityTypeConfiguration<InAnimalProperty>
{
    public void Configure(EntityTypeBuilder<InAnimalProperty> builder)
    {
        builder.ToTable("InAnimalProperties");
        builder.HasKey(x => new { x.PropertyUid, x.AnimalUid });
        builder.HasOne(x => x.Property).WithMany(x => x.Animals).HasForeignKey(x => x.PropertyUid);
        builder.HasOne(x => x.Animal).WithMany(x => x.Properties).HasForeignKey(x => x.AnimalUid);

        builder.HasIndex(x => x.PropertyUid).IsUnique(false);
        builder.HasIndex(x => x.AnimalUid).IsUnique(false);
    }
}
