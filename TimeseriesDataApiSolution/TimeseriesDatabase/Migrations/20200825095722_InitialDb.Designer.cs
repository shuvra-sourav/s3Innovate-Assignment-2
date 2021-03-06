﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimeseriesDatabase;

namespace TimeseriesDatabase.Migrations
{
    [DbContext(typeof(TimeSeriesDbContext))]
    [Migration("20200825095722_InitialDb")]
    partial class InitialDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TimeseriesDatabase.Model.Building", b =>
                {
                    b.Property<short>("Id")
                        .HasColumnType("smallint");

                    b.Property<string>("Location")
                        .HasColumnType("VARCHAR(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.HasKey("Id");

                    b.ToTable("Building");
                });

            modelBuilder.Entity("TimeseriesDatabase.Model.BuildingDataField", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.HasKey("Id");

                    b.ToTable("BuildingDataField");
                });

            modelBuilder.Entity("TimeseriesDatabase.Model.BuildingObject", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.HasKey("Id");

                    b.ToTable("BuildingObject");
                });

            modelBuilder.Entity("TimeseriesDatabase.Model.Reading", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("BuildingEntityId")
                        .HasColumnType("smallint");

                    b.Property<byte>("DataFieldEntityId")
                        .HasColumnType("tinyint");

                    b.Property<byte>("ObjectEntityId")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BuildingEntityId");

                    b.HasIndex("DataFieldEntityId");

                    b.HasIndex("ObjectEntityId");

                    b.HasIndex("Timestamp");

                    b.ToTable("Reading");
                });
#pragma warning restore 612, 618
        }
    }
}
