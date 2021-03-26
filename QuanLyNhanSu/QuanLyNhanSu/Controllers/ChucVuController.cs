using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyNhanSu.Models;
using QuanLyNhanSu.DAL;

namespace QuanLyNhanSu.Controllers
{
    public class ChucVuController : Controller
    {
        QLNHANSUContext db = new QLNHANSUContext();
        // GET: ChucVu
        public ActionResult ChucVu()
        {
            return View(db.ChucVus.ToList());
        }
        public ActionResult DanhSachNhanVienChucVu(int id)
        {
            List<NHANVIEN> lis = new List<NHANVIEN>();
            foreach (NHANVIEN n in db.NhanViens)
            {
                if (n.MaChucVu == id)
                {
                    lis.Add(n);
                }
            }
            return View(lis);
        }
        public ActionResult XoaNhanVienChucVu(string id)
        {
            var n = db.NhanViens.Where(m => m.MaNhanVien == id).FirstOrDefault();
            var a = db.ChucVus.Where(m => m.MaChucVu == n.MaChucVu).FirstOrDefault();
            n.ChucVu = null;
            db.SaveChanges();
            return RedirectToAction("ChucVu");
        }
        [HttpGet]
        public ActionResult ThemChucVu()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemChucVu(CHUCVU c)
        {
            if (ModelState.IsValid)
            {
                db.ChucVus.Add(c);
                db.SaveChanges();
                return RedirectToAction("ChucVu");
            }
            return View(c);
        }
        [HttpGet]
        public ActionResult SuaChucVu(int id)
        {
            return View(db.ChucVus.Where(m => m.MaChucVu == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult SuaChucVu(CHUCVU c, int id)
        {
            db.Entry(c).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ChucVu");
        }
        public ActionResult XoaChucVu(CHUCVU c, int id)
        {
            try
            {
                c = db.ChucVus.Where(m => m.MaChucVu == id).FirstOrDefault();
                db.ChucVus.Remove(c);
                db.SaveChanges();
                return RedirectToAction("ChucVu");
            }
            catch
            {
                return View("Vẫn còn nhân viên đang giữ chức vụ hiện tại");
            }
        }
    }
}