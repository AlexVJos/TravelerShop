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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginData data)
        {

            var uLoginData = new ULoginData
            {
                Credential = data.Username,
                Password = data.Password,
                Ip = "",
                FirstLoginTime = DateTime.Now
            };

            RResponseData responce = _session.UserLoginAction(uLoginData);
            if (responce != null && responce.Status)
            {
                //Coockie Generation
                UCoockieData cData = _session.GenCoockieAlgo(responce.CurrentUser);

                if (cData != null)
                {
                    //SET COOCKKE TO USER
                }
            }


            return View();
        }
    }
}