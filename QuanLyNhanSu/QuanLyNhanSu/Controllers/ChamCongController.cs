using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyNhanSu.Models;
using QuanLyNhanSu.DAL;
using PagedList;
using PagedList.Mvc;

namespace QuanLyNhanSu.Controllers
{
    public class ChamCongController : Controller
    {
        QLNHANSUContext db = new QLNHANSUContext();
        // GET: ChamCong
        public ActionResult ChamCong(string chamcong, string ngay, int? trang)
        {
            if (chamcong == "dacham")
            {
                List<CHAMCONG> lis = new List<CHAMCONG>();
                foreach (var item in db.ChamCongs.ToList())
                {
                    var ngaycham = item.Ngay.Date.ToString("MM/dd/yyyy");
                    Console.WriteLine(ngaycham);
                    if (ngaycham == ngay && item.TrangThai != null)
                    {
                        lis.Add(item);
                    }
                }
                return View(lis.ToPagedList(trang ?? 1, 10));
            }
            else if (chamcong == "chuacham")
            {
                List<CHAMCONG> lis = new List<CHAMCONG>();
                foreach (var item in db.ChamCongs.ToList())
                {
                    var ngaycham = item.Ngay.Date.ToString("MM/dd/yyyy");
                    Console.WriteLine(ngaycham);
                    if (ngaycham == ngay && item.TrangThai == null)
                    {
                        lis.Add(item);
                    }
                }
                return View(lis.ToPagedList(trang ?? 1, 10));
            }
            else if (chamcong == "tatca")
            {
                List<CHAMCONG> lischamcong = new List<CHAMCONG>();
                foreach (var item in db.ChamCongs.ToList())
                {
                    var _ngay = item.Ngay.Date.ToString("MM/dd/yyyy");
                    if (_ngay == ngay)
                    {
                        lischamcong.Add(item);
                    }
                }
                return View(lischamcong.OrderByDescending(m =>m.IDChamCong).ToPagedList(trang ?? 1, 10));
            }
            var bangchamcong = db.ChamCongs.OrderByDescending(m => m.IDChamCong).ToPagedList(trang ?? 1, 3);
            return View(bangchamcong);
        }
        public ActionResult TaoMoi(string ngay)
        {
            int a = 0;
            foreach (var item in db.ChamCongs.ToList())
            {
                var _ngay = item.Ngay.Date.ToString("MM/dd/yyyy");
                if (_ngay == DateTime.Today.ToString("MM/dd/yyyy"))
                {
                    a = 1;
                    //ViewData["Thongbao_cc"] = "Danh sách chấm công hôm nay đã được tạo";
                    break;
                }
            }
            if (a == 0)
            {
                foreach (var item in db.NhanViens.ToList())
                {
                    CHAMCONG c = new CHAMCONG();
                    c.MaNhanVien = item.MaNhanVien;
                    c.Ngay = DateTime.Today;
                    db.ChamCongs.Add(c);
                }
                db.SaveChanges();
                return RedirectToAction("ChamCong");
            }
            return RedirectToAction("ChamCong", "ChamCong");
        }
        public ActionResult dunggio(int id)
        {
            var nhanvien = db.ChamCongs.Where(m => m.IDChamCong == id).FirstOrDefault();
            nhanvien.TrangThai = "Đúng giờ";
            db.Entry(nhanvien).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ChamCong");
        }
        public ActionResult ditre(int id)
        {
            var nhanvien = db.ChamCongs.Where(m => m.IDChamCong == id).FirstOrDefault();
            nhanvien.TrangThai = "Đi trễ";
            db.Entry(nhanvien).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ChamCong");
        }
        public ActionResult nghi(int id)
        {
            var nhanvien = db.ChamCongs.Where(m => m.IDChamCong == id).FirstOrDefault();
            nhanvien.TrangThai = "Nghỉ";
            db.Entry(nhanvien).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ChamCong");
        }
        public ActionResult BangChamCong_NhanVien(string ngay)
        {
            string id = Session["MaNhanVien"].ToString();
            if (ngay != "" && ngay != null)
            {
                List<CHAMCONG> lis = new List<CHAMCONG>();
                foreach (var item in db.ChamCongs.ToList())
                {
                    if (ngay == item.Ngay.ToString("MM/dd/yyyy") && item.MaNhanVien == id)
                    {
                        lis.Add(item);
                    }
                }
                return View(lis);
            }
            return View(db.ChamCongs.Where(m => m.MaNhanVien == id).ToList());
        }
    }
}