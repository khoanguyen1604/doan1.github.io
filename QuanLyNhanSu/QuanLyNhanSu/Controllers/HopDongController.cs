using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyNhanSu.Models;
using QuanLyNhanSu.DAL;

namespace QuanLyNhanSu.Controllers
{
    public class HopDongController : Controller
    {
        QLNHANSUContext db = new QLNHANSUContext();
        // GET: HopDong
        public ActionResult DanhSachHopDong(string ten)
        {
            if (ten != "" && ten!= null)
            {
                List<HopDong> lis = new List<HopDong>();
                foreach (var item in db.HopDongs.ToList())
                {
                    if (item.NhanVien.LastName == ten)
                    {
                        lis.Add(item);
                    }
                }
                return View(lis);
            }
            return View(db.HopDongs.ToList());
        }
        [HttpGet]
        public ActionResult Them()
        {
            List<_NhanVien> lis = new List<_NhanVien>();
            foreach(var item in db.NhanViens.ToList())
            {
                _NhanVien n = new _NhanVien();
                n.MaNhanVien = item.MaNhanVien;
                n.Ten = item.FirstName + " " + item.LastName;
                lis.Add(n);
            }
            ViewBag.MaNhanVien = new SelectList(lis, "MaNhanVien","Ten", null);
            return View();
        }
        [HttpPost]
        public ActionResult Them(HopDong h)
        {
            if (ModelState.IsValid)
            {
                db.HopDongs.Add(h);
                db.SaveChanges();
                return RedirectToAction("DanhSachHopDong");
            }
            //ViewBag.MaNhanVien = new SelectList(db.NhanViens, "MaNhanVien", "FirstName", h.MaNhanVien);
            return View();
        }
        [HttpGet]
        public ActionResult Sua(int id)
        {
            return View(db.HopDongs.Where(m => m.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Sua(HopDong h)
        {
            if (ModelState.IsValid)
            {
                db.Entry(h).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachHopDong");
            }
            return View();
        }
    }
}