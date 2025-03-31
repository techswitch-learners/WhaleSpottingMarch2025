﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WhaleSpottingBackend.Database;

#nullable disable

namespace WhaleSpottingBackend.Migrations
{
    [DbContext(typeof(WhaleSpottingDbContext))]
    [Migration("20250331142642_SeedDataMigration")]
    partial class SeedDataMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("WhaleSpottingBackend.Models.DatabaseModels.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision")
                        .HasColumnName("Latitude");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision")
                        .HasColumnName("Longitude");

                    b.HasKey("Id");

                    b.ToTable("Location");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Latitude = 41.902799999999999,
                            Longitude = -60.0
                        },
                        new
                        {
                            Id = 2,
                            Latitude = 69.169370999999998,
                            Longitude = -174.81342699999999
                        });
                });

            modelBuilder.Entity("WhaleSpottingBackend.Models.DatabaseModels.Sighting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Description");

                    b.Property<string>("ImageSource")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ImageSource");

                    b.Property<int>("LocationId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("Quantity");

                    b.Property<DateTime>("ReportDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ReportDate");

                    b.Property<DateTime>("SightingDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("SightingDate");

                    b.Property<int>("SpeciesId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("SpeciesId");

                    b.ToTable("Sighting");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Details of Sighting 1",
                            ImageSource = "https://images.google.com/",
                            LocationId = 2,
                            Quantity = 1,
                            ReportDate = new DateTime(2024, 3, 2, 13, 21, 33, 0, DateTimeKind.Utc),
                            SightingDate = new DateTime(2024, 3, 1, 13, 21, 33, 0, DateTimeKind.Utc),
                            SpeciesId = 2
                        },
                        new
                        {
                            Id = 2,
                            Description = "Details of Sighting 2",
                            ImageSource = "https://images.google.com/",
                            LocationId = 1,
                            Quantity = 1,
                            ReportDate = new DateTime(2024, 3, 3, 13, 21, 33, 0, DateTimeKind.Utc),
                            SightingDate = new DateTime(2024, 3, 2, 13, 21, 33, 0, DateTimeKind.Utc),
                            SpeciesId = 1
                        },
                        new
                        {
                            Id = 3,
                            Description = "Details of Sighting 3",
                            ImageSource = "https://images.google.com/",
                            LocationId = 2,
                            Quantity = 1,
                            ReportDate = new DateTime(2024, 3, 4, 13, 21, 33, 0, DateTimeKind.Utc),
                            SightingDate = new DateTime(2024, 3, 3, 13, 21, 33, 0, DateTimeKind.Utc),
                            SpeciesId = 2
                        },
                        new
                        {
                            Id = 4,
                            Description = "Details of Sighting 4",
                            ImageSource = "https://images.google.com/",
                            LocationId = 1,
                            Quantity = 1,
                            ReportDate = new DateTime(2024, 3, 5, 13, 21, 33, 0, DateTimeKind.Utc),
                            SightingDate = new DateTime(2024, 3, 4, 13, 21, 33, 0, DateTimeKind.Utc),
                            SpeciesId = 1
                        },
                        new
                        {
                            Id = 5,
                            Description = "Details of Sighting 5",
                            ImageSource = "https://images.google.com/",
                            LocationId = 2,
                            Quantity = 1,
                            ReportDate = new DateTime(2024, 3, 6, 13, 21, 33, 0, DateTimeKind.Utc),
                            SightingDate = new DateTime(2024, 3, 5, 13, 21, 33, 0, DateTimeKind.Utc),
                            SpeciesId = 2
                        },
                        new
                        {
                            Id = 6,
                            Description = "Details of Sighting 6",
                            ImageSource = "https://images.google.com/",
                            LocationId = 1,
                            Quantity = 1,
                            ReportDate = new DateTime(2024, 3, 7, 13, 21, 33, 0, DateTimeKind.Utc),
                            SightingDate = new DateTime(2024, 3, 6, 13, 21, 33, 0, DateTimeKind.Utc),
                            SpeciesId = 1
                        },
                        new
                        {
                            Id = 7,
                            Description = "Details of Sighting 7",
                            ImageSource = "https://images.google.com/",
                            LocationId = 2,
                            Quantity = 1,
                            ReportDate = new DateTime(2024, 3, 8, 13, 21, 33, 0, DateTimeKind.Utc),
                            SightingDate = new DateTime(2024, 3, 7, 13, 21, 33, 0, DateTimeKind.Utc),
                            SpeciesId = 2
                        },
                        new
                        {
                            Id = 8,
                            Description = "Details of Sighting 8",
                            ImageSource = "https://images.google.com/",
                            LocationId = 1,
                            Quantity = 1,
                            ReportDate = new DateTime(2024, 3, 9, 13, 21, 33, 0, DateTimeKind.Utc),
                            SightingDate = new DateTime(2024, 3, 8, 13, 21, 33, 0, DateTimeKind.Utc),
                            SpeciesId = 1
                        },
                        new
                        {
                            Id = 9,
                            Description = "Details of Sighting 9",
                            ImageSource = "https://images.google.com/",
                            LocationId = 2,
                            Quantity = 1,
                            ReportDate = new DateTime(2024, 3, 10, 13, 21, 33, 0, DateTimeKind.Utc),
                            SightingDate = new DateTime(2024, 3, 9, 13, 21, 33, 0, DateTimeKind.Utc),
                            SpeciesId = 2
                        },
                        new
                        {
                            Id = 10,
                            Description = "Details of Sighting 10",
                            ImageSource = "https://images.google.com/",
                            LocationId = 1,
                            Quantity = 1,
                            ReportDate = new DateTime(2024, 3, 11, 13, 21, 33, 0, DateTimeKind.Utc),
                            SightingDate = new DateTime(2024, 3, 10, 13, 21, 33, 0, DateTimeKind.Utc),
                            SpeciesId = 1
                        },
                        new
                        {
                            Id = 11,
                            Description = "Details of Sighting 11",
                            ImageSource = "https://images.google.com/",
                            LocationId = 2,
                            Quantity = 1,
                            ReportDate = new DateTime(2024, 3, 12, 13, 21, 33, 0, DateTimeKind.Utc),
                            SightingDate = new DateTime(2024, 3, 11, 13, 21, 33, 0, DateTimeKind.Utc),
                            SpeciesId = 2
                        },
                        new
                        {
                            Id = 12,
                            Description = "Details of Sighting 12",
                            ImageSource = "https://images.google.com/",
                            LocationId = 1,
                            Quantity = 1,
                            ReportDate = new DateTime(2024, 3, 13, 13, 21, 33, 0, DateTimeKind.Utc),
                            SightingDate = new DateTime(2024, 3, 12, 13, 21, 33, 0, DateTimeKind.Utc),
                            SpeciesId = 1
                        },
                        new
                        {
                            Id = 13,
                            Description = "Details of Sighting 13",
                            ImageSource = "https://images.google.com/",
                            LocationId = 2,
                            Quantity = 1,
                            ReportDate = new DateTime(2024, 3, 14, 13, 21, 33, 0, DateTimeKind.Utc),
                            SightingDate = new DateTime(2024, 3, 13, 13, 21, 33, 0, DateTimeKind.Utc),
                            SpeciesId = 2
                        },
                        new
                        {
                            Id = 14,
                            Description = "Details of Sighting 14",
                            ImageSource = "https://images.google.com/",
                            LocationId = 1,
                            Quantity = 1,
                            ReportDate = new DateTime(2024, 3, 15, 13, 21, 33, 0, DateTimeKind.Utc),
                            SightingDate = new DateTime(2024, 3, 14, 13, 21, 33, 0, DateTimeKind.Utc),
                            SpeciesId = 1
                        },
                        new
                        {
                            Id = 15,
                            Description = "Details of Sighting 15",
                            ImageSource = "https://images.google.com/",
                            LocationId = 2,
                            Quantity = 1,
                            ReportDate = new DateTime(2024, 3, 16, 13, 21, 33, 0, DateTimeKind.Utc),
                            SightingDate = new DateTime(2024, 3, 15, 13, 21, 33, 0, DateTimeKind.Utc),
                            SpeciesId = 2
                        },
                        new
                        {
                            Id = 16,
                            Description = "Details of Sighting 16",
                            ImageSource = "https://images.google.com/",
                            LocationId = 1,
                            Quantity = 1,
                            ReportDate = new DateTime(2024, 3, 17, 13, 21, 33, 0, DateTimeKind.Utc),
                            SightingDate = new DateTime(2024, 3, 16, 13, 21, 33, 0, DateTimeKind.Utc),
                            SpeciesId = 1
                        },
                        new
                        {
                            Id = 17,
                            Description = "Details of Sighting 17",
                            ImageSource = "https://images.google.com/",
                            LocationId = 2,
                            Quantity = 1,
                            ReportDate = new DateTime(2024, 3, 18, 13, 21, 33, 0, DateTimeKind.Utc),
                            SightingDate = new DateTime(2024, 3, 17, 13, 21, 33, 0, DateTimeKind.Utc),
                            SpeciesId = 2
                        },
                        new
                        {
                            Id = 18,
                            Description = "Details of Sighting 18",
                            ImageSource = "https://images.google.com/",
                            LocationId = 1,
                            Quantity = 1,
                            ReportDate = new DateTime(2024, 3, 19, 13, 21, 33, 0, DateTimeKind.Utc),
                            SightingDate = new DateTime(2024, 3, 18, 13, 21, 33, 0, DateTimeKind.Utc),
                            SpeciesId = 1
                        },
                        new
                        {
                            Id = 19,
                            Description = "Details of Sighting 19",
                            ImageSource = "https://images.google.com/",
                            LocationId = 2,
                            Quantity = 1,
                            ReportDate = new DateTime(2024, 3, 20, 13, 21, 33, 0, DateTimeKind.Utc),
                            SightingDate = new DateTime(2024, 3, 19, 13, 21, 33, 0, DateTimeKind.Utc),
                            SpeciesId = 2
                        },
                        new
                        {
                            Id = 20,
                            Description = "Details of Sighting 20",
                            ImageSource = "https://images.google.com/",
                            LocationId = 1,
                            Quantity = 1,
                            ReportDate = new DateTime(2024, 3, 21, 13, 21, 33, 0, DateTimeKind.Utc),
                            SightingDate = new DateTime(2024, 3, 20, 13, 21, 33, 0, DateTimeKind.Utc),
                            SpeciesId = 1
                        });
                });

            modelBuilder.Entity("WhaleSpottingBackend.Models.DatabaseModels.Species", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("SpeciesName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("SpeciesName");

                    b.HasKey("Id");

                    b.ToTable("Species");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            SpeciesName = "Blue Whale"
                        },
                        new
                        {
                            Id = 2,
                            SpeciesName = "Beluga Whale"
                        });
                });

            modelBuilder.Entity("WhaleSpottingBackend.Models.DatabaseModels.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WhaleSpottingBackend.Models.DatabaseModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WhaleSpottingBackend.Models.DatabaseModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WhaleSpottingBackend.Models.DatabaseModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WhaleSpottingBackend.Models.DatabaseModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WhaleSpottingBackend.Models.DatabaseModels.Sighting", b =>
                {
                    b.HasOne("WhaleSpottingBackend.Models.DatabaseModels.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WhaleSpottingBackend.Models.DatabaseModels.Species", "Species")
                        .WithMany()
                        .HasForeignKey("SpeciesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Species");
                });
#pragma warning restore 612, 618
        }
    }
}
