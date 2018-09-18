using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biglesson_MVC.Models
{
    public class CartItem
    {
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