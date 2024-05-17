using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelerShop.Domain.Entities.Auth.DBModel;

namespace TravelerShop.BusinessLogic.DBModel
{
    public class SessionContext : DbContext
    {
        public SessionContext() :
            base("name=TravelerShop")
        {
        }
        public virtual DbSet<Session> Sessions { get; set; }
    }
}
