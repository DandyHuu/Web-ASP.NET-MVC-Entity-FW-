using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Biglesson_MVC.Models;

namespace Biglesson_MVC.Controllers
{
    public class HomeController : Controller
    {
        private Model_HiTech db = new Model_HiTech();
        public List<Category> cate = new List<Category>();
        public List<Brand> brand = new List<Brand>();
       

        

        public ActionResult Index()
        {
            //Slide//
            List<Slide> slide = new List<Slide>();
            slide = (from sl in db.Slides select sl).OrderByDescending(x => x.id).ToList();
            ViewBag.listSlide = slide;
            //Danh mục//
            List<Category> Categories = new List<Category>();
            Categories = (from cate in db.Categories select cate).ToList();
            ViewBag.listCategories = Categories;

            //Hãng//
            List<Brand> Brands = new List<Brand>();
            Brands = (from brand in db.Brands select brand).ToList();
            ViewBag.listBrands = Brands;

            //yêu thích//
            List<Product> Feature = new List<Product>();
            Feature = (from product in db.Products where product.favorite == 1 && product.star >= 4 && product.cate_id >= 2 select product).ToList();
            ViewBag.listFeature = Feature;

            //hàng giảm giá//
            List<Product> Sales = new List<Product>();
            Sales = (from product in db.Products where product.sale > 10 && product.star >= 4 select product).ToList();
            ViewBag.listSale = Sales.Take(10);

            List<Product> SaleAccess = new List<Product>();
            SaleAccess = (from product in db.Products where product.sale > 0 && product.star >= 4 && product.cate_id == 4 select product).ToList();
            ViewBag.listSaleAccess = SaleAccess.Take(12);

            List<Product> SaleLaptop = new List<Product>();
            SaleLaptop = (from product in db.Products where product.sale > 10 && product.star >= 4 && product.cate_id == 1 select product).ToList();
            ViewBag.listSaleLaptop = SaleLaptop.Take(12);

            //Hàng mới/
            List<Product> News = new List<Product>();
            News = (from product in db.Products where product._new  == 1  select product).ToList();
            ViewBag.listNew = News.Take(8);

            List<Product> Stars = new List<Product>();
            Stars = (from product in db.Products where product.star >= 4 select product).ToList();
            ViewBag.listStar = Stars.Take(16);

            //Phụ kiện//
            List<Product> Access = new List<Product>();
            Access = (from product in db.Products where product.star >= 4 && product.cate_id == 4 select product).ToList();
            ViewBag.listAccess = Access.Take(16);

            //Lap top//
            List<Product> Laptop = new List<Product>();
            Laptop = (from product in db.Products where product.star >= 4 && product.cate_id == 1 select product).ToList();
            ViewBag.listLaptop = Laptop.Take(16);

            //Đánh giá//
            List<Product_View> review = new List<Product_View>();
            var Review = db.Views.Join(db.Products, v => v.product_id, p => p.id, (v, p) => new 
            {
                id = p.id,
                name = v.name,
                comment = v.comment,
                image = p.image,
                vote = v.vote
            }).ToList();
            foreach (var item in Review)
            {
                Product_View newItem = new Product_View()
                {
                    ID = item.id,
                    Name = item.name,
                    Dicription = item.comment,
                    Image = item.image,
                    Vote = item.vote
                };

                review.Add(newItem);
            }
            
            ViewBag.listReview = review.OrderByDescending(x => x.ID).ToList();

            //Sản phẩm đề cử//
            Random r = new Random();
            List<Product> NewHot = new List<Product>();
            NewHot = (from product in db.Products where product._new == 1 && product.star == 5 select product).ToList();
            ViewBag.listNewHot = NewHot.OrderBy(product => r.Next()).Take(1);

            //Sản phẩm đề cử//
            List<Product> Recent = new List<Product>();
            Recent = (from product in db.Products where product._new == 1 && product.star == 5  select product).ToList();
            ViewBag.listRecent = Recent.Take(8);

            //Giỏ hàng//
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            ViewBag.Cart = giohang;

            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;

            return View();
           

        }

