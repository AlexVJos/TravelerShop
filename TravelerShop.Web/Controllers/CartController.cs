using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelerShop.BusinessLogic.Interfaces;
using TravelerShop.Domain.Entities.Cart.DBModel;
using TravelerShop.Domain.Entities.GeneralResponse;
using TravelerShop.Domain.Entities.Product.DBModel;
using TravelerShop.Domain.Entities.User.DBModel;

namespace TravelerShop.Web.Controllers
{
    public class CartController : Controller
    {
        internal ICart _cart;
        internal IProduct _product;
        public CartController()
        {
            var logicBl = new BusinessLogic.BusinessLogic();
            _cart = logicBl.GetCartBL();
            _product = logicBl.GetProductBL();
        }
        public ActionResult Index()
        {
            var currentUser = (User)HttpContext?.Session["__SessionObject"];
            if (currentUser == null)
            {
                throw new Exception("User not logged in");
            }

            var cart = _cart.GetByUserId(currentUser.Id);
            return View(cart);
        }
        public ActionResult AddToCart(int productId, int quantity)
        {
            var prod = _product.GetSingleProduct(productId).SingleProduct;
            var currentUser = (User)HttpContext?.Session["__SessionObject"];
            if(currentUser == null) { throw new Exception(); }
            var cartItem = new CartItem
            {
                ProductId = prod.ProductId,
                UserId = currentUser.Id,
                Image = prod.Image,
                Name = prod.Name,
                Price = prod.Price,
                Quantity = quantity,
                SubTotal = quantity * prod.Price
            };
            
            ProdResponseData prodResponseData = _cart.AddToCart(cartItem);
            
            return RedirectToAction("Index");
        }
    }
}