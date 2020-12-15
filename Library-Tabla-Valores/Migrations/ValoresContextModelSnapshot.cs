﻿// <auto-generated />
using Library_Tabla_Valores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Library_Tabla_Valores.Migrations
{
    [DbContext(typeof(ValoresContext))]
    partial class ValoresContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Library_Tabla_Valores.Valores", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Año2005")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Año2006")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Año2007")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Año2008")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Año2009")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Año2010")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Año2011")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Año2012")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Año2013")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Año2014")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Año2015")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Año2016")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Año2017")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Año2018")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Año2019")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Marca")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Modelo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Moneda")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Okm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Version")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Valores");
                });
#pragma warning restore 612, 618
        }
    }
}
