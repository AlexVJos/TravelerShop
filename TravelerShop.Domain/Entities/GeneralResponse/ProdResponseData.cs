using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelerShop.Domain.Entities.GeneralResponse
{
    public class ProdResponseData
    {
        public bool Status { get; set; }
        public string ResponseMessage { get; set; }
        public Product.DBModel.Product CurrentProduct { get; set; }
    }
}
