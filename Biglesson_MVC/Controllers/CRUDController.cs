using Biglesson_MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Biglesson_MVC.Controllers
{
    public class CRUDController : Controller
    {
       private Model_HiTech db = new Model_HiTech();
        // GET: CRUD
        public ActionResult Index()
        {
            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;
            return View();
        }
        
        public ActionResult CateCreate()
        {
            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;
            List<Category> Cate = new List<Category>();
            Cate = (from cate in db.Categories select cate).ToList();
            ViewBag.listCate = Cate;

            return View();
        }
        [HttpGet]
        public ActionResult CateEdit(int ID)
        {
            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;
            List<Category> Cate = new List<Category>();
            Cate = (from cate in db.Categories where cate.id == ID select cate).ToList();
            ViewBag.listCateID = Cate;

            return View();
        }
        [HttpPost]
        public ActionResult CateEdit(int ID, FormCollection collection)
        {
            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;
            string name_Cate = collection["name_Cate"];

            Category cate = db.Categories.SingleOrDefault(c => c.id == ID);

            cate.name = name_Cate;

            UpdateModel(cate);
            db.SaveChanges();

            return RedirectToAction("Category", "Admin");
        }
        public RedirectToRouteResult AddCate()
        {
            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;
            string name_Cate = Request.Form["name_Cate"];

            Category newItem = new Category()
            {
                name = name_Cate
            };

                db.Categories.Add(newItem);
                db.SaveChanges();
            

            return RedirectToAction("Category", "Admin");
        }

        
        public RedirectToRouteResult DeleteCate( int ID)
        {
            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;
            Category cate = db.Categories.SingleOrDefault(c => c.id == ID);

            if (cate == null)
            {
                return null;
            }
            else
            {
                db.Categories.Remove(cate);
                db.SaveChanges();

                return RedirectToAction("Category", "Admin");
            }
            
        }

        //Product//
        [HttpGet]
        public ActionResult ProductCreate()
        {
            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;
            ViewBag.listCate = (from c in db.Categories select c).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult ProductCreate(FormCollection collection)
        {
            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;
            string nameProduct = collection["nameProduct"];

            int cateProduct = Convert.ToInt16(collection["cateProduct"]);

            int priceProduct = Convert.ToInt16(collection["priceProduct"]);

            string imgProduct = collection["imgProduct"];

            string dicriptionProduct = collection["dicriptionProduct"];

            int saleProduct = Convert.ToInt16(collection["saleProduct"]);

            int starProduct = Convert.ToInt16(collection["starProduct"]);

            int quantityProduct = Convert.ToInt16(collection["quantityProduct"]);

            Product newItem = new Product()
            {
                name_product = nameProduct,
                price = priceProduct,
                cate_id = cateProduct,
                image = imgProduct,
                sale = saleProduct,
                star = starProduct,
                quantity = quantityProduct,
                dicription = dicriptionProduct
            };

            db.Products.Add(newItem);
            db.SaveChanges(); 

            return RedirectToAction("Products","Admin");
        }

        [HttpGet]
        public ActionResult ProductEdit( int ID)
        {
            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;

            ViewBag.listCate = (from c in db.Categories select c).ToList();
            Product pro = db.Products.SingleOrDefault(x => x.id == ID);
            ViewBag.listProID = pro;
            return View();
        }
        [HttpPost]
        public ActionResult ProductEdit(int ID, FormCollection collection)
        {
            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;

            ViewBag.listCate = (from c in db.Categories select c).ToList();

            string nameProduct = collection["nameProduct"];

            int cateProduct = Convert.ToInt16(collection["cateProduct"]);

            int priceProduct = Convert.ToInt16(collection["priceProduct"]);

            string imgProduct = collection["imgProduct"];

            string dicriptionProduct = collection["dicriptionProduct"];

            int saleProduct = Convert.ToInt16(collection["saleProduct"]);

            int starProduct = Convert.ToInt16(collection["starProduct"]);

            int quantityProduct = Convert.ToInt16(collection["quantityProduct"]);

            Product pro = db.Products.SingleOrDefault(x => x.id == ID);

            if (pro != null)
            {
                pro.name_product = nameProduct;
                pro.price = priceProduct;
                pro.cate_id = cateProduct;
                pro.image = imgProduct;
                pro.sale = saleProduct;
                pro.star = starProduct;
                pro.quantity = quantityProduct;
                pro.dicription = dicriptionProduct;
            };
            UpdateModel(pro);
            db.SaveChanges();

            return RedirectToAction("Products", "Admin");
        }

        public RedirectToRouteResult DeleteProduct(int ID)
        {
            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;

            Product pro = db.Products.SingleOrDefault(p => p.id == ID);

            if (pro == null)
            {
                return null;
            }
            else
            {
                db.Products.Remove(pro);
                db.SaveChanges();

                return RedirectToAction("Products", "Admin");
            }

        }
        //Blog //

        [HttpGet]
        public ActionResult BlogCreate()
        {
            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;
            return View();
        }

        /* public ActionResult BlogCreate(HttpPostedFileBase nameFile , FormCollection collection )
         {

             try
             {
                 if (nameFile.ContentLength >0)
                 {
                     var filename = Path.GetFileName(nameFile.FileName);
                     var path = Path.Combine(Server.MapPath("~/Content/images"), filename);

                     nameFile.SaveAs(path);

                     string title_Blog = collection["title_Blog"];

                     string dicription_Blog = collection["dicription_Blog"].ToString();

                     string image = collection["img_Blog"];
                     string author_Blog = collection["author_Blog"];

                     Blog newItem = new Blog()
                     {
                         title = title_Blog,
                         dicription = dicription_Blog,
                         author = author_Blog,
                         image = filename
                     };

                     db.Blogs.Add(newItem);
                     db.SaveChanges();
                 }
                 else
                 {

                     ViewBag.error = "Bạn cần chọn file!";
                 }
             }
             catch (Exception e)
             {

                 ViewBag.error = "Có lỗi xảy ra với thao tác" + e;
             }

             return RedirectToAction("Blogs", "Admin");
         }
         */
        [HttpPost]
        public ActionResult BlogCreate( FormCollection collection)
        {
            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;
            string title_Blog = collection["title_Blog"];

                    string dicription_Blog = collection["dicription_Blog"];

                    string image = " aa";
                    string author_Blog = collection["author_Blog"];

                    Blog newItem = new Blog()
                    {
                        title = title_Blog,
                        dicription = dicription_Blog,
                        author = author_Blog,
                        image = image
                    };

                    db.Blogs.Add(newItem);
                    db.SaveChanges();

            return RedirectToAction("Blogs", "Admin");
        }


        [HttpGet]
        public ActionResult BlogEdit(int ID)
        {
            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;
            List<Blog> blog = new List<Blog>();
            blog = (from b in db.Blogs where b.id == ID select b).ToList();
            ViewBag.listBlog = blog;

            return View();
        }



        ///Úp ảnh không thành công//
        /* public ActionResult BlogEdit(int ID , HttpPostedFileBase nameFile, FormCollection collection)
         {
             try
             {
                 if (nameFile.ContentLength > 0)
                 {
                     var filename = Path.GetFileName(nameFile.FileName);
                     var path = Path.Combine(Server.MapPath("~/Content/images"), filename);

                     nameFile.SaveAs(path);

                     Blog blog = db.Blogs.SingleOrDefault(x => x.id == ID);

                     string title_Blog = collection["title_Blog"];

                     string dicription_Blog = collection["dicription_Blog"];

                     string author_Blog = collection["author_Blog"];

                     if (blog != null)
                     {

                         blog.title = title_Blog;
                         blog.dicription = dicription_Blog;
                         blog.author = author_Blog;
                         blog.image = filename;

                         UpdateModel(blog);
                         db.SaveChanges();
                     }
                 }
                 else
                 {
                     ViewBag.error = "Bạn chưa chọn file";
                 }
             }
             catch (Exception e)
             {
                 ViewBag.error = "Có lỗi xảy ra với thao tác" + e;

             }
             return RedirectToAction("Blogs", "Admin");
         }*/

        //KHÔNG UP ẢNH//
        [HttpPost]
        public RedirectToRouteResult BlogEdit(int ID, FormCollection collection)
        {
            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;


            Blog blog = db.Blogs.SingleOrDefault(x => x.id == ID);

                    string title_Blog = collection["title_Blog"];

                    string dicription_Blog = Convert.ToString(collection["dicription_Blog"]);

                    string author_Blog = collection["author_Blog"];

                    string img_Blog = "aa";

            if (blog != null)
                    {

                        blog.title = title_Blog;
                        blog.dicription = dicription_Blog;
                        blog.author = author_Blog;
                        blog.image = img_Blog;

                        UpdateModel(blog);
                        db.SaveChanges();
                    }
                
            
            return RedirectToAction("Blogs", "Admin");
        }

        public RedirectToRouteResult DeleteBlog(int ID)
        {


            Blog blog = db.Blogs.SingleOrDefault(x => x.id == ID);

            if (blog == null)
            {
                return null;
            }
            else
            {
                db.Blogs.Remove(blog);
                db.SaveChanges();

                return RedirectToAction("Blogs", "Admin");
            }

        }
        //Người dùng//
        public ActionResult UserCreate()
        {
            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;
            return View();
        }

        public ActionResult AddUser()
        {
            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;
            string userName_User = Request.Form["userName_User"];

            string pass_User = Request.Form["pass_User"];

            string phone_User = Request.Form["phone_User"];

            string name_User = Request.Form["name_User"];

            string email_User = Request.Form["email_User"];

            string address_User = Request.Form["address_User"];

            User newItem = new User()
            {
                userName = userName_User,
                name = name_User,
                pass = pass_User,
                phone = phone_User,
                email = email_User,
                address = address_User,
                avartar = null,
                role_id = null

            };

            db.Users.Add(newItem);
            db.SaveChanges();

            return RedirectToAction("Users", "Admin");
        }
        [HttpGet]
        public ActionResult UserEdit(int ID)
        {
            //Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;
            List<User> users = new List<User>();
            users = (from u in db.Users where u.id == ID select u).ToList();
            ViewBag.listUser = users;

            return View();
        }
        [HttpPost]
        public ActionResult UserEdit(int ID,FormCollection collection)
        {//Session User//
            List<UserItem> UserSession = Session["UserSession"] as List<UserItem>;
            ViewBag.listUserSession = UserSession;


            return RedirectToAction("Users", "Admin");
        }

        public RedirectToRouteResult DeleteUser(int ID)
        {

            User user = db.Users.SingleOrDefault(x => x.id == ID);

            if (user == null || -+user.role_id != 1)
            {
                return null;
            }
            else
            {
                db.Users.Remove(user);
                db.SaveChanges();

                return RedirectToAction("Users", "Admin");
            }

        }
    }
}