using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelerShop.BusinessLogic.Core;
using TravelerShop.BusinessLogic.Interfaces;
using TravelerShop.Domain.Entities.Cart.DBModel;
using TravelerShop.Domain.Entities.GeneralResponse;

namespace TravelerShop.BusinessLogic.MainBL
{
    public class CartBL : UserApi, ICart
    {
        public Cart FindOrCreateByUserId(int userId)
        {
            return FindOrCreateCartService(userId);
        }
        public ProdResponseData AddToCart(CartItem cartItem)
        {
            return AddToCartService(cartItem);
        }
        public Cart GetByUserId(int userId)
        {
            return GetByUserIdService(userId);
        }
    }
}
