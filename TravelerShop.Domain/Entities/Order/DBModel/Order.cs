using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelerShop.Domain.Entities.Cart.DBModel;
using TravelerShop.Domain.Enums;

namespace TravelerShop.Domain.Entities.Order.DBModel
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        //public int CartId { get; set; }
        [Required]
        public string BillingName { get; set; }
        [Required]
        public string BillingEmail { get; set; }
        [Required]
        public string BillingAddress { get; set; }
        [Required]
        public string BillingPhone { get; set; }
        [StringLength(150)]
        public string Comments { get; set; }
        [Required]
        public string ShippingAddress { get; set; }
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal Total { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
    }
}