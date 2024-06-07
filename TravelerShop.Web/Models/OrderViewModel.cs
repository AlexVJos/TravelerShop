using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelerShop.Domain.Entities.Cart.DBModel;

namespace TravelerShop.Web.Models
{
    public class OrderViewModel
    {
        public Cart Cart { get; set; }
        public string BillingName { get; set; }
        public string BillingEmail { get; set; }
        public string BillingAddress { get; set; }
        public string BillingPhone { get; set; }
        public string ShippingAddress { get; set; }
        public string Comments { get; set; }
    }
}