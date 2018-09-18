using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biglesson_MVC.Models
{
    public class OrderItem
    {
        public int id_order { get; set; }

        public string name { get; set; }
        public string address { get; set; }

        public int id_product { get; set; }

        public string name_product { get; set; }

        public string image { get; set; }

        public int quantity { get; set; }

        public int price { get; set; }

        public int ThanhTien
        {
            get
            {
                return quantity * price;
            }
        }
        

    }
}