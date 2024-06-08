using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelerShop.Domain.Entities.Product.DBModel;
using TravelerShop.Domain.Entities.Review.DBModel;

namespace TravelerShop.BusinessLogic.DBModel
{
    public class ReviewContext : DbContext
    {
        public ReviewContext() :
            base("name=TravelerShop")
        {
        }

        public virtual DbSet<Review> Reviews { get; set; }
    }
}