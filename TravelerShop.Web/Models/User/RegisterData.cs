﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelerShop.Web.Models.User
{
    public class RegisterData
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}