using Domain.Models;
using Infrastructure.EF.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF;

public class EvolutionDbContext : DbContext
{
#pragma warning disable CS8618
    public EvolutionDbContext(DbContextOptions options) : base(options)
    {
    }
#pragma warning restore CS8618

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<InGameUser> InGameUsers { get; set; }

    public virtual DbSet<Addition> Additions { get; set; }

    public virtual DbSet<IProperty> Properties { get; set; }

    public virtual DbSet<Animal> Animals { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public static DbContextOptionsBuilder SetOptions(string connectionString, DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);

        optionsBuilder.UseLazyLoadingProxies();

        return optionsBuilder;
    }
}
