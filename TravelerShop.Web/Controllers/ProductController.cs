﻿using System;
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

        public ActionResult AddNewProduct()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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

                ProdResponseData responce = _product.AddProdToDb(product);
                if (responce.Status)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
    }
}