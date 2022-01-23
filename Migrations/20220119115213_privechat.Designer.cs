﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace webapplication.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20220119115213_privechat")]
    partial class privechat
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("PriveChat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Afzender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DateTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Ontvanger")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("PriveChat");
                });
#pragma warning restore 612, 618
        }
    }
}