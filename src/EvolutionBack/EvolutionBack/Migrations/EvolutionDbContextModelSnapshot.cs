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

            modelBuilder.Entity("AdditionRoom", b =>
                {
                    b.Property<Guid>("AdditionsUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoomsUid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AdditionsUid", "RoomsUid");

                    b.HasIndex("RoomsUid");

                    b.ToTable("AdditionRoom");
                });

            modelBuilder.Entity("Domain.Models.Addition", b =>
                {
                    b.Property<Guid>("Uid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Icon")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("IconName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBase")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Uid");

                    b.HasIndex("Name")
                        .IsUnique();

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

                    b.Property<Guid>("InGameCardUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("InGameUserRoomUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("InGameUserUserUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Uid");

                    b.HasIndex("InGameCardUid", "InGameUserRoomUid")
                        .IsUnique();

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

                    b.HasIndex("FirstPropertyUid");

                    b.HasIndex("SecondPropertyUid");

                    b.ToTable("Cards", (string)null);
                });

            modelBuilder.Entity("Domain.Models.GameHistory", b =>
                {
                    b.Property<Guid>("Uid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FinishedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Uid");

                    b.ToTable("GameHistory", (string)null);
                });

            modelBuilder.Entity("Domain.Models.GameHistoryUser", b =>
                {
                    b.Property<Guid>("Uid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameHistoryUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<Guid>("UserUid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Uid");

                    b.HasIndex("GameHistoryUid");

                    b.HasIndex("UserUid");

                    b.ToTable("GameHistoryUsers", (string)null);
                });

            modelBuilder.Entity("Domain.Models.InAnimalProperty", b =>
                {
                    b.Property<Guid>("PropertyUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnimalUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsHasFatTissue")
                        .HasColumnType("bit");

                    b.HasKey("PropertyUid", "AnimalUid");

                    b.HasIndex("AnimalUid");

                    b.HasIndex("PropertyUid");

                    b.ToTable("InAnimalProperties", (string)null);
                });

            modelBuilder.Entity("Domain.Models.InGameCard", b =>
                {
                    b.Property<Guid>("RoomUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CardUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AnimalUid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.HasKey("RoomUid", "CardUid");

                    b.HasIndex("CardUid");

                    b.HasIndex("RoomUid");

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

                    b.HasIndex("UserUid");

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

                    b.Property<int?>("FeedIncreasing")
                        .HasColumnType("int");

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

                    b.Property<bool>("IsPrivate")
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

            modelBuilder.Entity("AdditionRoom", b =>
                {
                    b.HasOne("Domain.Models.Addition", null)
                        .WithMany()
                        .HasForeignKey("AdditionsUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Room", null)
                        .WithMany()
                        .HasForeignKey("RoomsUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.Animal", b =>
                {
                    b.HasOne("Domain.Models.InGameCard", "InGameCard")
                        .WithOne("Animal")
                        .HasForeignKey("Domain.Models.Animal", "InGameCardUid", "InGameUserRoomUid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Models.InGameUser", "InGameUser")
                        .WithMany("Animals")
                        .HasForeignKey("InGameUserUserUid", "InGameUserRoomUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InGameCard");

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
                        .WithMany()
                        .HasForeignKey("FirstPropertyUid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Models.Property", "SecondProperty")
                        .WithMany()
                        .HasForeignKey("SecondPropertyUid")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Addition");

                    b.Navigation("FirstProperty");

                    b.Navigation("SecondProperty");
                });

            modelBuilder.Entity("Domain.Models.GameHistoryUser", b =>
                {
                    b.HasOne("Domain.Models.GameHistory", "GameHistory")
                        .WithMany("Users")
                        .HasForeignKey("GameHistoryUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.User", "User")
                        .WithOne("GameHistoryUser")
                        .HasForeignKey("Domain.Models.GameHistoryUser", "UserUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameHistory");

                    b.Navigation("User");
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

            modelBuilder.Entity("Domain.Models.GameHistory", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Domain.Models.InGameCard", b =>
                {
                    b.Navigation("Animal");
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
                    b.Navigation("Cards");

                    b.Navigation("InGameUsers");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Navigation("GameHistoryUser");

                    b.Navigation("InGameUser");
                });
#pragma warning restore 612, 618
        }
    }
}
