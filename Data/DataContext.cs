using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YemekTarifiApp.Models;

namespace YemekTarifiApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Yemek> Yemekler{ get; set; }
        public DbSet<Malzeme> Malzemeler{ get; set;}
        public DbSet<YemekMalzeme> YemekMalzemeler{get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<YemekMalzeme>()
            .HasKey(ym => new {ym.YemekId, ym.MalzemeId});

            modelBuilder.Entity<YemekMalzeme>()
            .HasOne(ym => ym.Yemek)
            .WithMany(y => y.YemekMalzemeler)
            .HasForeignKey(ym => ym.YemekId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<YemekMalzeme>()
            .HasOne(ym => ym.Malzeme)
            .WithMany(m => m.YemekMalzemeler)
            .HasForeignKey(ym => ym.MalzemeId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Yemek>()
            .Property(y => y.Ad)
            .IsRequired();

            modelBuilder.Entity<Malzeme>()
            .Property(m => m.Ad)
            .IsRequired();

            modelBuilder.Entity<Malzeme>()
            .Property(m => m.Birim)
            .HasMaxLength(50);
        }

        }
    }
