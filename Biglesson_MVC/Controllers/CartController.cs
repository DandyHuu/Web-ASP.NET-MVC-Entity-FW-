using Biglesson_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Biglesson_MVC.Controllers
{
    public class CartController : Controller
    {
        private Model_HiTech db = new Model_HiTech();
        // GET: Cart
        public RedirectToRouteResult Index()
        {
            //Giỏ hàng //

            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            ViewBag.Cart = giohang;
            return RedirectToAction("Index", "Home");
        }

        public RedirectToRouteResult Add(int SanPhamID)
        {
            if (Session["giohang"] == null) // 
            {
                Session["giohang"] = new List<CartItem>();  
            }

            List<CartItem> giohang = Session["giohang"] as List<CartItem>; 

            if (giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID) == null) // ko co sp nay trong gio hang
            {
                Product sp = db.Products.Find(SanPhamID);  // tim sp theo sanPhamID

                CartItem newItem = new CartItem()
                {
                    SanPhamID = SanPhamID,
                    TenSanPham = sp.name_product,
                    SoLuong = 1,
                    Hinh = sp.image,
                    DonGia = sp.price

                };  // Tạo ra 1 CartItem mới

                giohang.Add(newItem);  // Thêm CartItem vào giỏ 
            }
            else
            {
                // Nếu sản phẩm khách chọn đã có trong giỏ hàng thì không thêm vào giỏ nữa mà tăng số lượng lên.
                CartItem cardItem = giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID);
                cardItem.SoLuong++;
            }

            ViewBag.Cart = giohang;
            // Action này sẽ chuyển hướng về trang chi tiết sp khi khách hàng đặt vào giỏ thành công. Bạn có thể chuyển về chính trang khách hàng vừa đứng bằng lệnh return Redirect(Request.UrlReferrer.ToString()); nếu muốn.
            return RedirectToAction("Index", "Home");

        }

        public RedirectToRouteResult SuaSoLuong(int SanPhamID, int soluongmoi)
        {
            // tìm carditem muon sua
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem itemSua = giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID);
            if (itemSua != null)
            {
                itemSua.SoLuong = soluongmoi;
            }
            return RedirectToAction("Cart", "Home");

        }

        public RedirectToRouteResult XoaKhoiGio(int SanPhamID)
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem itemXoa = giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID);
            if (itemXoa != null)
            {
                giohang.Remove(itemXoa);
            }
            return RedirectToAction("Cart","Home");
        }


        public RedirectToRouteResult ThanhToan()
        {
            string Name = Request.Form["name"];
            string Phone = Request.Form["phone"];
            string Address = Request.Form["address"];
            int Total = Convert.ToInt32(Request.Form["total"]);

            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            ViewBag.Cart = giohang;

            User newUser = new User()
            {
                name = Name,
                phone = Phone,
                address = Address,
            };
            db.Users.Add(newUser);
            db.SaveChanges();


            User user = db.Users.LastOrDefault();
            
            Order newOder = new Order()
            {
               user_id = user.id,
               totalOrder = Total,

            };
            db.Orders.Add(newOder);
            db.SaveChanges();


            Order order = db.Orders.LastOrDefault();
            for (int i = 0; i < giohang.Count; i++)
            {
                OrderDetail newOrderDetail = new OrderDetail()
                {
                    order_id = order.id,
                    product_id = giohang[i].SanPhamID
                };
                db.OrderDetails.Add(newOrderDetail);
                db.SaveChanges();
            }

            ViewBag.Succ = "Bạn đã đặt hàng thành công!";
            return RedirectToAction("Cart", "Cart");
        }
    }
}