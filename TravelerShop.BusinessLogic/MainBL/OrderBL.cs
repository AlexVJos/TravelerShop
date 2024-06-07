using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelerShop.BusinessLogic.Core;
using TravelerShop.BusinessLogic.Interfaces;
using TravelerShop.Domain.Entities.GeneralResponse;
using TravelerShop.Domain.Entities.Order.DBModel;

namespace TravelerShop.BusinessLogic.MainBL
{
    public class OrderBL : UserApi, IOrder
    {
        public ProdResponseData Create(Order order)
        {
            return CreateOrderAction(order);
        }

    }
}