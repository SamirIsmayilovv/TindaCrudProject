using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Contexts
{
    public class AppDbcontext:IdentityDbContext<IdentityUser>
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options):base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; } 
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductTipe> ProductTipes { get; set;}
        public DbSet<Tipe> Tipes { get; set; }


        //Fluent 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);  

                entity.HasMany(e => e.ProductCategories)
                      .WithOne(pc => pc.Category)
                      .HasForeignKey(pc => pc.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });


            //Product Fluent Api
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Price)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(e => e.NumberInStock)
                      .IsRequired();

                entity.HasMany(e => e.ProductCategories)
                      .WithOne(pc => pc.Product)
                      .HasForeignKey(pc => pc.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.ProductTipes)
                      .WithOne(pt => pt.Product)
                      .HasForeignKey(pt => pt.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.CategoryId });

                entity.HasOne(pc => pc.Product)
                      .WithMany(p => p.ProductCategories)
                      .HasForeignKey(pc => pc.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(pc => pc.Category)
                      .WithMany(c => c.ProductCategories)
                      .HasForeignKey(pc => pc.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            //şəkillər üçün

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Image)
                      .IsRequired();

                entity.Property(e => e.IsDeleted)
                      .IsRequired();

                entity.Property(e => e.IsMain)
                      .IsRequired();

                entity.HasOne(e => e.Product)
                      .WithMany()
                      .HasForeignKey(e => e.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            //Mehsul Tipleri üçün

            modelBuilder.Entity<ProductTipe>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.TipeId });

                entity.HasOne(pt => pt.Product)
                      .WithMany(p => p.ProductTipes)
                      .HasForeignKey(pt => pt.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(pt => pt.Tipe)
                      .WithMany(t => t.ProductTipes)
                      .HasForeignKey(pt => pt.TipeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            
            modelBuilder.Entity<Tipe>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasMany(e => e.ProductTipes)
                      .WithOne(pt => pt.Tipe)
                      .HasForeignKey(pt => pt.TipeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
