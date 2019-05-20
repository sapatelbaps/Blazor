﻿// <auto-generated />
using System;
using Beam.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Beam.Data.Migrations
{
    [DbContext(typeof(BeamContext))]
    [Migration("20180612032510_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Beam.Data.Frequency", b =>
                {
                    b.Property<int>("FrequencyId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("FrequencyId");

                    b.ToTable("Frequencies");
                });

            modelBuilder.Entity("Beam.Data.Prism", b =>
                {
                    b.Property<int>("PrismId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RayId");

                    b.Property<int>("UserId");

                    b.HasKey("PrismId");

                    b.HasIndex("RayId");

                    b.HasIndex("UserId");

                    b.ToTable("Prisms");
                });

            modelBuilder.Entity("Beam.Data.Ray", b =>
                {
                    b.Property<int>("RayId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FrequencyId");

                    b.Property<string>("Text");

                    b.Property<int?>("UserId");

                    b.HasKey("RayId");

                    b.HasIndex("FrequencyId");

                    b.HasIndex("UserId");

                    b.ToTable("Rays");
                });

            modelBuilder.Entity("Beam.Data.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Username");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Beam.Data.Prism", b =>
                {
                    b.HasOne("Beam.Data.Ray", "Ray")
                        .WithMany("Prisms")
                        .HasForeignKey("RayId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Beam.Data.User", "User")
                        .WithMany("Prisms")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Beam.Data.Ray", b =>
                {
                    b.HasOne("Beam.Data.Frequency", "Frequency")
                        .WithMany("Rays")
                        .HasForeignKey("FrequencyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Beam.Data.User", "User")
                        .WithMany("Rays")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
