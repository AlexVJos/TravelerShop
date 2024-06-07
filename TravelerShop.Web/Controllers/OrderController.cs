using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelerShop.BusinessLogic.Interfaces;
using TravelerShop.Domain.Entities.Cart.DBModel;
using TravelerShop.Domain.Entities.GeneralResponse;
using TravelerShop.Domain.Entities.Order.DBModel;
using TravelerShop.Domain.Entities.User.DBModel;
using TravelerShop.Web.Models;

namespace TravelerShop.Web.Controllers
{
    public class OrderController : Controller
    {
        internal ICart _cart;
        internal IOrder _order;
        public OrderController()
        {
            var logicBl = new BusinessLogic.BusinessLogic();
            _cart = logicBl.GetCartBL();
            _order = logicBl.GetOrderBL();
        }
        // GET: Order
        public ActionResult Index()
        {
            var currentUser = (User)HttpContext?.Session["__SessionObject"];
            var model = new OrderViewModel
            {
                Cart = _cart.GetByUserId(currentUser.Id)
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderViewModel model)
        {
            var currentUser = (User)HttpContext?.Session["__SessionObject"];
            model.Cart = _cart.GetByUserId(currentUser.Id);
            if (ModelState.IsValid)
            {
                List<OrderItem> items = new List<OrderItem>();
                foreach (var cartItem in model.Cart.Items)
                {
                    var orderItem = new OrderItem
                    {
                        ProductId = cartItem.ProductId,
                        Image = cartItem.Image,
                        Name = cartItem.Name,
                        Price = cartItem.Price,
                        Quantity = cartItem.Quantity,
                        SubTotal = cartItem.SubTotal
                    };
                    items.Add(orderItem);
                }
                var order = new Order
                {
                    UserId = model.Cart.UserId,
                    BillingName = model.BillingName,
                    BillingAddress = model.BillingAddress,
                    ShippingAddress = model.ShippingAddress,
                    BillingEmail = model.BillingEmail,
                    BillingPhone = model.BillingPhone,
                    Comments = model.Comments,
                    Total = model.Cart.Total,
                    OrderItems = items
                };
                ProdResponseData response = _order.Create(order);
                if (response.Status)
                {
                    _cart.ClearCart(model.Cart.Id);
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index", "Cart");

        }
    }
}