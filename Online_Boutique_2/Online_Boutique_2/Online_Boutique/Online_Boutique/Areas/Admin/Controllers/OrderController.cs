using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Online_Boutique.Areas.Admin.Models.Entites;
using System.Web.Script.Serialization;

namespace Online_Boutique.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        OnlineB db = new OnlineB();
        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            return View(db.donhangs.SqlQuery("SELECT * FROM donhang Where trangthai = 0 ").ToPagedList(pageNumber, pageSize));

        }
        public ActionResult processing(int? page)
        {
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            return View(db.donhangs.SqlQuery("SELECT * FROM donhang Where trangthai = 1 ").ToPagedList(pageNumber, pageSize));
        }
        public ActionResult processed(int? page, int? masp)
        {
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            return View(db.donhangs.SqlQuery("SELECT * FROM donhang Where trangthai = 2 ").ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Edit(int id)
        {
            donhang dh = db.donhangs.SingleOrDefault(n => n.id_hd == id);
            if(dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {           
                return View(dh);
            }
        }
        public JsonResult handle(int id,int status)
        {
            donhang dh = db.donhangs.SingleOrDefault(n => n.id_hd == id);
            if (dh == null)
            {
                return Json(false);
            }
            else
            {
                if (status == 1)
                {
                    dh.trangthai = status;
                    DateTime date = DateTime.Now;
                    dh.update_time = date;
                    db.Entry(dh).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.donhangs.Remove(dh);
                    db.SaveChanges();
                }
                return Json(true);
            }
        }
        public JsonResult handle2(int id, int status)
        {
            donhang dh = db.donhangs.SingleOrDefault(n => n.id_hd == id);
            if (dh == null)
            {
                return Json(false);
            }
            else
            {
                if (status == 2)
                {
                    dh.trangthai = status;
                    DateTime date = DateTime.Now;
                    dh.update_time = date;
                    db.Entry(dh).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.donhangs.Remove(dh);
                    db.SaveChanges();
                }
                return Json(true);
            }
        }
        public ActionResult EditAction(donhang dh)
        {
            db.Entry(dh).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}