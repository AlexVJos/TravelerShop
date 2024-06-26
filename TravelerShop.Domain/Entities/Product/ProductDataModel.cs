﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TravelerShop.Domain.Entities.Product
{
    public class ProductDataModel
    {
        public DBModel.Product SingleProduct { get; set; }
        public List<DBModel.Product> Products { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
