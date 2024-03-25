using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelerShop.Domain.Entities.Product.DBModel
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
