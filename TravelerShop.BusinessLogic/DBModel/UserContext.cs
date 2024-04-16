using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelerShop.Domain.Entities.User;
using TravelerShop.Domain.Entities.User.DBModel;

namespace TravelerShop.BusinessLogic.DBModel
{
    public class UserContext: DbContext
    {
        public UserContext() :
            base("name=eUseControl")
        {
        }
        public virtual DbSet<User> Users {  get; set; }
    }
}
