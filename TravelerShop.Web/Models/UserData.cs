using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelerShop.Web.Models
{
    public class UserData
    {
        public List<string> ProductsName { get; set; }
        public int ProductCount { get; set; }
        public string ProductSingleName { get; set; }
    }
}