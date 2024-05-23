using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelerShop.Domain.Entities.Cart.DBModel;

namespace TravelerShop.BusinessLogic.DBModel
{
    public class CartContext : DbContext
    {
        public CartContext() : base("name=TravelerShop")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                        .HasMany(c => c.Items)
                        .WithRequired()
                        .HasForeignKey(i => i.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
