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
        public int Quantity { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime DateCreated { get; set; }
        public int ProductId { get; set; }
        public virtual Product.DBModel.Product Product { get; set; }
        public int UserId { get; set; }
        public virtual User.DBModel.User User { get; set; }
    }
}
