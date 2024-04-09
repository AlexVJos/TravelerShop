using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelerShop.Domain.Enums;

namespace TravelerShop.Web.Models
{
    public class ProductData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public PCategory Category { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public int Amount { get; set; }
    }
}