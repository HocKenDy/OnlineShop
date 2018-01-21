using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyBanHangOnline.Models;

namespace QuanLyBanHangOnline.Controllers
{
    public class SanPhamController : Controller
    {
        //
        // GET: /SanPham/
        QuanLyBanHangOnlineEntities db = new QuanLyBanHangOnlineEntities();
        [ChildActionOnly]
        public PartialViewResult SanPhamStyle1Partial()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public PartialViewResult SanPhamStyle2Partial()
        {
            return PartialView();
        }
        public ActionResult XemChiTiet(int? id, string tensp)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == id && x.DaXoa==false);
            if(sp==null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }
        public ActionResult PhanLoaiThuongHieu(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sp = db.SanPhams.Where(x => x.MaNSX == id && x.DaXoa == false);
            return View(sp);

        }
	}
}