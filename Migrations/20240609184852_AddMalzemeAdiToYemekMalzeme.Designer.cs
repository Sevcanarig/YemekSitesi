﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YemekTarifiApp.Data;

#nullable disable

namespace YemekTarifiApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240609184852_AddMalzemeAdiToYemekMalzeme")]
    partial class AddMalzemeAdiToYemekMalzeme
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.8");

            modelBuilder.Entity("YemekTarifiApp.Models.Malzeme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Ad")
                        .HasColumnType("TEXT");

                    b.Property<string>("Birim")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Malzemeler");
                });

            modelBuilder.Entity("YemekTarifiApp.Models.Yemek", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Ad")
                        .HasColumnType("TEXT");

                    b.Property<string>("ResimUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Yapilis")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Yemekler");
                });

            modelBuilder.Entity("YemekTarifiApp.Models.YemekMalzeme", b =>
                {
                    b.Property<int>("YemekId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MalzemeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MalzemeAdi")
                        .HasColumnType("TEXT");

                    b.Property<string>("Miktar")
                        .HasColumnType("TEXT");

                    b.HasKey("YemekId", "MalzemeId");

                    b.HasIndex("MalzemeId");

                    b.ToTable("YemekMalzemeler");
                });

            modelBuilder.Entity("YemekTarifiApp.Models.YemekMalzeme", b =>
                {
                    b.HasOne("YemekTarifiApp.Models.Malzeme", "Malzeme")
                        .WithMany("YemekMalzemeler")
                        .HasForeignKey("MalzemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YemekTarifiApp.Models.Yemek", "Yemek")
                        .WithMany("YemekMalzemeler")
                        .HasForeignKey("YemekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Malzeme");

                    b.Navigation("Yemek");
                });

            modelBuilder.Entity("YemekTarifiApp.Models.Malzeme", b =>
                {
                    b.Navigation("YemekMalzemeler");
                });

            modelBuilder.Entity("YemekTarifiApp.Models.Yemek", b =>
                {
                    b.Navigation("YemekMalzemeler");
                });
#pragma warning restore 612, 618
        }
    }
}
