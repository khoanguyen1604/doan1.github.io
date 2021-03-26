using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyNhanSu.Models;
using QuanLyNhanSu.DAL;

namespace QuanLyNhanSu.Controllers
{
    public class LoginController : Controller
    {
        QLNHANSUContext db = new QLNHANSUContext();
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(TAIKHOAN t)
        {
            if(db.TaiKhoans.Count() == 0)
            {
                NHANVIEN n = new NHANVIEN();
                n.MaNhanVien = "QL-1";
                db.NhanViens.Add(n);
                TAIKHOAN tk = new TAIKHOAN();
                tk.MaTaiKhoan = n.MaNhanVien;
                tk.TenTaiKhoan = "admin";
                tk.MatKhau = "123";
                tk.NhapLaiMatKhau = "123";
                db.TaiKhoans.Add(tk);
                db.SaveChanges();
            }
            var check = db.TaiKhoans.Where(m => m.TenTaiKhoan.Equals(t.TenTaiKhoan) && m.MatKhau.Equals(t.MatKhau)).FirstOrDefault();
            if (check!= null)
            {
                var check1 = db.NhanViens.Where(m => m.MaNhanVien == check.MaTaiKhoan).FirstOrDefault();
                Session["hinhanh"] = check1.HinhAnh;
                Session["ten"] = check1.FirstName +" "+ check1.LastName;
                var a = check1.MaNhanVien.Split('-');
                Session["ma"] = a[0];
                if (a[0] != "QL")
                {
                    ViewBag.href = "/Employee/HoSoNhanSu";
                    Session["MaNhanVien"] = check1.MaNhanVien;
                    return RedirectToAction("ThongTinCaNhan", "Employee");
                }
                else
                {
                    Session["MaNhanVien"] = check1.MaNhanVien;
                    return RedirectToAction("HoSoNhanSu", "Employee");
                }
            }
            else
            {
                ViewBag.error = "Thông tin đăng nhập không chính xác";
                return View();
            }
        }
        [HttpGet]
        public ActionResult ThayDoiMatKhau(string id)
        {
            if (Session["MaNhanVien"] != null)
            {
                id = Session["MaNhanVien"].ToString();
                return View(db.TaiKhoans.Where(m => m.MaTaiKhoan == id).FirstOrDefault());
            }
            return View();
        }
        [HttpPost]
        public ActionResult ThayDoiMatKhau(TAIKHOAN t, string id)
        {
            if (Session["MaNhanVien"] != null)
            {
                id = Session["MaNhanVien"].ToString();
                if (ModelState.IsValid)
                {
                    db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    string manhanvien = Session["MaNhanVien"].ToString().Split('-')[0];
                    if (manhanvien == "QL")
                    {
                        return RedirectToAction("TaiKhoanCaNhan_Admin", "Employee");
                    }
                    return RedirectToAction("TaiKhoanCaNhan", "Employee");
                }
            }
            return View(t);
        }
    }
}