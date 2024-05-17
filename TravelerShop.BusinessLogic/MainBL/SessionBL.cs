using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using TravelerShop.BusinessLogic.Core;
using TravelerShop.BusinessLogic.Interfaces;
using TravelerShop.Domain.Entities.Auth;
using TravelerShop.Domain.Entities.GeneralResponse;
using TravelerShop.Domain.Entities.User.DBModel;
using TravelerShop.Domain.Entities.User;
using System.Runtime.CompilerServices;

namespace TravelerShop.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public RResponseData UserLoginAction(ULoginData data)
        {
            return LoginUpService(data);
        }
        public RResponseData UserRegisterAction(URegisterData data)
        {
            return RegisterService(data);
        }
        public HttpCookie GenerateCoockie(string username)
        {
            return CoockieGenerationService(username);
        }
        public User GetUserByCookie(string value)
        {
            return GetUserByCookieService(value);
        }
        public User GetUserByUsername(string username)
        {
            return GetUserByUsernameService(username);
        }
    }
}
