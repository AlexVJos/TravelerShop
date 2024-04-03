using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelerShop.Web.Models
{
    public class ProductData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public byte[] Image { get; set; }
        public int Amount { get; set; }
    }
}