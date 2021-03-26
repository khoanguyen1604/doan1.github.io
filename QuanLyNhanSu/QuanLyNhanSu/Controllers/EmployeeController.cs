using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyNhanSu.Models;
using QuanLyNhanSu.DAL;
using System.Data.Entity.Validation;
using System.IO;
using PagedList;
using PagedList.Mvc;

namespace QuanLyNhanSu.Controllers
{
    public class EmployeeController : Controller
    {
        QLNHANSUContext db = new QLNHANSUContext();
        // GET: Employee
        public ActionResult ThongTinCaNhan()
        {
            string id = Session["MaNhanVien"].ToString();
            var nhanvien = db.NhanViens.Where(m => m.MaNhanVien == id).FirstOrDefault();
            return View(nhanvien);
        }
        public ActionResult ThongTinCaNhan_Admin()
        {
            string id = Session["MaNhanVien"].ToString();
            var nhanvien = db.NhanViens.Where(m => m.MaNhanVien == id).FirstOrDefault();
            return View(nhanvien);
        }
        public ActionResult ChiTietLienLac()
        {
            string id = Session["MaNhanVien"].ToString();
            var nhanvien = db.NhanViens.Where(m => m.MaNhanVien == id).FirstOrDefault();
            return View(nhanvien);
        }
        
