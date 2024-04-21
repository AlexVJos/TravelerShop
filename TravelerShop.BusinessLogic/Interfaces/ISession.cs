using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TravelerShop.Domain.Entities.Auth;
using TravelerShop.Domain.Entities.GeneralResponse;
using TravelerShop.Domain.Entities.User;
using TravelerShop.Domain.Entities.User.DBModel;

namespace TravelerShop.BusinessLogic.Interfaces
{
    public interface ISession
    {
        RResponseData UserLoginAction(ULoginData data);
        RResponseData UserRegisterAction(URegisterData data);
        HttpCookie GenerateCoockie(string username);
        User GetUserByCookie(string value);
        User GetUserByUsername(string username);
    }
}
