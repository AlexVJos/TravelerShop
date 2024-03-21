using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelerShop.Domain.Entities.GeneralResponse
{
    public class RResponseData
    {
        public bool Status { get; set; }
        public string ResponceMessage { get; set; }

        public User.DBModel.User CurrentUser { get; set; }
    }
}
