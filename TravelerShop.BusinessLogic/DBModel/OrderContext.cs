using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelerShop.Domain.Entities.Cart.DBModel;
using TravelerShop.Domain.Entities.Order.DBModel;

namespace TravelerShop.BusinessLogic.DBModel
{
    public class OrderContext : DbContext
    {
        public OrderContext() : base("name=TravelerShop")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                        .HasMany(c => c.OrderItems)
                        .WithRequired()
                        .HasForeignKey(i => i.OrderId);

            base.OnModelCreating(modelBuilder);
        }
    }
}