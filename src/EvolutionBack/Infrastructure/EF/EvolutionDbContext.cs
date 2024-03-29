﻿using Domain.Models;
using Infrastructure.EF.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF;

public class EvolutionDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
#pragma warning disable CS8618
    public EvolutionDbContext(DbContextOptions options) : base(options)
    {
    }
#pragma warning restore CS8618

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<InGameUser> InGameUsers { get; set; }

    public virtual DbSet<InGameCard> InGameCards { get; set; }

    public virtual DbSet<InAnimalProperty> InAnimalProperties { get; set; }

    public virtual DbSet<Addition> Additions { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<Animal> Animals { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<GameHistory> GameHistory { get; set; }

    public virtual DbSet<GameHistoryUser> GameHistoryUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoomConfiguration());
        modelBuilder.ApplyConfiguration(new CardConfiguration());
        modelBuilder.ApplyConfiguration(new AnimalConfiguration());
        modelBuilder.ApplyConfiguration(new AdditionConfiguration());
        modelBuilder.ApplyConfiguration(new PropertyConfiguration());
        modelBuilder.ApplyConfiguration(new InGameUserConfiguration());
        modelBuilder.ApplyConfiguration(new InGameCardConfiguration());
        modelBuilder.ApplyConfiguration(new InAnimalPropertyConfiguration());
        modelBuilder.ApplyConfiguration(new GameHistoryConfiguration());
        modelBuilder.ApplyConfiguration(new GameHistoryUserConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
