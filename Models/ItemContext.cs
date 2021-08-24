using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using System.Collections.Generic;

using items_service.Models;

namespace items_service
{
    public class ItemContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Bin> Bins { get; set; }
        public DbSet<Cabinet> Cabinets { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<CabinetBin> CabinetBins { get; set; }
        public DbSet<BinItem> BinItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ItemRemoved> ItemsRemoved { get; set; }
        public DbSet<ItemReturned> ItemsReturned { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=dispenser-system;user=root;password=admin");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).IsRequired();
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.Expiry).IsRequired();
                entity.Property(e => e.DateAdded).IsRequired();
                entity.Property(e => e.GenericName).IsRequired();
                entity.Property(e => e.SupplierID).IsRequired();
                // entity.HasOne(e => e.Supplier);
            });

            modelBuilder.Entity<Cabinet>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).IsRequired();
                entity.Property(e => e.Tag).IsRequired();
                // entity.HasMany(e => e.Bins);
            });

            modelBuilder.Entity<Bin>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).IsRequired();
                entity.Property(e => e.Tag).IsRequired();
                entity.Property(e => e.Capacity).IsRequired();
                entity.Property(e => e.DateAdded).IsRequired();
                // entity.HasMany(e => e.Items);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).IsRequired();
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).IsRequired();
                entity.Property(e => e.Name).IsRequired();
                // entity.HasMany(e => e.Items);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).IsRequired();
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.ItemID).IsRequired();
                entity.Property(e => e.SupplierID).IsRequired();
                // entity.HasOne(e => e.Item);
                // entity.HasOne(e => e.Supplier);
            });

            modelBuilder.Entity<BinItem>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).IsRequired();
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.DateAdded).IsRequired();
                // entity.HasOne(e => e.Bin);
                // entity.HasMany(e => e.Items);
            });

            // dont know if required
            modelBuilder.Entity<CabinetBin>(entity =>
            {
                entity.HasNoKey();
                entity.Property(e => e.DateAdded).IsRequired();
                // entity.HasOne(e => e.Cabinet);
                // entity.HasOne(e => e.Bin);
            });

            modelBuilder.Entity<ItemRemoved>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).IsRequired();
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.DateRemoved).IsRequired();
                // entity.HasOne(e => e.Patient);
                // entity.HasOne(e => e.BinItem);
            });

            modelBuilder.Entity<ItemReturned>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).IsRequired();
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.DateReturned).IsRequired();
                // entity.HasOne(e => e.BinItem);
            });
        }
    }
}