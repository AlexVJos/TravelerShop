﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelerShop.Domain.Entities.User;
using TravelerShop.Web.Models;

namespace TravelerShop.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(LoginData login)
        {
           return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult News()
        {
            return View();
        }

        public ActionResult SingleProduct()
        {
            return View();
        }
    }
}