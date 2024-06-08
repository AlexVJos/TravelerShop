using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelerShop.Domain.Entities.User;
using TravelerShop.Web.Models;

namespace TravelerShop.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            /*SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("SignIn", "Login");
            }*/
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Cart()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("SignIn", "Login");
            }
            return RedirectToAction("Index", "Cart");
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
    }
}