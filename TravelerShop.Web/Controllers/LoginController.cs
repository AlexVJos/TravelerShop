using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelerShop.BusinessLogic.Interfaces;
using TravelerShop.BusinessLogic;
using TravelerShop.Domain.Entities.Auth;
using TravelerShop.Domain.Entities.GeneralResponse;
using TravelerShop.Domain.Entities.User;
using TravelerShop.Web.Models;
using TravelerShop.Web.Models.User;
using TravelerShop.Domain.Entities.User.DBModel;

namespace TravelerShop.Web.Controllers
{
    public class LoginController : Controller
    {
        internal ISession _session;
        public LoginController()
        {
            var logicBl = new BusinessLogic.BusinessLogic();
            _session = logicBl.GetSessionBL();
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SignIn()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            System.Web.HttpContext.Current.Session.Clear();
            if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("X-KEY"))
            {
                var cookie = ControllerContext.HttpContext.Request.Cookies["X-KEY"];
                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                }
            }
            System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";
            return RedirectToAction("SignIn","Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(LoginData data)
        {
            var uLoginData = new ULoginData
            {
                Username = data.Username,
                Password = data.Password,
                Ip = Request.UserHostAddress,
                LoginDate = DateTime.Now
            };

            RResponseData response = _session.UserLoginAction(uLoginData);
            if (response != null && response.Status)
            {
                HttpCookie cookie = _session.GenerateCoockie(response.CurrentUser.Username);

                if (cookie != null)
                {
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                }
                return RedirectToAction("Index", "Home");
            }
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterData data)
        {
            var uRegisterData = new URegisterData
            {
                Username = data.Username,
                Name = data.Name,
                Surname = data.Surname,
                Email = data.Email,
                Password = data.Password,
                Ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"],
                RegistrationDate = DateTime.Now
            };

            RResponseData response = _session.UserRegisterAction(uRegisterData);
            if (response != null && response.Status)
            {
                //Cookie Generation
                HttpCookie cookie = _session.GenerateCoockie(response.CurrentUser.Username);

                if (cookie != null)
                {
                    //SET COOKIE TO USER
                    cookie.Expires = DateTime.Now.AddMinutes(60);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                }
            }


            return RedirectToAction("Index", "Home");
        }
    }
}