using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_Boutique.Models.MyModels;

namespace Online_Boutique.Controllers
{
    public class MyOrderController : Controller
    {
        // GET: MyOrder
        Online_BoutiqueEntities db = new Online_BoutiqueEntities();
        public ActionResult Index()
        {
            string namecus = Request.Form["txtHoTen"];
            string phone = Request.Form["txtSoDienThoai"];
            DateTime date = DateTime.Now;
            string addcus = Request.Form["txtDiaChi"];
            string note = Request.Form["txtGhiChu"];
            string email = Request.Form["txtEmail"];
            string status = "Chưa ship";
            List<GioHang> listgh = Session["cart"] as List<GioHang>;
            donhang order = new donhang();
            foreach (var item in listgh)
            {
                order.masp = item.masp;
                order.tenkh = namecus;
                order.sdt = phone;
                order.diachi = addcus;
                order.ghichu = note;
                order.soluong = item.slmuasp;
                order.create_time = date;
                order.trangthai = status;
                order.email = email;
                db.donhangs.Add(order);
                db.SaveChanges();
            }
            return View();
            
        }
    }
}