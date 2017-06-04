using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_Boutique.Models.MyModels;
using System.Web.Script.Serialization;

namespace Online_Boutique.Controllers
{
    public class MyCartController : Controller
    {
        Online_BoutiqueEntities db = new Online_BoutiqueEntities();
        // GET: MyCart
        public ActionResult Index()
        {
            return View();
        }

        //lay gio hang
        public List<GioHang> LayGioHang()   //lay cai session ra
        {
            List<GioHang> listgh = Session["cart"] as List<GioHang>;

            // viet như thế này nếu session nó chưa tồn tại thì ép kiểu sẽ lỗi
            //còn viết như trên kia, nếu  session chưa tồn tại thì trả về null
            //List<GioHang> listgh = (List<GioHang>)Session["cart"]
            if (listgh == null)
            {
                //neu gio hang chua ton tai thi khoi tao list gio hang (session gio hang)
                listgh = new List<GioHang>();
                Session["cart"] = listgh;
            }
            return listgh; 
        }


        //thêm giỏ hàng
        public ActionResult ThemGioHang(int masp,string url)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.masp == masp);
            if(sp==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lay ra session gio hang
            int contcart = 0;
            List<GioHang> listgh = LayGioHang();
            foreach(GioHang item in listgh)
            {
                contcart += item.slmuasp;
                
            }
            Session["ls"] = contcart + 1;
            GioHang sp2 = listgh.Find(x => x.masp == masp);
            if(sp2==null)
            {
                sp2 = new GioHang(masp);
                listgh.Add(sp2);
                return Redirect(Request.UrlReferrer.ToString());
                //bản chất của return Redirect(url); vẫn giống câu lệnh trên và vẫn bị load lại trang
            }
            else
            {
                sp2.slmuasp++;
                return Redirect(Request.UrlReferrer.ToString());
            }
        }

        // cập nhật giỏ hàng
        public JsonResult CapNhatGioHang(string cartModel)
        {
            var cart = new JavaScriptSerializer().Deserialize<List<GioHang>>(cartModel);
            var ls = Session["cart"] as List<GioHang>;
            foreach(var item in ls)
            {
               var sp = cart.SingleOrDefault(n=>n.masp == item.masp);
               if(sp!=null)
                {
                    item.slmuasp = sp.slmuasp;
                }
            }
            Session["cart"] = ls;
            return Json(new { status = true });
        }
        

        //xoa gio hang
        public ActionResult XoaGioHang(int masp)
        {
            List<GioHang> listgh = LayGioHang();
            GioHang sp = listgh.SingleOrDefault(x => x.masp == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                listgh.Remove(sp);
            }
            if(listgh.Count==0)
            {
                return RedirectToAction("Index", "MyHome");
            }
            return RedirectToAction("GioHang");

        }

        //xay dung trang gio hang
        public ActionResult GioHang()
        {
            if (Session["cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                List<GioHang> listgh = LayGioHang();
                ViewBag.tongtien = TongThanhTien();
                return View(listgh);
            }
        }


        // tinh tong sl mua va thanh tien

        public int TongSLMua()
        {
            int tongslmua = 0;
            List<GioHang> listgh = Session["cart"] as List<GioHang>;
            if (listgh != null)
                tongslmua = listgh.Sum(x => x.slmuasp);
            return tongslmua;
        }
        public ActionResult XoaTatCaGioHang()
        {
            List<GioHang> listgh = Session["cart"] as List<GioHang>;
            if(listgh != null)
            {
                Session.Remove("cart");
                
            }
            return RedirectToAction("Index", "MyHome");
        }
        public Nullable<int> TongThanhTien()
        {
            Nullable<int> tongthanhtien = 0;
            List<GioHang> listgh = Session["cart"] as List<GioHang>;
            if (listgh != null)
                tongthanhtien = listgh.Sum(x => x.thanhtien);
          
            return tongthanhtien;
        }

        
    }
}