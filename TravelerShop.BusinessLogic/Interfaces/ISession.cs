using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        UCoockieData GenCoockieAlgo(User dataUser);
    }
}
