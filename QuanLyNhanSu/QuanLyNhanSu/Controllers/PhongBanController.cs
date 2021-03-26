using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyNhanSu.Models;
using QuanLyNhanSu.DAL;

namespace QuanLyNhanSu.Controllers
{
    public class PhongBanController : Controller
    {
        QLNHANSUContext db = new QLNHANSUContext();
        // GET: PhongBan
        public ActionResult DanhSachPhongBan()
        {
            return View(db.PhongBans.ToList());
        }
        public ActionResult DanhSachNhanVienPhongBan(int id)
        {
            List<NHANVIEN> lis = new List<NHANVIEN>();
            foreach(NHANVIEN n in db.NhanViens)
            {
                if (n.MaPhongBan == id)
                {
                    lis.Add(n);
                }
            }
            return View(lis);
        }
        public ActionResult XoaNhanVienPhongBan(string id)
        {
            var n = db.NhanViens.Where(m => m.MaNhanVien == id).FirstOrDefault();
            var a = db.PhongBans.Where(m => m.MaPhongBan == n.MaPhongBan).FirstOrDefault();
            if (a != null)
            {
                a.SoNhanVienHienTai--;
            }
            n.MaPhongBan = null;
            db.SaveChanges();
            return RedirectToAction("DanhSachPhongBan");
        }
        [HttpGet]
        public ActionResult ThemPhongBan()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemPhongBan(PHONGBAN p)
        {
            if(ModelState.IsValid)
            {
                db.PhongBans.Add(p);
                db.SaveChanges();
                return RedirectToAction("DanhSachPhongBan");
            }
            return View(p);
        }
        [HttpGet]
        public ActionResult SuaPhongBan(int id)
        {
            return View(db.PhongBans.Where(m => m.MaPhongBan == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult SuaPhongBan(PHONGBAN p, int id)
        {
            db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DanhSachPhongBan");
        }
        public ActionResult XoaPhongBan(PHONGBAN p, int id)
        {
            try
            {
                p = db.PhongBans.Where(m => m.MaPhongBan == id).FirstOrDefault();
                db.PhongBans.Remove(p);
                db.SaveChanges();
                return RedirectToAction("DanhSachPhongBan");
            }
            catch
            {
                Session["error"] = "Đang còn nhân viên trong phòng ban";
                Session["id"] = id;
                return RedirectToAction("DanhSachPhongBan");
            }
        }
    }
}