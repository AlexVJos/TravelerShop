using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelerShop.BusinessLogic.Interfaces;
using TravelerShop.BusinessLogic.MainBL;

namespace TravelerShop.BusinessLogic
{
    public class BusinessLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }
        public IProduct GetProductBL()
        {
            return new ProductBL();
        }
        public ICart GetCartBL()
        {
            return new CartBL();
        }
        public IOrder GetOrderBL()
        {
            return new OrderBL();
        }
    }
}
