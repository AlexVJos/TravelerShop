using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelerShop.BusinessLogic.Interfaces;
using TravelerShop.BusinessLogic;
using TravelerShop.Domain.Entities.Product;

namespace TravelerShop.Web.Controllers
{
    public class ProductController : Controller
    {
        internal IProduct _product;
        public ProductController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _product = bl.GetProductBL();
        }

        // GET: Product
        public ActionResult Index()
        {

            ProductDataModel products = _product.GetProductsToList();

            var model = new
            {
                products
            };


            return View(model);
        }

        public ActionResult Details(int id)
        {

            ProductDataModel singleProduct = _product.GetSingleProduct(id);

            var model = new
            {
                singleProduct
            };

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            return View();
        }
    }
}