        public ActionResult ChiTietLienLac_Admin()
        {
            string id = Session["MaNhanVien"].ToString();
            var nhanvien = db.NhanViens.Where(m => m.MaNhanVien == id).FirstOrDefault();
            return View(nhanvien);
        }
        public ActionResult MucLuong(int? trang, string thang)
        {
            if (Session["MaNhanVien"] != null && thang == null)
            {
                string id = Session["MaNhanVien"].ToString();
                List<BangLuong> lis = new List<BangLuong>();
                foreach (var item in db.BangLuongs.ToList())
                {
                    if (item.MaNhanVien == id)
                    {
                        lis.Add(item);
                    }
                }
                return View(lis.ToPagedList(trang?? 1, 5));
            }
            else if (Session["MaNhanVien"] != null && thang != null)
            {
                string id = Session["MaNhanVien"].ToString();
                List<BangLuong> lis = new List<BangLuong>();
                foreach (var item in db.BangLuongs.ToList())
                {
                    if (item.MaNhanVien == id && thang == item.Thang.ToString("MM/yyyy"))
                    {
                        lis.Add(item);
                    }
                }
                return View(lis.ToPagedList(trang ?? 1, 5));
            }
            return View();
        }
        public ActionResult TaiKhoanCaNhan()
        {
            string id = Session["MaNhanVien"].ToString();
            var nhanvien = db.TaiKhoans.Where(m => m.MaTaiKhoan == id).FirstOrDefault();
            return View(nhanvien);
        }
        public ActionResult TaiKhoanCaNhan_Admin()
        {
            string id = Session["MaNhanVien"].ToString();
            var nhanvien = db.TaiKhoans.Where(m => m.MaTaiKhoan == id).FirstOrDefault();
            return View(nhanvien);
        }
        public ActionResult HoSoNhanSu()
        {
            return View(db.NhanViens.ToList());
        }
        public ActionResult XemChiTietHoSo(string id)
        {
            if (id != null)
            {
                Session["id"] = id;
            }
            string manv = Session["id"].ToString();
            var nhanvien = db.NhanViens.Where(m => m.MaNhanVien == manv).FirstOrDefault();
            Session["chitietten"] = nhanvien.FirstName + " " + nhanvien.LastName;
            Session["chitiethinhanh"] = nhanvien.HinhAnh;
            return View(nhanvien);
        }
        public ActionResult XemChiTietLienLac()
        {
            string manv = Session["id"].ToString();
            var nhanvien = db.NhanViens.Where(m => m.MaNhanVien == manv).FirstOrDefault();
            return View(nhanvien);
        }
        public ActionResult XemChiTietTaiKhoan()
        {
            string id = Session["id"].ToString();
            var tk = db.TaiKhoans.Where(m => m.MaTaiKhoan == id).FirstOrDefault();
            return View(tk);
        }
        public ActionResult chonChucVu()
        {
            CHUCVU c = new CHUCVU();
            c.listchucvu = db.ChucVus.ToList();
            return PartialView(c);
        }
        public ActionResult chonPhongBan()
        {
            PHONGBAN p = new PHONGBAN();
            p.listphongban = db.PhongBans.ToList();
            return PartialView(p);
        }
        [HttpGet]
        public ActionResult ThemNhanVien()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemNhanVien(NHANVIEN n, HttpPostedFileBase HinhAnh)
        {
            if (ModelState.IsValid)
            {
                var a = db.PhongBans.Where(m => m.MaPhongBan == n.MaPhongBan).FirstOrDefault();
                if (HinhAnh != null)
                {
                    var filename = Path.GetFileName(HinhAnh.FileName);
                    n.HinhAnh = filename;
                    string path = Path.Combine(Server.MapPath("~/Content/Images/"), filename);
                    HinhAnh.SaveAs(path);
                }
                else
                    n.HinhAnh = "image_mac_dinh.jpg";
                db.NhanViens.Add(n);
                if (a != null)
                {
                    if (a.SoNhanVienHienTai < a.SoNhanVienToiDa)
                    {
                        a.SoNhanVienHienTai += 1;
                        db.Entry(a).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                        return View("Số nhân viên trong phòng ban đã đạt tối đa");
                }
                db.SaveChanges();
                return RedirectToAction("HoSoNhanSu");
            }
            return View();
        }
        [HttpGet]
        public ActionResult SuaNhanVien(string id)
        {
            return View(db.NhanViens.Where(m => m.MaNhanVien == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult SuaNhanVien(NHANVIEN n, string id)
        {
            var a = db.PhongBans.Where(m => m.MaPhongBan == n.MaPhongBan).FirstOrDefault();
            db.Entry(n).State = System.Data.Entity.EntityState.Modified;
            //if (a != null)
            //{
            //    if (a.SoNhanVienHienTai < a.SoNhanVienToiDa)
            //    {
            //        a.SoNhanVienHienTai += 1;
            //        db.Entry(a).State = System.Data.Entity.EntityState.Modified;
            //    }
            //    else
            //        return View("Số nhân viên trong phòng ban đã đạt tối đa");
            //}
            db.SaveChanges();
            return RedirectToAction("HoSoNhanSu");
        }
        public ActionResult XoaNhanVien(NHANVIEN n, string id)
        {
            try
            {
                n = db.NhanViens.Where(m => m.MaNhanVien == id).FirstOrDefault();
                var a = db.PhongBans.Where(m => m.MaPhongBan == n.MaPhongBan).FirstOrDefault();
                var t = db.TaiKhoans.Where(m => m.MaTaiKhoan == n.MaNhanVien).FirstOrDefault();
                if (a != null && n.MaPhongBan!=null)
                {
                    a.SoNhanVienHienTai--;
                    db.Entry(a).State = System.Data.Entity.EntityState.Modified;
                }
                if (t != null)
                {
                    db.TaiKhoans.Remove(t);
                }
                db.NhanViens.Remove(n);
                db.SaveChanges();
                return RedirectToAction("HoSoNhanSu");
            }
            catch
            {
                return RedirectToAction("HoSoNhanSu");
            }
        }
        [HttpGet]
        public ActionResult SuaThongTinCaNhan(string id)
        {
            var nhanvien = db.NhanViens.Where(m => m.MaNhanVien == id).FirstOrDefault();
            return View(nhanvien);
        }
        [HttpPost]
        public ActionResult SuaThongTinCaNhan(NHANVIEN n, string id, HttpPostedFileBase HinhAnh)
        {
            if (n != null)
            {
                if (ModelState.IsValid)
                {
                    if (HinhAnh != null)
                    {
                        var filename = Path.GetFileName(HinhAnh.FileName);
                        n.HinhAnh = filename;
                        string path = Path.Combine(Server.MapPath("~/Content/Images/"), filename);
                        HinhAnh.SaveAs(path);
                        Session["hinhanh"] = n.HinhAnh;
                    }
                    else
                        n.HinhAnh = Session["hinhanh"].ToString();
                    Session["ten"] = n.FirstName + " " + n.LastName;
                    db.Entry(n).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ThongTinCaNhan");
                }
                else
                    return View();
            }
            else
                return View();
            
        }
        [HttpGet]
        public ActionResult SuaThongTinCaNhan_Admin(string id)
        {
            var nhanvien = db.NhanViens.Where(m => m.MaNhanVien == id).FirstOrDefault();
            return View(nhanvien);
        }
        [HttpPost]
        public ActionResult SuaThongTinCaNhan_Admin(NHANVIEN n, string id, HttpPostedFileBase HinhAnh)
        {
            if (n != null)
            {
                if (ModelState.IsValid)
                {
                    if (HinhAnh != null)
                    {
                        var filename = Path.GetFileName(HinhAnh.FileName);
                        n.HinhAnh = filename;
                        string path = Path.Combine(Server.MapPath("~/Content/Images/"), filename);
                        HinhAnh.SaveAs(path);
                        Session["hinhanh"] = n.HinhAnh;
                    }
                    else
                        n.HinhAnh = Session["hinhanh"].ToString();
                    Session["ten"] = n.FirstName + " " + n.LastName;
                    db.Entry(n).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ThongTinCaNhan_Admin");
                }
                else
                    return View();
            }
            else
                return View();

        }
        [HttpGet]
        public ActionResult SuaThongTinLienLac(string id)
        {
            var nhanvien = db.NhanViens.Where(m => m.MaNhanVien == id).FirstOrDefault();
            return View(nhanvien);
        }
        [HttpPost]
        public ActionResult SuaThongTinLienLac(NHANVIEN n, string id)
        {
            if (n != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(n).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ChiTietLienLac");
                }
                else
                    return View();
            }
            else
                return View();

        }
        [HttpGet]
        public ActionResult SuaThongTinLienLac_Admin(string id)
        {
            var nhanvien = db.NhanViens.Where(m => m.MaNhanVien == id).FirstOrDefault();
            return View(nhanvien);
        }
        [HttpPost]
        public ActionResult SuaThongTinLienLac_Admin(NHANVIEN n, string id)
        {
            if (n != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(n).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ChiTietLienLac_Admin");
                }
                else
                    return View();
            }
            else
                return View();

        }
    }
}