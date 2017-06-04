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
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        OnlineB db = new OnlineB();
        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            return View(db.LoaiSanPhams.SqlQuery("SELECT * FROM LoaiSanPham").ToPagedList(pageNumber, pageSize));

        }
        public ActionResult Create()
        {   

            return View();
        }
        [HttpPost]
        public ActionResult CreateAction(LoaiSanPham lsp,string gt)
        {
            lsp.gioitinh = gt;
            db.LoaiSanPhams.Add(lsp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            LoaiSanPham lsp = db.LoaiSanPhams.SingleOrDefault(n => n.maloaisp == id);
            if(lsp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                return View(lsp);
            }
        }
        [HttpPost]
        public ActionResult EditAction(LoaiSanPham lsp,string gt)
        {
            lsp.gioitinh = gt;
            db.Entry(lsp).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            LoaiSanPham lsp = db.LoaiSanPhams.SingleOrDefault(n => n.maloaisp == id);
            if (lsp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                db.LoaiSanPhams.Remove(lsp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}