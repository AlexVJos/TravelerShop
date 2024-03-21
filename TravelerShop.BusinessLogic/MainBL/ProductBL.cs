using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelerShop.BusinessLogic.Core;
using TravelerShop.BusinessLogic.Interfaces;
using TravelerShop.Domain.Entities.Product;

namespace TravelerShop.BusinessLogic.MainBL
{
    public class ProductBL : UserApi, IProduct
    {
        public ProductDataModel GetProductsToList()
        {
            return ProductActionGetToList();
        }

        public ProductDataModel GetSingleProduct(int id)
        {
            return ProductGetSingleAction(id);
        }
    }
}
