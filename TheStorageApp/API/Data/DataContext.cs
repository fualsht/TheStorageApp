using Microsoft.AspNetCore.Identity;
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
        public DbSet<Model> Models { get; set; }
        public DbSet<Field> Fields { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var adminid = Guid.NewGuid().ToString();
            var guestid = Guid.NewGuid().ToString();
            var userroleid = Guid.NewGuid().ToString();
            modelBuilder.Entity<AppRole>(entity =>
            {
                entity.HasData(new AppRole
                {
                    Id = adminid,
                    Name = "Admin"
                });
                entity.HasData(new AppRole
                {
                    Id = guestid,
                    Name = "Guest"
                });
                entity.HasData(new AppRole
                {
                    Id = userroleid,
                    Name = "User"
                });
            });

            var userid = Guid.NewGuid();
            modelBuilder.Entity<AppUser>(entity =>
            {
                Guid id = userid;
                entity.HasKey(e => e.Id);
                entity.Ignore(e => e.Password);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedNever();
                entity.HasMany(e => e.Receipts).WithOne(e => e.ReceiptHolder).HasForeignKey(e => e.ReceiptHolderId);
                entity.HasOne<AppRole>(e => e.Role).WithMany(e => e.RoleUsers).HasForeignKey(e => e.RoleId);
                entity.HasData(new AppUser
                {
                    Id = id.ToString(),
                    UserName = "<SYSTEM>",
                    FirstName = "system",
                    LastName = "user",
                    Email = "system@email.com",
                    RoleId = adminid
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
                    CreatedById = userid.ToString(),
                    ModifiedById = userid.ToString(),
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
                    CreatedById = userid.ToString(),
                    ModifiedById = userid.ToString(),
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
                    CreatedById = userid.ToString(),
                    ModifiedById = userid.ToString(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now
                });
            });

            modelBuilder.Entity<LogEntry>(entity => 
            {
                entity.HasKey(e => e.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedNever();
                entity.HasOne<AppUser>(x => x.CreatedBy).WithMany(x => x.CreatedByLogEntries).HasForeignKey(x => x.CreatedById);
                entity.Ignore(e => e.ModifiedById);
                entity.Ignore(e => e.ModifiedOn);
            });

            modelBuilder.Entity<Model>(entity => 
            {
                entity.HasKey(e => e.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedNever();
                entity.HasOne<AppUser>(x => x.CreatedBy).WithMany(x => x.CreatedByModels).HasForeignKey(x => x.CreatedById);
                entity.HasOne<AppUser>(x => x.ModifiedBy).WithMany(x => x.ModifiedByModels).HasForeignKey(x => x.ModifiedById);
                entity.HasMany<Field>(x => x.Fields).WithOne(x => x.Model).HasForeignKey(e => e.Id);
            });

            modelBuilder.Entity<Field>(entity => 
            {
                entity.HasKey(e => e.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedNever();
                entity.HasOne<AppUser>(x => x.CreatedBy).WithMany(x => x.CreatedByFields).HasForeignKey(x => x.CreatedById);
                entity.HasOne<AppUser>(x => x.ModifiedBy).WithMany(x => x.ModifiedByFields).HasForeignKey(x => x.ModifiedById);
            });

            modelBuilder.Entity<ModelRelationship>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedNever();
                entity.HasOne<AppUser>(x => x.CreatedBy).WithMany(x => x.CreatedByModelRelationships).HasForeignKey(x => x.CreatedById);
                entity.HasOne<AppUser>(x => x.ModifiedBy).WithMany(x => x.ModifiedByModelRelationships).HasForeignKey(x => x.ModifiedById);
                entity.HasOne<Model>(x => x.SorceModel).WithMany(x => x.SourceModelRelationships).HasForeignKey(x => x.SorceModelId);
                entity.HasOne<Model>(x => x.RelatedModel).WithMany(x => x.RelatedModelRelationships).HasForeignKey(x => x.RelatedModelId);
                entity.HasOne<Field>(x => x.SourceField).WithMany(x => x.SourceModelRelationships).HasForeignKey(x => x.SorceModelId);
                entity.HasOne<Field>(x => x.RelatedField).WithMany(x => x.RelatedModelRelationships).HasForeignKey(x => x.RelatedModelId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

