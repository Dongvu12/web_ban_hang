using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_Boutique.Areas.Admin.Models.Entites;

namespace Online_Boutique.Areas.Admin.Controllers
{
    
    public class HomeController : Controller
    {
        // GET: Admin/Admin
        OnlineB db = new OnlineB();
        public ActionResult Index()
        {
            return View();
        }
        public bool CheckLogin(string username, string password)
        {
            ad admin = db.ads.SingleOrDefault(n => n.username == username && n.password == password );
            if (admin == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        [HttpPost]
        public ActionResult Login()
        {
            string username = Request.Form["txt_user"];
            string password = Request.Form["txt_password"];
            if (CheckLogin(username, password))
            {
                Session["username"] = username;
                return View();
            }
            else
            {
                ViewBag.Err = "Lỗi đăng nhập";
                return View("Index");
            }
        }
    }
}