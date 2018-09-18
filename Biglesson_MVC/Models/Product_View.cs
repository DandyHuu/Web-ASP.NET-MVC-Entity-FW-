using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biglesson_MVC.Models
{
    public class Product_View
    {
        public int ID { get; set; }

        public string Image { get; set; }

        public string Dicription { get; set; }

        public string Name { get; set; }

        public int? Vote { get; set; }
    }
}