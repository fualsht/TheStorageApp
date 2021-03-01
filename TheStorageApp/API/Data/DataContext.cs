using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TheStorageApp.API.Models;

namespace TheStorageApp.API.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<ReceiptImage> ReceiptImages { get; set; }        
        public DbSet<Tag> Tags { get; set; }
        public DbSet<LogEntry> Logs { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var guid = Guid.NewGuid();
            modelBuilder.Entity<AppUser>(entity =>
            {
                Guid id = guid;
                entity.HasKey(e => e.Id);
                entity.Ignore(e => e.Password);
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
                entity.HasOne<AppUser>(x => x.CreatedBy).WithMany(x => x.CreatedByCategories).HasForeignKey(x => x.CreatedById);
                entity.HasOne<AppUser>(x => x.ModifiedBy).WithMany(x => x.ModifiedByCategories).HasForeignKey(x => x.ModifiedById);
                entity.HasData(new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "<DEFAULT>",
                    Color = "555555",
                    CreatedById = guid.ToString(),
                    ModifiedById = guid.ToString(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now
                });
            });

            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedNever();
                entity.HasOne<AppUser>(x => x.CreatedBy).WithMany(x => x.CreatedByReceipts).HasForeignKey(x => x.CreatedById);
                entity.HasOne<AppUser>(x => x.ModifiedBy).WithMany(x => x.ModifiedByReceipts).HasForeignKey(x => x.ModifiedById);
                entity.HasOne<Shop>(x => x.Shop);
                entity.HasMany<ReceiptImage>(x => x.RecipetImages).WithOne(x => x.Receipt).HasForeignKey(e => e.Id);

                entity.HasMany<Tag>(e => e.Tags).WithMany(x => x.Receipts);
            });

            modelBuilder.Entity<ReceiptImage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedNever();
                entity.HasOne<AppUser>(x => x.CreatedBy).WithMany(x => x.CreatedByReceiptImages).HasForeignKey(x => x.CreatedById);
                entity.HasOne<AppUser>(x => x.ModifiedBy).WithMany(x => x.ModifiedByReceiptImages).HasForeignKey(x => x.ModifiedById);

            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedNever();
                entity.HasOne<AppUser>(x => x.CreatedBy).WithMany(x => x.CreatedByShops).HasForeignKey(x => x.CreatedById);
                entity.HasOne<AppUser>(x => x.ModifiedBy).WithMany(x => x.ModifiedByShops).HasForeignKey(x => x.ModifiedById);
                entity.HasData(new Shop
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "<DEFAULT>",
                    GPSLocation = "",
                    Address = "",
                    Website = "",
                    CreatedById = guid.ToString(),
                    ModifiedById = guid.ToString(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now
                });
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedNever();
                entity.HasOne<AppUser>(x => x.CreatedBy).WithMany(x => x.CreatedByTags).HasForeignKey(x => x.CreatedById);
                entity.HasOne<AppUser>(x => x.ModifiedBy).WithMany(x => x.ModifiedByTags).HasForeignKey(x => x.ModifiedById);
                entity.HasMany<Receipt>(e => e.Receipts).WithMany(x => x.Tags);
                entity.HasData(new Tag
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "<DEFAULT>",
                    Color = "555555",
                    CreatedById = guid.ToString(),
                    ModifiedById = guid.ToString(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now
                });
            });

            modelBuilder.Entity<LogEntry>(entity => 
            {
                entity.HasKey(e => e.Id);
                entity.Property(p => p.Id).ValueGeneratedNever();
                entity.HasOne<AppUser>(x => x.CreatedBy).WithMany(x => x.CreatedByLogEntries).HasForeignKey(x => x.CreatedById);
                entity.Ignore(e => e.ModifiedById);
                entity.Ignore(e => e.ModifiedOn);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

