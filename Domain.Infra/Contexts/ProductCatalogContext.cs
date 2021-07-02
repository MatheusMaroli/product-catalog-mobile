using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Infra.Contexts
{
    public class ProductCatalogContext : DbContext
    {
        public DbSet<ProductTags> ProductTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductTags>().ToTable("product_tag");
            modelBuilder.Entity<Product>().ToTable("product");
            modelBuilder.Entity<ProductImage>().ToTable("product_image");
            modelBuilder.Entity<User>().ToTable("user");
            modelBuilder.Entity<Tag>().ToTable("tag");
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var herokuConectionString = "Server=us-cdbr-east-04.cleardb.com;User Id=b3290100b347d2;Password=ee1f1da1;Database=heroku_30aab0d6275dad6";
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(herokuConectionString, ServerVersion.AutoDetect(herokuConectionString));
            }
        }
    }
}
