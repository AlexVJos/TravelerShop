using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelerShop.Domain.Entities.GeneralResponse;
using TravelerShop.Domain.Entities.Order.DBModel;

namespace TravelerShop.BusinessLogic.Interfaces
{
    public interface IOrder
    {
        ProdResponseData Create(Order order);
    }
}