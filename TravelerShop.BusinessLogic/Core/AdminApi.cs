using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelerShop.Domain.Entities.GeneralResponse;
using TravelerShop.Domain.Entities.Product.DBModel;

namespace TravelerShop.BusinessLogic.Core
{
    public class AdminApi
    {
        internal RResponseData ProductAddToDb(Product product)
        {
            return new RResponseData { Status = false };
        }
    }
}
