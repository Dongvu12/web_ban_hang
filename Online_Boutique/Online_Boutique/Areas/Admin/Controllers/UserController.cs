using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_Boutique.Areas.Admin.Models.Entites;
using PagedList;
using PagedList.Mvc;

namespace Online_Boutique.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        OnlineB db = new OnlineB();
        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            return View(db.KhachHangs.SqlQuery("SELECT * FROM KhachHang").ToPagedList(pageNumber, pageSize));

        }
        public ActionResult Edit(int id)
        {
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.id == id);
            if(kh==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                return View(kh);
            }
        }
        public ActionResult EditAction(KhachHang kh)
        {
            db.Entry(kh).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}