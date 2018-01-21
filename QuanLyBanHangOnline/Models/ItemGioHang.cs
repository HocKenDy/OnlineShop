using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyBanHangOnline.Models
{
    public class ItemGioHang
    {
        public string TenSP { get; set; }
        public int MaSP { get; set;}
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
        public string HinhAnh { get; set; }
        public ItemGioHang() { }
        public ItemGioHang(int maSP, int SL)
        {
            
            using(QuanLyBanHangOnlineEntities db=new QuanLyBanHangOnlineEntities())
            {
                this.MaSP = maSP;
               SanPham sp = db.SanPhams.Single(x => x.MaSP == maSP);
               this.TenSP = sp.TenSP;
               this.DonGia = sp.DonGia.Value;
               this.HinhAnh = sp.HinhAnh;
               this.SoLuong = SL;
               this.ThanhTien = this.SoLuong * this.DonGia;
            }
        }
        public ItemGioHang(int maSP)
        {

            using (QuanLyBanHangOnlineEntities db = new QuanLyBanHangOnlineEntities())
            {
                this.MaSP = maSP;
                SanPham sp = db.SanPhams.Single(x => x.MaSP == maSP);
                this.TenSP = sp.TenSP;
                this.DonGia = sp.DonGia.Value;
                this.HinhAnh = sp.HinhAnh;
                this.SoLuong++;
                this.ThanhTien = this.SoLuong * this.DonGia;
               
            }
        }
       

    }
}