using Biglesson_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Biglesson_MVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        private Model_HiTech db = new Model_HiTech();

        public ActionResult Index(int ID = 0)
        {
            List<Category> Cate = new List<Category>();
            Cate = (from cate in db.Categories select cate).ToList();
            ViewBag.listCate = Cate;
            ViewBag.countCate = Cate.Count();

            List<Product> Pro = new List<Product>();
            Pro = (from product in db.Products select product).ToList();
            ViewBag.listPro = Pro;
            ViewBag.countPro = Pro.Count();

            if (ID == 0)
            {
                List<Product> minPro = new List<Product>();
                minPro = (from product in db.Products select product).OrderByDescending(x => x.id).ToList();
                ViewBag.list_minPro = minPro.Take(4);
                ViewBag.TextButton = "Xem tất cả sản phẩm";
                ViewBag.ButtonID = 1;
            }
            else if (ID == 1)
            {
                List<Product> minPro = new List<Product>();
                minPro = (from product in db.Products select product).OrderByDescending(x => x.id).ToList();
                ViewBag.list_minPro = minPro;
                ViewBag.TextButton = "Ẩn bớt";
                ViewBag.ButtonID =0 ;
            }

            List<Order> _Order = new List<Order>();
            _Order = (from order in db.Orders select order).ToList();
            ViewBag.listOrder = _Order;
            ViewBag.countOrder = _Order.Count();

            List<User> _User = new List<User>();
            _User = (from user in db.Users select user).ToList();
            ViewBag.listUser = _User;
            ViewBag.countUser = _User.Count();

            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;

            //Danh sách Order//
            //List<Order> order = new List<Order>();
            var orders = (from o in db.Orders
                         join od in db.OrderDetails on o.id equals od.order_id
                         join u in db.Users on o.user_id equals u.id
                         join p in db.Products on od.product_id equals p.id
                         select new
                         {
                             id = o.id,
                             name = u.name,
                             total = o.totalOrder,
                             id_product = p.id,
                             name_product = p.name_product,
                             image = p.image,
                             price = p.price,
                             address = u.address,
                             quantity = p.quantity

                         }).ToList();

            List<OrderItem> listOrderItem = new List<OrderItem>();
            foreach (var item in orders)
            {
                OrderItem newItem = new OrderItem()
                {
                    id_order = item.id,

                    name = item.name,
                    name_product = item.name_product,
                    id_product = item.id_product,
                    image = item.image,
                    price = item.price,
                    quantity = item.quantity,
                    address = item.address
                };

                listOrderItem.Add(newItem);
            }
            ViewBag.listOrderDetail = listOrderItem;
            //end//
            //Review//
            List<Review> review = new List<Review>();
            review = (from rw in db.Reviews select rw).ToList();
            ViewBag.listReview = review.OrderByDescending(x=>x.id);

            
            ViewBag.Message = "Admin";
            return View();
        }
        public RedirectToRouteResult DeleteReview(int ID)
        {
            Review re = db.Reviews.SingleOrDefault(x => x.id == ID);

            if (re != null)
            {
                db.Reviews.Remove(re);
                db.SaveChanges();
            }

            return RedirectToAction("Index","Admin");
        }

        public ActionResult Login(int check = 5 )
        {

            if (check == 0 || check == 1)
            {
                ViewBag.ErorrUser = "";
                ViewBag.ErorrPass = "";

                
            }

            else if (check == 2)
            {
                ViewBag.ErorrUser = "";
                ViewBag.ErorrPass = "Mật khẩu của bạn sai !!";
            }
            else if (check == 3)
            {

                ViewBag.ErorrUser = "Tài khoản của bạn không đúng !!";
                ViewBag.ErorrPass = "Mật khẩu của bạn sai !!";
            }
           
            ViewBag.Message = "Đăng nhập";
            return View();
        }

        public RedirectToRouteResult Logout()
        {
            Session.Abandon();

            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public ActionResult Signup()
        {
            ViewBag.mess = "";
            ViewBag.Message = "Đăng ký";
            return View();
        }
        [HttpPost]
        public RedirectToRouteResult Signup(FormCollection collection)
        {
            
            string Name = collection["name"];
            string user_name = collection["user_name"];
            string Pass = collection["password"];
            string Phone = collection["phone"];
            string Email = collection["email"];
            string Address = collection["address"];

            User newItem = new User()
            {
                name = Name,
                userName = user_name,
                pass = Pass,
                phone = Phone,
                email = Email,
                address = Address
            };

            db.Users.Add(newItem);
            db.SaveChanges();

            ViewBag.mess = "Bạn đã đăng kí thành công !";
            return RedirectToAction("Signup", "Admin");
        }
        public RedirectToRouteResult CheckLogin()
        {
            string user_name = Request.Form["name"];
            string password = Request.Form["password"];

           
            int check = 1;
            int id_user = 0;

            List<User> _User = new List<User>();
            _User = (from user in db.Users select user).ToList();

            foreach (var item in _User)
            {

                if (item.userName == user_name && item.pass == password)
                {
                    if (item.role_id == 1)
                    {
                        id_user = item.id;
                        check = 0;
                    }
                    else
                    {
                        id_user = item.id;
                        check = 1;
                    }

                    //Session Nguoi dung//
                    Session["UserSession"] = new List<UserItem>();

                    User listUser = db.Users.Find(id_user);
                    List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
                    ViewBag.listUserSession = UserSession;

                    UserItem newItem = new UserItem()
                    {
                        id = listUser.id,
                        userName = listUser.userName,
                        pass = listUser.pass,
                        email = listUser.email,
                        name = listUser.name,
                        avartar = listUser.avartar

                    };  // Tạo ra 1 CartItem mới
                    UserSession.Add(newItem);

                    break;
                }
                else if (item.userName == user_name && item.pass != password)
                {
                
                    check = 2;
                    break;
                }
                else if (item.userName != user_name && item.pass != password)
                {
                    check = 3;
                }
            }

            if (check == 0)
            {
                return RedirectToAction("Index", "Admin", new { check = 0 });
            }
            else if(check ==  1)
            {
                return RedirectToAction("Index", "Home", new { check = 1 });
            }
            else if (check == 2)
            {
                return RedirectToAction("Login", "Admin",new { check = 2});
            }
            else
            {
                
                return RedirectToAction("Login", "Admin", new { check =3});
            }
        }

        public ActionResult Category()
        {
            List<Category> Cate = new List<Category>();
            Cate = (from cate in db.Categories select cate).ToList();
            ViewBag.listCate = Cate;

            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;
            return View();
        }

       

        public ActionResult Products()
        {
            List<Product> Pro = new List<Product>();
            Pro = (from p in db.Products select p).ToList();
            ViewBag.listPro = Pro;

            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;

            return View();
        }

        public ActionResult Blogs()
        {
            List<Blog> blog = new List<Blog>();
            blog = (from b in db.Blogs select b).ToList();
            ViewBag.listBlog = blog;

            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;

            return View();
        }
        public ActionResult Users()
        {
            List<User> user = new List<User>();
            user = (from u in db.Users select u).ToList();
            ViewBag.listUser = user;

            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;

            return View();
        }

        public ActionResult Orders()
        {
            //List<Order> order = new List<Order>();
            var order = (from o in db.Orders
                        join od in db.OrderDetails on o.id equals od.order_id
                        join u in db.Users on o.user_id equals u.id
                        join p in db.Products on od.product_id equals p.id
                        select new
                        {
                            id = o.id,
                            name = u.name,
                            total = o.totalOrder,
                            id_product = p.id,
                            name_product = p.name_product,
                            image = p.image,
                            price = p.price,
                            address = u.address,
                            quantity = p.quantity

                        }).ToList();

            List<OrderItem> listOrderItem = new List<OrderItem>();
            foreach (var item in order)
            {
                OrderItem newItem = new OrderItem()
                {
                    id_order = item.id,

                    name = item.name,
                    name_product = item.name_product,
                    id_product = item.id_product,
                    image =item.image,
                    price = item.price,
                    quantity = item.quantity,
                    address =item.address
                };

                listOrderItem.Add(newItem);
            }
            ViewBag.listOrder = listOrderItem;

            List<Product> Pro = new List<Product>();
            Pro = (from p in db.Products select p).ToList();
            ViewBag.listPro = Pro;


            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;

            return View();
        }
    }
}