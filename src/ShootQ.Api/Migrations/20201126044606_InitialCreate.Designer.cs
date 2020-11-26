﻿// <auto-generated />
using System;
using BuildingBlocks.EventStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ShootQ.Api.Migrations
{
    [DbContext(typeof(EventStoreDbContext))]
    [Migration("20201126044606_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("BuildingBlocks.EventStore.SnapShot", b =>
                {
                    b.Property<Guid>("SnapShotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SnapShotId");

                    b.ToTable("SnapShots");
                });

            modelBuilder.Entity("BuildingBlocks.EventStore.StoredEvent", b =>
                {
                    b.Property<Guid>("StoredEventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Aggregate")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AggregateDotNetType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DotNetType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.Property<Guid>("StreamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("StoredEventId");

                    b.HasIndex("StreamId", "Aggregate");

                    b.ToTable("StoredEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
