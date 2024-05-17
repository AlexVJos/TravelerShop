using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelerShop.BusinessLogic.Interfaces;
using TravelerShop.BusinessLogic;
using TravelerShop.Domain.Entities.Product;
using TravelerShop.Web.Models;
using TravelerShop.Domain.Entities.Product.DBModel;
using TravelerShop.Domain.Entities.GeneralResponse;
using TravelerShop.Domain.Entities.User;
using System.IO;
using System.Web.UI.WebControls;
using TravelerShop.Web.Attributes;

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
            return View(products);
        }

        public ActionResult SingleProduct(int id)
        {

            ProductDataModel singleProduct = _product.GetSingleProduct(id);
            return View(singleProduct);
        }

        //[Authorize(Roles = "Admin")]
        public ActionResult AddNewProduct()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminMod]
        public ActionResult AddNewProduct(ProductData model)
        {
            if (ModelState.IsValid)
            {               
                var product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Category = model.Category,
                    Amount = model.Amount
                };

                if (model.ImageFile != null && model.ImageFile.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(model.ImageFile.InputStream))
                    {
                        product.Image = binaryReader.ReadBytes(model.ImageFile.ContentLength);
                    }
                }

                ProdResponseData responce = _product.AddProductToDb(product);
                if (responce.Status)
                {
                    return RedirectToAction("Index", "Product");
                }
            }
            return View();
        }
        [AdminMod]
        public ActionResult Delete(int id)
        {
            ProdResponseData response = _product.DeleteProduct(id);
            if(response.Status)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [AdminMod]
        public ActionResult Edit(int id)
        {
            ProductDataModel singleProduct = _product.GetSingleProduct(id);
            return View(singleProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminMod]
        public ActionResult Edit(ProductDataModel data)
        {
            if(ModelState.IsValid)
            {
                var product = new Product
                {
                    ProductId = data.SingleProduct.ProductId,
                    Name = data.SingleProduct.Name,
                    Description = data.SingleProduct.Description,
                    Price = data.SingleProduct.Price,
                    Category = data.SingleProduct.Category,
                    Amount = data.SingleProduct.Amount,
                };
                using (var binaryReader = new BinaryReader(data.ImageFile.InputStream))
                {
                    product.Image = binaryReader.ReadBytes(data.ImageFile.ContentLength);
                }

                ProdResponseData response = _product.EditProduct(product);
                if(response.Status)
                    return RedirectToAction("Index", "Product");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}