        public ActionResult Product(int flat, string search = null, int ID = 0, int page = 0 , int pageSize = 17)
        {
            
            //Danh mục//
            List<Category> Categories = new List<Category>();
            Categories = (from cate in db.Categories select cate).ToList();
            ViewBag.listCategories = Categories;

            //Hãng//
            List<Brand> Brands = new List<Brand>();
            Brands = (from brand in db.Brands select brand).ToList();
            ViewBag.listBrands = Brands;

            
            if (flat == 0)
            {
                if (page == 0)
                {
                    //Tất cả sản phẩm//
                    List<Product> Pro = new List<Product>();
                    Pro = (from product in db.Products select product).ToList();
                    ViewBag.listPro = Pro.Take(pageSize);
                    ViewBag.CountPro = Pro.Count();
                    ViewBag.flat = flat;
                    ViewBag.Message = "Tất cả sản phẩm";
                }

                else if (page == 1 || page ==2 || page == 3)
                {
                    //Tất cả sản phẩm//
                    List<Product> Pro = new List<Product>();
                    Pro = (from product in db.Products select product).ToList();
                    ViewBag.listPro = Pro.Skip(14*page).Take(pageSize);
                    ViewBag.CountPro = Pro.Count();
                    ViewBag.Message = "Tất cả sản phẩm";
                    ViewBag.flat = flat;
                }
            }
            else if (flat == 1 && search != "" && ID == 0)
            {
                //Tìm sản phẩm theo tên//
                search = Request.Form["search"];
                List<Product> Pro = new List<Product>();
                Pro = (from product in db.Products where product.name_product.Contains(search) select product).ToList();
                ViewBag.listPro = Pro.Skip(14 * page).Take(pageSize); 
                ViewBag.CountPro = Pro.Count();
                ViewBag.flat = flat;
                ViewBag.Message = "Tìm kiếm :"+ search;
            }
            else if (flat == 2 && search == null && ID != 0)
            {
                //Tất cả sản phẩm theo danh mục//
                List<Product> Pro = new List<Product>();
                Pro = (from product in db.Products where product.cate_id == ID select product).ToList();

                List<Category> Cate = new List<Category>();
                Cate = (from cate in db.Categories where cate.id == ID select cate).ToList();

                ViewBag.listPro = Pro.Skip(14 * page).Take(pageSize);
                ViewBag.CountPro = Pro.Count();
                ViewBag.flat = flat;
                foreach (var item in Cate)
                {
                    ViewBag.Message =  item.name;

                }
            }
            else if (flat == 3 && search == null && ID != 0)
            {
                //Tất cả sản phẩm theo Hãng//
                List<Product> Pro = new List<Product>();
                Pro = (from product in db.Products where product.brand_id == ID select product).ToList();

                List<Brand> brandID = new List<Brand>();
                brandID = (from brand in db.Brands where brand.id == ID select brand).ToList();

                ViewBag.listPro = Pro.Skip(14 * page).Take(pageSize);
                ViewBag.flat = flat;
                ViewBag.CountPro = Pro.Count();
                foreach (var item in brandID)
                {
                    ViewBag.Message =item.brand_name;

                }
            }

            //Sản phẩm đề cử//
            List<Product> Recent = new List<Product>();
            Recent = (from product in db.Products where product._new == 1 && product.star == 5 select product).ToList();
            ViewBag.listRecent = Recent.Take(8);

            //Giỏ Hàng//
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            ViewBag.Cart = giohang;

            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;

            return View();
        }

        [HttpGet]
        public ActionResult ProductDetail( int ID)
        {
            //Danh mục//
            List<Category> Categories = new List<Category>();
            Categories = (from cate in db.Categories select cate).ToList();
            ViewBag.listCategories = Categories;

            //Hãng//
            List<Brand> Brands = new List<Brand>();
            Brands = (from brand in db.Brands select brand).ToList();
            ViewBag.listBrands = Brands;

            //Sản phẩm đề cử//
            List<Product> Recent = new List<Product>();
            Recent = (from product in db.Products where product._new == 1 && product.star == 5 select product).ToList();
            ViewBag.listRecent = Recent.Take(8);

            //Chi tiết sản phẩm//
            List<Product> ProductDetail = new List<Product>();
            ProductDetail = (from product in db.Products where product.id == ID select product).ToList();
            ViewBag.ProductDetail = ProductDetail;

            List<Photo> ProductPhoto = new List<Photo>();
            ProductPhoto = (from photo in db.Photos where photo.product_id == ID select photo).ToList();
            ViewBag.ProductPhoto = ProductPhoto;

            //Giỏ Hàng//
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            ViewBag.Cart = giohang;

            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;
            //Đánh giá /

            List<View> view = new List<View>();
            view = (from v in db.Views where v.product_id == ID select v).ToList();
            ViewBag.listView = view;

            return View();
        }

