using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EF.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        var converter = new ValueConverter<string, string>(
            x => PasswordComputing.GetHash(x),
            x => x);

        builder.ToTable("Users");
        builder.HasKey(x => x.Uid);
        builder.Property(x => x.Uid).ValueGeneratedNever();
        builder.Property(x => x.Password).HasConversion(converter);
        builder.HasOne(x => x.InGameUser).WithOne(x => x.User);
        builder.Property(x => x.Login).HasMaxLength(30);
        builder.HasIndex(x => x.Login).IsUnique();
    }
}
