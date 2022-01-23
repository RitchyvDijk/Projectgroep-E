﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace webapplication.Migrations.afspraakDb
{
    [DbContext(typeof(afspraakDbContext))]
    partial class afspraakDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("afspraakModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BSN")
                        .HasColumnType("TEXT");

                    b.Property<string>("achternaam")
                        .HasColumnType("TEXT");

                    b.Property<string>("emailvanGebruiker")
                        .HasColumnType("TEXT");

                    b.Property<string>("emailvanOuder")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("geboorteDatum")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("gekozenDatum")
                        .HasColumnType("TEXT");

                    b.Property<string>("gekozenHulpverlener")
                        .HasColumnType("TEXT");

                    b.Property<string>("gekozenTijd")
                        .HasColumnType("TEXT");

                    b.Property<bool>("jongerDan16")
                        .HasColumnType("INTEGER");

                    b.Property<string>("naamOuder")
                        .HasColumnType("TEXT");

                    b.Property<string>("voornaam")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("afspraakModel");
                });
#pragma warning restore 612, 618
        }
    }
}