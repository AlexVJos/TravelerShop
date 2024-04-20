using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TravelerShop.BusinessLogic.Core;
using TravelerShop.BusinessLogic.Interfaces;
using TravelerShop.Domain.Entities.GeneralResponse;
using TravelerShop.Domain.Entities.Product;
using TravelerShop.Domain.Entities.Product.DBModel;

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
            return GetSingleProductAction(id);
        }

        public ProdResponseData AddProductToDb(Product prod)
        {
            return AddProductToDbAction(prod);
        }

        public ProdResponseData DeleteProduct(int id)
        {
            return DeleteProductAction(id);
        }
        public ProdResponseData EditProduct(Product product)
        {
            return EditProductAction(product);
        }
    }
}
