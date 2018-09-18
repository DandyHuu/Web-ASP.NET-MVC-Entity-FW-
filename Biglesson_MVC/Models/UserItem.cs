using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biglesson_MVC.Models
{
    public class UserItem
    {
        public int id { get; set; }

        public string userName { get; set; }

        public string pass { get; set; }

        public string name { get; set; }

        public string avartar { get; set; }

        public string email { get; set; }

        public string phone { get; set; }

        public string address { get; set; }

        public int? role_id { get; set; }

        public string Hinh { get; set; }

        public int SanPhamID { get; set; }

        public string TenSanPham { get; set; }

        public int DonGia { get; set; }

        public int SoLuong { get; set; }

        public int ThanhTien
        {
            get
            {
                return SoLuong * DonGia;
            }
        }
    }
}