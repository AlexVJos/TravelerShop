using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelerShop.Domain.Enums;

namespace TravelerShop.Domain.Entities.Product.DBModel
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Name cannot be longer than 30 characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(512, ErrorMessage = "Description cannot be longer than 512 characters")]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required]
        public PCategory Category { get; set; }
        [Required]
        public int Amount { get; set; } = 1;
        public double Rating { get; set; }
        public byte[] Image { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
