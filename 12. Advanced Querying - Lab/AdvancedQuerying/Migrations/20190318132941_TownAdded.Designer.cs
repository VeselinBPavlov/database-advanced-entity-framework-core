﻿// <auto-generated />
using AdvancedQuerying;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AdvancedQuerying.Migrations
{
    [DbContext(typeof(AdvancedDbContext))]
    [Migration("20190318132941_TownAdded")]
    partial class TownAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AdvancedQuerying.Town", b =>
                {
                    b.Property<int>("TownId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("TownId");

                    b.ToTable("Towns");

                    b.HasData(
                        new
                        {
                            TownId = 1,
                            Name = "Sofia"
                        });
                });

            modelBuilder.Entity("AdvancedQuerying.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password");

                    b.Property<int>("TownId");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("TownId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Password = "secret",
                            TownId = 1,
                            Username = "Stamo"
                        });
                });

            modelBuilder.Entity("AdvancedQuerying.User", b =>
                {
                    b.HasOne("AdvancedQuerying.Town", "Town")
                        .WithMany("Users")
                        .HasForeignKey("TownId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}