       [HttpPost]
        public ActionResult ProductDetail(int ID, FormCollection collection)
        {
            //Danh mục//
            List<Category> Categories = new List<Category>();
            Categories = (from cate in db.Categories select cate).ToList();
            ViewBag.listCategories = Categories;

            //Hãng//
            List<Brand> Brands = new List<Brand>();
            Brands = (from brand in db.Brands select brand).ToList();
            ViewBag.listBrands = Brands;

            //Sản phẩm đề cử//
            List<Product> Recent = new List<Product>();
            Recent = (from product in db.Products where product._new == 1 && product.star == 5 select product).ToList();
            ViewBag.listRecent = Recent.Take(8);

            //Chi tiết sản phẩm//
            List<Product> ProductDetail = new List<Product>();
            ProductDetail = (from product in db.Products where product.id == ID select product).ToList();
            ViewBag.ProductDetail = ProductDetail;

            List<Photo> ProductPhoto = new List<Photo>();
            ProductPhoto = (from photo in db.Photos where photo.product_id == ID select photo).ToList();
            ViewBag.ProductPhoto = ProductPhoto;

            //Giỏ Hàng//
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            ViewBag.Cart = giohang;

            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;
            
            int Product_id = ID;
            string Name = collection["name"];
            int Vote = Convert.ToInt16(collection["vote"]);
            string Comment = collection["comment"];
            View newItem = new View()
            {
                name = Name,
                vote = Vote,
                comment = Comment,
                product_id = Product_id
            };

            db.Views.Add(newItem);
            db.SaveChanges();

            //Đánh giá /

            List<View> view = new List<View>();
            view = (from v in db.Views where v.product_id == ID select v).ToList();
            ViewBag.listView = view;


            return View();
        }
        public ActionResult About()
        {
            //Danh mục//
            List<Category> Categories = new List<Category>();
            Categories = (from cate in db.Categories select cate).ToList();
            ViewBag.listCategories = Categories;

            //Hãng//
            List<Brand> Brands = new List<Brand>();
            Brands = (from brand in db.Brands select brand).ToList();
            ViewBag.listBrands = Brands;

            ViewBag.Message = "Your application description page.";

            //Giỏ Hàng//
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            ViewBag.Cart = giohang;

            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;



            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            //Danh mục//
            List<Category> Categories = new List<Category>();
            Categories = (from cate in db.Categories select cate).ToList();
            ViewBag.listCategories = Categories;

            //Hãng//
            List<Brand> Brands = new List<Brand>();
            Brands = (from brand in db.Brands select brand).ToList();
            ViewBag.listBrands = Brands;

            ViewBag.Message = "Your contact page.";

            //Giỏ Hàng//
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            ViewBag.Cart = giohang;

            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;

            ViewBag.sussess = "";

            return View();
        }
        [HttpPost]
        public ActionResult Contact(FormCollection collection)
        {
            //Danh mục//
            List<Category> Categories = new List<Category>();
            Categories = (from cate in db.Categories select cate).ToList();
            ViewBag.listCategories = Categories;

            //Hãng//
            List<Brand> Brands = new List<Brand>();
            Brands = (from brand in db.Brands select brand).ToList();
            ViewBag.listBrands = Brands;

            ViewBag.Message = "Your contact page.";

            //Giỏ Hàng//
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            ViewBag.Cart = giohang;

            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;


            string Name = collection["name"];
            string Email = collection["email"];
            string Phone = collection["phone"];
            string Mess = collection["message"];

            Review newItem = new Review()
            {
                name = Name,
                email = Email,
                phone = Phone,
                dicription = Mess
            };
            db.Reviews.Add(newItem);
            db.SaveChanges();

            ViewBag.sussess = "Bạn đã gửi đánh giá";
            return View();
        }

        public ActionResult Blog()
        {
            //Danh mục//
            List<Category> Categories = new List<Category>();
            Categories = (from cate in db.Categories select cate).ToList();
            ViewBag.listCategories = Categories;

            //Hãng//
            List<Brand> Brands = new List<Brand>();
            Brands = (from brand in db.Brands select brand).ToList();
            ViewBag.listBrands = Brands;

            //Danh sách tin tức//
            List<Blog> blog = new List<Blog>();
            blog = (from bl in db.Blogs select bl).ToList();
            ViewBag.listBlog = blog;

            //Giỏ Hàng//
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            ViewBag.Cart = giohang;

            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;

            ViewBag.Message = "Tin tức công nghệ";
            return View();
        }

        public ActionResult BlogDetail(int ID)
        {
            //Danh mục//
            List<Category> Categories = new List<Category>();
            Categories = (from cate in db.Categories select cate).ToList();
            ViewBag.listCategories = Categories;

            //Hãng//
            List<Brand> Brands = new List<Brand>();
            Brands = (from brand in db.Brands select brand).ToList();
            ViewBag.listBrands = Brands;

            //Danh sách tin tức//
            List<Blog> blogs = new List<Blog>();
            blogs = (from bl in db.Blogs where bl.id != ID select bl).ToList();
            ViewBag.listBlogs = blogs.Take(3);

            //Chi tiết bản tin//
            List<Blog> blog = new List<Blog>();
            blog = (from bl in db.Blogs where bl.id == ID select bl).ToList();
            ViewBag.listBlog = blog;

            //Giỏ Hàng//
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            ViewBag.Cart = giohang;

            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;

            ViewBag.Message = "Tin tức công nghệ";
            return View();
        }

        public ActionResult Cart()
        {
            //Danh mục//
            List<Category> Categories = new List<Category>();
            Categories = (from cate in db.Categories select cate).ToList();
            ViewBag.listCategories = Categories;

            //Hãng//
            List<Brand> Brands = new List<Brand>();
            Brands = (from brand in db.Brands select brand).ToList();
            ViewBag.listBrands = Brands;

            //Giỏ Hàng//
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            ViewBag.Cart = giohang;

            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;

            ViewBag.Message = "Giỏ hàng của bạn";
            return View();
        }

        
    }

    internal class ProductView
    {
    }
}