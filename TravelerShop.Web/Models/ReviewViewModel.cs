using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelerShop.Web.Models
{
    public class ReviewViewModel
    {
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string Title { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public string Comment { get; set; }
    }
}