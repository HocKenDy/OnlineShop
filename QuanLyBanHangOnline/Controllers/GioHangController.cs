using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanHangOnline.Models;

namespace QuanLyBanHangOnline.Controllers
{
    public class GioHangController : Controller
    {
        //
        // GET: /GioHang/
        QuanLyBanHangOnlineEntities db = new QuanLyBanHangOnlineEntities();
        public List<ItemGioHang> LayGioHang()
        {
            List<ItemGioHang> listGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (listGioHang == null)
            {
                listGioHang = new List<ItemGioHang>();
                Session["GioHang"] = listGioHang;
            }
            return listGioHang;

        }
        public ActionResult ThemGioHang(int maSP, string strURL)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == maSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<ItemGioHang> listGioHang = LayGioHang();
            ItemGioHang spCheck = listGioHang.SingleOrDefault(x => x.MaSP == maSP);
            if (spCheck == null)
            {
                spCheck = new ItemGioHang(maSP);
             
               spCheck.SoLuong = 1;
               
            }
            else
            {
                if (sp.SoLuongTon-1 < spCheck.SoLuong)
                {
                    return View("ThongBao");
                }
                spCheck.SoLuong++;
                spCheck.ThanhTien = spCheck.SoLuong * spCheck.DonGia;
                return Redirect(strURL);
            }
            listGioHang.Add(spCheck);

            return Redirect(strURL);
        }
        public int TinhTongSoLuong()
        {
            List<ItemGioHang> listGH = Session["GioHang"] as List<ItemGioHang>;
            if (listGH == null)
            {
                return 0;
            }
            return listGH.Sum(x => x.SoLuong);
        }
        public decimal TinhTongTien()
        {
            List<ItemGioHang> listGH = Session["GioHang"] as List<ItemGioHang>;
            if (listGH == null)
            {
                return 0;
            }
            return listGH.Sum(x => x.ThanhTien);
        }
        public ActionResult XemGioHang()
        {
            List<ItemGioHang> listGioHang = LayGioHang();
            return View(listGioHang);
        }
        public ActionResult GioHangPartial()
        {
            if (TinhTongSoLuong() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();
            return PartialView();
        }
        public ActionResult SuaGioHang(int maSP)
        {
            if(Session["GioHang"]==null)
            {
                return RedirectToAction("Index", "Home");
            }
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == maSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<ItemGioHang> listGH = LayGioHang();
            ItemGioHang spCheck = listGH.SingleOrDefault(x => x.MaSP == maSP);
            if(spCheck==null)
            {
                return RedirectToAction("Index", "Home");
            }
           //Hiển thị toàn bộ giỏ hàng
            ViewBag.GioHang = listGH;
            return View(spCheck);
        }
        [HttpPost]
        public ActionResult CapNhatGioHang(ItemGioHang itemGH)
        {
            SanPham spCheck = db.SanPhams.SingleOrDefault(x => x.MaSP == itemGH.MaSP);
            if(spCheck.SoLuongTon<itemGH.SoLuong)
            {
                return View("ThongBao");
            }
            List<ItemGioHang> listGH = LayGioHang();
            ItemGioHang itemGHUpdates = listGH.Find(x => x.MaSP == itemGH.MaSP);
            itemGHUpdates.SoLuong = itemGH.SoLuong;
            itemGHUpdates.ThanhTien = itemGHUpdates.SoLuong * itemGHUpdates.DonGia;
            return RedirectToAction("XemGioHang");
            
        }
    }
}