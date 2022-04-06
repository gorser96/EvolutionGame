﻿// <auto-generated />
using System;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EvolutionBack.Migrations
{
    [DbContext(typeof(EvolutionDbContext))]
    partial class EvolutionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AnimalProperty", b =>
                {
                    b.Property<Guid>("AnimalsUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PropertiesUid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AnimalsUid", "PropertiesUid");

                    b.HasIndex("PropertiesUid");

                    b.ToTable("AnimalProperty");
                });

            modelBuilder.Entity("Domain.Models.Addition", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Uid");

                    b.ToTable("Additions", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Animal", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("FoodCurrent")
                        .HasColumnType("int");

                    b.Property<int>("FoodMax")
                        .HasColumnType("int");

                    b.Property<Guid?>("InGameUserRoomUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("InGameUserUserUid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Uid");

                    b.HasIndex("InGameUserUserUid", "InGameUserRoomUid");

                    b.ToTable("Animals", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Card", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AdditionUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FirstPropertyUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SecondPropertyUid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Uid");

                    b.HasIndex("AdditionUid");

                    b.HasIndex("FirstPropertyUid")
                        .IsUnique();

                    b.HasIndex("SecondPropertyUid")
                        .IsUnique()
                        .HasFilter("[SecondPropertyUid] IS NOT NULL");

                    b.ToTable("Cards", (string)null);
                });

            modelBuilder.Entity("Domain.Models.InGameUser", b =>
                {
                    b.Property<Guid>("UserUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoomUid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserUid", "RoomUid");

                    b.HasIndex("RoomUid");

                    b.HasIndex("UserUid")
                        .IsUnique();

                    b.ToTable("InGameUsers", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Property", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AssemblyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsOnEnemy")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPair")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Uid");

                    b.ToTable("Properties", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Room", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Uid");

                    b.ToTable("Rooms", (string)null);
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Uid");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("AnimalProperty", b =>
                {
                    b.HasOne("Domain.Models.Animal", null)
                        .WithMany()
                        .HasForeignKey("AnimalsUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Property", null)
                        .WithMany()
                        .HasForeignKey("PropertiesUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.Animal", b =>
                {
                    b.HasOne("Domain.Models.InGameUser", "InGameUser")
                        .WithMany("Animals")
                        .HasForeignKey("InGameUserUserUid", "InGameUserRoomUid");

                    b.Navigation("InGameUser");
                });

            modelBuilder.Entity("Domain.Models.Card", b =>
                {
                    b.HasOne("Domain.Models.Addition", "Addition")
                        .WithMany("Cards")
                        .HasForeignKey("AdditionUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Property", "FirstProperty")
                        .WithOne()
                        .HasForeignKey("Domain.Models.Card", "FirstPropertyUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Property", "SecondProperty")
                        .WithOne()
                        .HasForeignKey("Domain.Models.Card", "SecondPropertyUid");

                    b.Navigation("Addition");

                    b.Navigation("FirstProperty");

                    b.Navigation("SecondProperty");
                });

            modelBuilder.Entity("Domain.Models.InGameUser", b =>
                {
                    b.HasOne("Domain.Models.Room", "Room")
                        .WithMany("InGameUsers")
                        .HasForeignKey("RoomUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.User", "User")
                        .WithOne("InGameUser")
                        .HasForeignKey("Domain.Models.InGameUser", "UserUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.Addition", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("Domain.Models.InGameUser", b =>
                {
                    b.Navigation("Animals");
                });

            modelBuilder.Entity("Domain.Models.Room", b =>
                {
                    b.Navigation("InGameUsers");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Navigation("InGameUser");
                });
#pragma warning restore 612, 618
        }
    }
}
