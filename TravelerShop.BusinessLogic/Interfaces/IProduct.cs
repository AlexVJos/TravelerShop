using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelerShop.Domain.Entities.GeneralResponse;
using TravelerShop.Domain.Entities.Product;
using TravelerShop.Domain.Entities.Product.DBModel;

namespace TravelerShop.BusinessLogic.Interfaces
{
    public interface IProduct
    {
        ProductDataModel GetProductsToList();
        ProductDataModel GetSingleProduct(int id);
        ProdResponseData AddProductToDb(Product product);
        ProdResponseData DeleteProduct(int id);
        ProdResponseData EditProduct(Product product);
    }
}
