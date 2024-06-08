using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelerShop.Domain.Entities.Order.DBModel;
using TravelerShop.Domain.Entities.Product.DBModel;

namespace TravelerShop.BusinessLogic.DBModel
{
    class ProductContext : DbContext
    {
        public ProductContext() :
            base("name=TravelerShop")
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                        .HasMany(c => c.Reviews)
                        .WithOptional()
                        .HasForeignKey(i => i.ProductId);

            base.OnModelCreating(modelBuilder);
        }
    }
}