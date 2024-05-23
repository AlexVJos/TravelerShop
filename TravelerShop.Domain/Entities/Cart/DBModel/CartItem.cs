using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelerShop.Domain.Entities.Product.DBModel;
using TravelerShop.Domain.Entities.User.DBModel;

namespace TravelerShop.Domain.Entities.Cart.DBModel
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public byte[] Image { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
    }
}
