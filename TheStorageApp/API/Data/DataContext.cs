using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TheStorageApp.API.Models;

namespace TheStorageApp.API.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        //public DbSet<AppUser> Users { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<ReceiptImage> ReceiptImages { get; set; }        
        public DbSet<Tag> Tags { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var guid = Guid.NewGuid();
            modelBuilder.Entity<AppUser>(entity =>
            {
                Guid id = guid;
                entity.HasKey(e => e.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedNever();
                entity.HasMany(e => e.Receipts).WithOne(e => e.ReceiptHolder).HasForeignKey(e => e.ReceiptHolderId);
                entity.HasData(new AppUser
                {
                    Id = id.ToString(),
                    UserName = "<SYSTEM>",
                    FirstName = "system",
                    LastName = "user",
                    Email = "system@email.com"
                });
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedNever();

                entity.Ignore(x => x.CreatedBy);                
                entity.Ignore(x => x.ModifiedBy);

                entity.HasData(new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "<DEFAULT>",
                    Color = "555555",
                    CreatedById = guid,
                    ModifiedById = guid,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now
                });
            });

            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedNever();
                entity.Ignore(x => x.CreatedBy);
                entity.Ignore(x => x.ModifiedBy);
                entity.HasOne<Shop>(x => x.Shop);
                entity.HasMany<ReceiptImage>(x => x.RecipetImages).WithOne(x => x.Receipt).HasForeignKey(e => e.Id);

                entity.HasMany<Tag>(e => e.Tags).WithMany(x => x.Receipts);
            });

            modelBuilder.Entity<ReceiptImage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedNever();
                entity.Ignore(x => x.CreatedBy);
                entity.Ignore(x => x.ModifiedBy);

            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedNever();
                entity.Ignore(x => x.CreatedBy);
                entity.Ignore(x => x.ModifiedBy);
                entity.HasData(new Shop
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "<DEFAULT>",
                    GPSLocation = "",
                    Address = "",
                    Website = "",
                    CreatedById = guid,
                    ModifiedById = guid,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now
                });
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedNever();
                entity.Ignore(x => x.CreatedBy);
                entity.Ignore(x => x.ModifiedBy);
                entity.HasMany<Receipt>(e => e.Receipts).WithMany(x => x.Tags);
                entity.HasData(new Tag
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "<DEFAULT>",
                    Color = "555555",
                    CreatedById = guid,
                    ModifiedById = guid,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now
                });
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

