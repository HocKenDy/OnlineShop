using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanHangOnline.Models;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;
namespace QuanLyBanHangOnline.Controllers
   
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        QuanLyBanHangOnlineEntities db = new QuanLyBanHangOnlineEntities();
        public ActionResult Index()
        {
            var dsDT = db.SanPhams.Where(x => x.MaLoaiSP == 1 && x.Moi == 1 && x.DaXoa == false);
            ViewBag.listDT = dsDT;
            var dsMTB = db.SanPhams.Where(x => x.MaLoaiSP == 2 && x.Moi == 1 && x.DaXoa == false);
            ViewBag.listMTB = dsMTB;
            var dsLT = db.SanPhams.Where(x => x.MaLoaiSP == 3 && x.Moi == 1 && x.DaXoa == false);
            ViewBag.listLT = dsLT;

            return View();
        }
        
        public ActionResult MenuPartial()
        {
            var dsSP = db.SanPhams;
            return PartialView(dsSP);
        }
        public ActionResult MenuChiTietPartial()
        {
            var dsSp = db.SanPhams;
            return PartialView(dsSp);
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(ThanhVien tv)
        {
            if(this.IsCaptchaValid("Captcha is not valid"))
            {
                ViewBag.ThongBao = "Thêm thành công";
                return View();
            }
            ViewBag.ThongBao = "Sai ma captcha";
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap( FormCollection f)
        {
            string TaiKhoan = f["txtDangNhap"].ToString();
            string MatKhau = f["txtPassword"].ToString();
            ThanhVien tv = db.ThanhViens.SingleOrDefault(x => x.TaiKhoan == TaiKhoan && x.MatKhau == MatKhau);
            if(tv!=null)
            {
                Session["TaiKhoan"]=tv;
                return Content("<script> window.location.reload();</script>");
            }

            return Content("Tài khoản or mật khẩu không chính xác");
            }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult DangKy1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy1(ThanhVien tv)
        {
            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                ViewBag.ThongBao = "Thêm thành công";
                return View();
            }
            ViewBag.ThongBao = "Sai ma captcha";
            return View();
        }
	}
}