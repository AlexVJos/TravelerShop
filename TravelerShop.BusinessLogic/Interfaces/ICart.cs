using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelerShop.Domain.Entities.Cart.DBModel;
using TravelerShop.Domain.Entities.GeneralResponse;

namespace TravelerShop.BusinessLogic.Interfaces
{
    public interface ICart
    {
        Cart FindOrCreateByUserId(int id);
        ProdResponseData AddToCart(CartItem cartItem);
        Cart GetByUserId(int userId);
        ProdResponseData DeleteItem(int itemId);
        ProdResponseData UpdateItem(int itemId, int itemQuantity);
        void ClearCart(int cartId);
    }
}
