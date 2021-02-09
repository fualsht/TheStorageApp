using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheStorageApp.Shared.Models;

namespace TheStorageApp.API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<ReceiptImage> ReceiptImages { get; set; }        
        public DbSet<Tag> Tags { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                Guid id = Guid.NewGuid();
                entity.HasKey(e => e.Id);
                entity.HasMany(e => e.Receipts).WithOne(e => e.ReceiptHolder).HasForeignKey(e => e.ReceiptHolderId);
                entity.HasData(new User
                {
                    Id = id,
                    Name = "<SYSTEM>",
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    FirstName = "system",
                    LastName = "user",
                    Email = "system@email.com",
                    Password = "password"
                });
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(x => x.CreatedBy);
                entity.HasOne(x => x.ModifiedBy);
            });

            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(x => x.CreatedBy);
                entity.HasOne(x => x.ModifiedBy);
                entity.HasOne<Shop>(x => x.Shop);
                entity.HasMany<ReceiptImage>(x => x.RecipetImages).WithOne(x => x.Receipt);
                entity.HasMany<Tag>(e => e.Tags).WithMany(x => x.Receipts);
            });

            modelBuilder.Entity<ReceiptImage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(x => x.CreatedBy);
                entity.HasOne(x => x.ModifiedBy);
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(x => x.CreatedBy);
                entity.HasOne(x => x.ModifiedBy);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.CreatedBy);
                entity.HasOne(e => e.ModifiedBy);
                entity.HasMany<Receipt>(e => e.Receipts).WithMany(x => x.Tags);
            });
        }

        public int SeedData()
        {
            User defaultUser = Users.SingleOrDefault(x => x.Name == "<SYSTEM>");

            if (defaultUser == null)
                return -1;

            var user = new Category
            {
                Id = Guid.NewGuid(),
                Name = "<DEFAULT>",
                Color = System.Drawing.Color.White.ToArgb(),
                ModifiedById = defaultUser.Id,
                CreatedById = defaultUser.Id,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                IsSelected = false
            };
            Categories.Add(user);

            var shop = new Shop
            {
                Id = Guid.NewGuid(),
                Name = "<DEFAULT>",
                ModifiedById = defaultUser.Id,
                CreatedById = defaultUser.Id,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                IsSelected = false
            };
            Shops.Add(shop);

            return base.SaveChanges();
        }
    }
}

