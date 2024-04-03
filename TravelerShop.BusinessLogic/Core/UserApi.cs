using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelerShop.Domain.Entities.Product.DBModel;
using TravelerShop.Domain.Entities.Product;
using TravelerShop.Domain.Entities.User;
using TravelerShop.Domain.Entities.GeneralResponse;
using TravelerShop.Domain.Entities.Auth;
using TravelerShop.Domain.Entities.User.DBModel;

namespace TravelerShop.BusinessLogic.Core
{
    public class UserApi
    {
        internal RResponseData ULASessionCheck(ULoginData data)
        {

            //db connection

            return new RResponseData
            {
                Status = false,
                CurrentUser = new User
                {
                    Username = "Vasilica"
                }
            };
        }
        internal UCoockieData UserCoockieGenerationAlg(User user)
        {

            //COOCKIE GENERATION

            return new UCoockieData
            {
                MaxAge = 1709044385,
                Coockie = "MY UNIQUE ID FOR THIS SESSION"
            };
        }


        //PRODUCT

        internal ProductDataModel ProductActionGetToList()
        {




            var products = new List<Product>();


            return new ProductDataModel { Products = products };
        }
        internal ProductDataModel ProductGetSingleAction(int id)
        {


            var product = new Product();

            return new ProductDataModel { SingleProduct = product };

        }

        //-----------------------------УБРАТЬ ЭТОТ МЕТОД ОТСЮДА------------------------------------//
        internal ProdResponseData ProductAddToDb(Product product)
        {
            //CHECK IF UNIQUE
            //ADD PRODUCT TO DB
            //return new RResponseData { Status = true };
            return new ProdResponseData { Status = false };
        }


    }
}
