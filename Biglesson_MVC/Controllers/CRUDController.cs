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
       private Modelhitech db = new Modelhitech();
        // GET: CRUD
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult CateCreate()
        {
            List<Category> Cate = new List<Category>();
            Cate = (from cate in db.Categories select cate).ToList();
            ViewBag.listCate = Cate;

            return View();
        }
        [HttpGet]
        public ActionResult CateEdit(int ID)
        {
            List<Category> Cate = new List<Category>();
            Cate = (from cate in db.Categories where cate.id == ID select cate).ToList();
            ViewBag.listCateID = Cate;

            return View();
        }
        [HttpPost]
        public ActionResult CateEdit(int ID, FormCollection collection)
        {
            string name_Cate = collection["name_Cate"];

            Category cate = db.Categories.SingleOrDefault(c => c.id == ID);

            cate.name = name_Cate;

            UpdateModel(cate);
            db.SaveChanges();

            return RedirectToAction("Category", "Admin");
        }
        public RedirectToRouteResult AddCate()
        {
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
        public ActionResult ProductCreate()
        {
            List<Category> Cate = new List<Category>();
            Cate = (from cate in db.Categories select cate).ToList();
            ViewBag.listCate = Cate;

            return View();
        }
        [HttpGet]
        public ActionResult ProductEdit( int ID)
        {
            List<Category> Cate = new List<Category>();
            Cate = (from cate in db.Categories select cate).ToList();
            ViewBag.listCate = Cate;

            //List<Product> Pro = new List<Product>();
             var Pro = (from p in db.Products where p.id == ID select p).ToList();
            ViewBag.listProID = Pro;

            return View();
        }
        [HttpPost]
        public ActionResult ProductEdit(int ID, FormCollection collection)
        {
            

            return View();
        }

        public RedirectToRouteResult DeleteProduct(int ID)
        {

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
        public ActionResult BlogCreate()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult AddBlog(FormCollection collection , HttpPostedFileBase img_Blog)
        {
            string title_Blog = collection["title_Blog"].ToString();

            string dicription_Blog = collection["dicription_Blog"].ToString();

            string image = img_Blog.ToString();
            if (img_Blog != null)
            {
                image = Path.GetFileName(img_Blog.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/images"), image);
                img_Blog.SaveAs(path);
            }

            string author_Blog = collection["author_Blog"].ToString();

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
            List<Blog> blog = new List<Blog>();
            blog = (from b in db.Blogs where b.id == ID select b).ToList();
            ViewBag.listBlog = blog;

            return View();
        }

        [HttpPost]
        public ActionResult BlogEdit(int ID , FormCollection collection)
        {
            List<Blog> blog = new List<Blog>();
            blog = (from b in db.Blogs where b.id == ID select b).ToList();
            ViewBag.listBlog = blog;

            return View();
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
            return View();
        }

        public ActionResult AddUser()
        {
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
            List<User> users = new List<User>();
            users = (from u in db.Users where u.id == ID select u).ToList();
            ViewBag.listUser = users;

            return View();
        }
        [HttpPost]
        public ActionResult UserEdit(int ID,FormCollection collection)
        {


            return RedirectToAction("Users", "Admin");
        }

        public RedirectToRouteResult DeleteUser(int ID)
        {

            User user = db.Users.SingleOrDefault(x => x.id == ID);

            if (user == null)
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