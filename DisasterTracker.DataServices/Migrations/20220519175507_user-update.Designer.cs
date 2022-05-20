﻿// <auto-generated />
using System;
using DisasterTracker.DataServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DisasterTracker.DataServices.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220519175507_user-update")]
    partial class userupdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DisasterTracker.Data.Country.Country", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ISO3")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LongName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("DisasterTracker.Data.Country.CountryDisaster", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AffectedPopulation")
                        .HasColumnType("integer");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DisasterId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("DisasterId");

                    b.ToTable("CountryDisaster");
                });

            modelBuilder.Entity("DisasterTracker.Data.Disaster.Disaster", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApiId")
                        .HasColumnType("uuid");

                    b.Property<bool>("AutoExpire")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Severity")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("UpdateId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ApiId");

                    b.HasIndex("ApiId", "LastUpdateDate");

                    b.ToTable("Disaster");
                });

            modelBuilder.Entity("DisasterTracker.Data.Disaster.DisasterImage", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DisasterId")
                        .HasColumnType("uuid");

                    b.Property<string>("InfrastructureMapImageAddress")
                        .HasColumnType("text");

                    b.Property<string>("MapImageAddress")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("OverviewMapImageAddress")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DisasterId")
                        .IsUnique();

                    b.ToTable("DisasterImage");
                });

            modelBuilder.Entity("DisasterTracker.Data.Disaster.DisasterStatistics", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long?>("CapitalExposed")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DisasterId")
                        .HasColumnType("uuid");

                    b.Property<int?>("Hospitals")
                        .HasColumnType("integer");

                    b.Property<int?>("Households")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("Population0_14Affected")
                        .HasColumnType("integer");

                    b.Property<int?>("Population15_64Affected")
                        .HasColumnType("integer");

                    b.Property<int?>("PopulationAbove65Affected")
                        .HasColumnType("integer");

                    b.Property<int?>("Schools")
                        .HasColumnType("integer");

                    b.Property<string>("Severity")
                        .HasColumnType("text");

                    b.Property<int?>("TotalPopulation")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DisasterId");

                    b.ToTable("DisasterStatistics");
                });

            modelBuilder.Entity("DisasterTracker.Data.User.User", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("ReceiveEmails")
                        .HasColumnType("boolean");

                    b.Property<bool>("ReceivePushNotifications")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("DisasterTracker.Data.User.UserLocation", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Distance")
                        .HasColumnType("integer");

                    b.Property<string>("Label")
                        .HasColumnType("text");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserLocation");
                });

            modelBuilder.Entity("DisasterTracker.Data.Country.CountryDisaster", b =>
                {
                    b.HasOne("DisasterTracker.Data.Country.Country", "Country")
                        .WithMany("Disasters")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DisasterTracker.Data.Disaster.Disaster", "Disaster")
                        .WithMany("Countries")
                        .HasForeignKey("DisasterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Disaster");
                });

            modelBuilder.Entity("DisasterTracker.Data.Disaster.DisasterImage", b =>
                {
                    b.HasOne("DisasterTracker.Data.Disaster.Disaster", "Disaster")
                        .WithOne("DisasterImage")
                        .HasForeignKey("DisasterTracker.Data.Disaster.DisasterImage", "DisasterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Disaster");
                });

            modelBuilder.Entity("DisasterTracker.Data.Disaster.DisasterStatistics", b =>
                {
                    b.HasOne("DisasterTracker.Data.Disaster.Disaster", "Disaster")
                        .WithMany("DisasterStatistics")
                        .HasForeignKey("DisasterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Disaster");
                });

            modelBuilder.Entity("DisasterTracker.Data.User.UserLocation", b =>
                {
                    b.HasOne("DisasterTracker.Data.User.User", "User")
                        .WithMany("Locations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DisasterTracker.Data.Country.Country", b =>
                {
                    b.Navigation("Disasters");
                });

            modelBuilder.Entity("DisasterTracker.Data.Disaster.Disaster", b =>
                {
                    b.Navigation("Countries");

                    b.Navigation("DisasterImage");

                    b.Navigation("DisasterStatistics");
                });

            modelBuilder.Entity("DisasterTracker.Data.User.User", b =>
                {
                    b.Navigation("Locations");
                });
#pragma warning restore 612, 618
        }
    }
}