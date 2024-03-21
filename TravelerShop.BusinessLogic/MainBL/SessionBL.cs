﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelerShop.BusinessLogic.Core;
using TravelerShop.BusinessLogic.Interfaces;
using TravelerShop.Domain.Entities.Auth;
using TravelerShop.Domain.Entities.GeneralResponse;
using TravelerShop.Domain.Entities.User.DBModel;
using TravelerShop.Domain.Entities.User;

namespace TravelerShop.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public RResponseData UserLoginAction(ULoginData data)
        {
            return ULASessionCheck(data);
        }

        public UCoockieData GenCoockieAlgo(User dataUser)
        {
            return UserCoockieGenerationAlg(dataUser);
        }
    }
}
