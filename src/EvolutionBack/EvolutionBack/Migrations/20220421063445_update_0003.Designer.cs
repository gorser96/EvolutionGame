﻿// <auto-generated />
using System;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EvolutionBack.Migrations
{
    [DbContext(typeof(EvolutionDbContext))]
    [Migration("20220421063445_update_0003")]
    partial class update_0003
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Models.Addition", b =>
                {
                    b.Property<Guid>("Uid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsBase")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<Guid?>("RoomUid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Uid");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("RoomUid");

                    b.ToTable("Additions", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Animal", b =>
                {
                    b.Property<Guid>("Uid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("FoodCurrent")
                        .HasColumnType("int");

                    b.Property<int>("FoodMax")
                        .HasColumnType("int");

                    b.Property<Guid>("InGameUserRoomUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("InGameUserUserUid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Uid");

                    b.HasIndex("InGameUserUserUid", "InGameUserRoomUid");

                    b.ToTable("Animals", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Card", b =>
                {
                    b.Property<Guid>("Uid")
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

            modelBuilder.Entity("Domain.Models.InAnimalProperty", b =>
                {
                    b.Property<Guid>("PropertyUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnimalUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("PropertyUid", "AnimalUid");

                    b.HasIndex("AnimalUid");

                    b.ToTable("InAnimalProperties", (string)null);
                });

            modelBuilder.Entity("Domain.Models.InGameCard", b =>
                {
                    b.Property<Guid>("RoomUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CardUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.HasKey("RoomUid", "CardUid");

                    b.HasIndex("CardUid");

                    b.ToTable("InGameCards", (string)null);
                });

            modelBuilder.Entity("Domain.Models.InGameUser", b =>
                {
                    b.Property<Guid>("UserUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoomUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsCurrent")
                        .HasColumnType("bit");

                    b.Property<bool>("IsHost")
                        .HasColumnType("bit");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartStepTime")
                        .HasColumnType("datetime2");

                    b.HasKey("UserUid", "RoomUid");

                    b.HasIndex("RoomUid");

                    b.HasIndex("UserUid")
                        .IsUnique();

                    b.ToTable("InGameUsers", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Property", b =>
                {
                    b.Property<Guid>("Uid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AssemblyName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<bool>("IsOnEnemy")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPair")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Uid");

                    b.HasIndex("AssemblyName")
                        .IsUnique();

                    b.ToTable("Properties", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Room", b =>
                {
                    b.Property<Guid>("Uid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FinishedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPaused")
                        .HasColumnType("bit");

                    b.Property<bool>("IsStarted")
                        .HasColumnType("bit");

                    b.Property<TimeSpan?>("MaxTimeLeft")
                        .HasColumnType("time");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PauseStartedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StartDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("StepNumber")
                        .HasColumnType("int");

                    b.HasKey("Uid");

                    b.ToTable("Rooms", (string)null);
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("UserName")
                        .IsUnique()
                        .HasFilter("[UserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Addition", b =>
                {
                    b.HasOne("Domain.Models.Room", null)
                        .WithMany("Additions")
                        .HasForeignKey("RoomUid");
                });

            modelBuilder.Entity("Domain.Models.Animal", b =>
                {
                    b.HasOne("Domain.Models.InGameUser", "InGameUser")
                        .WithMany("Animals")
                        .HasForeignKey("InGameUserUserUid", "InGameUserRoomUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

            modelBuilder.Entity("Domain.Models.InAnimalProperty", b =>
                {
                    b.HasOne("Domain.Models.Animal", "Animal")
                        .WithMany("Properties")
                        .HasForeignKey("AnimalUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Property", "Property")
                        .WithMany("Animals")
                        .HasForeignKey("PropertyUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("Domain.Models.InGameCard", b =>
                {
                    b.HasOne("Domain.Models.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Room", "Room")
                        .WithMany("Cards")
                        .HasForeignKey("RoomUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Room");
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.Addition", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("Domain.Models.Animal", b =>
                {
                    b.Navigation("Properties");
                });

            modelBuilder.Entity("Domain.Models.InGameUser", b =>
                {
                    b.Navigation("Animals");
                });

            modelBuilder.Entity("Domain.Models.Property", b =>
                {
                    b.Navigation("Animals");
                });

            modelBuilder.Entity("Domain.Models.Room", b =>
                {
                    b.Navigation("Additions");

                    b.Navigation("Cards");

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