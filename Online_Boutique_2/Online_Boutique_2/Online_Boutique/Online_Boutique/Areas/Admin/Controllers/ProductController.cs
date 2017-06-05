using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_Boutique.Areas.Admin.Models.Entites;
using System.IO;
using PagedList;
using PagedList.Mvc;

namespace Online_Boutique.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        OnlineB db = new OnlineB();
        public ActionResult Index(int? page,string key= "")
        {
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            
            
            return View(db.SanPhams.SqlQuery("SELECT * FROM SanPham WHERE tensp LIKE '%" + key + "%'").ToPagedList(pageNumber, pageSize));
            
        }
        [HttpGet]
        public ActionResult Create() 
        {
            ViewBag.maloaisp = new SelectList(db.LoaiSanPhams.ToList().OrderBy(n => n.tenloaisp), "maloaisp", "tenloaisp");
            return View();
        }
        public ActionResult timkiem(string key)
        {
            List<SanPham> ls = db.SanPhams.SqlQuery("SELECT * FROM SanPham WHERE tensp LIKE " + key + "").ToList();
            return View(ls);
        }
        [HttpPost]
        public ActionResult CreateAction(SanPham product, HttpPostedFileBase fileUpload)
        {
            //Lưu tên file
            if (fileUpload != null)
            {
                var fileName = Path.GetFileName(fileUpload.FileName);
                //Lưu đường dẫn của file
                var path = Path.Combine(Server.MapPath("~/HinhAnh/Nu"), fileName);
                //Kiểm tra hình ảnh đã tồn tại chưa
                    fileUpload.SaveAs(path);
                    DateTime date = DateTime.Now;
                    product.ngaynhapsp = date;
                    product.anhsp = fileUpload.FileName;
                    db.SanPhams.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Chọn ảnh";                
                return RedirectToAction("Create");
            }
        }
        

        public ActionResult Delete(int id = 0)
        {
            //SanPham product = db.SanPhams.SingleOrDefault(n => n.masp == id);
            SanPham product = db.SanPhams.SqlQuery("SELECT * FROM SanPham WHERE masp = "+id+"").Single();
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                db.SanPhams.Remove(product);
                db.SaveChanges();
                ViewBag.OK = "OK";
                return RedirectToAction("index");
            }
        }
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            SanPham product = db.SanPhams.SingleOrDefault(n => n.masp == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                ViewBag.maloaisp = new SelectList(db.LoaiSanPhams.OrderBy(n => n.maloaisp), "maloaisp", "tenloaisp", product.maloaisp);
                return View(product);
            }
        }
        [HttpPost]
        public ActionResult EditAction(SanPham product, HttpPostedFileBase fileUpload, string img)
        {
            if (fileUpload != null)
            {
                //Thêm vào cơ sở dữ liệu
                //Lưu tên file
                var fileName = Path.GetFileName(fileUpload.FileName);
                //Lưu đường dẫn của file
                var path = Path.Combine(Server.MapPath("~/HinhAnh/Nu"), fileName);
                //Kiểm tra hình ảnh đã tồn tại chưa
                fileUpload.SaveAs(path);
                product.anhsp = fileUpload.FileName;
            }
            else
            {
                product.anhsp = img;
            }
            db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
