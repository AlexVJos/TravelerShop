using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelerShop.Domain.Entities.Auth.DBModel
{
    public class Session
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string CookieString { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
