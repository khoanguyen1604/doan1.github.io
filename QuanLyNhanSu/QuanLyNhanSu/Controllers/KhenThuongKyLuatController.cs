using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyNhanSu.Models;
using QuanLyNhanSu.DAL;

namespace QuanLyNhanSu.Controllers
{
    public class KhenThuongKyLuatController : Controller
    {
        // GET: KhenThuongKyLuat
        QLNHANSUContext db = new QLNHANSUContext();
        public ActionResult KhenThuongKyLuat(string ngay, string ktkl)
        {
            if (ktkl != null && ngay != "")
            {
                if (ktkl == "khenthuong")
                {
                    List<KhenThuongKyLuat> lis = new List<KhenThuongKyLuat>();
                    foreach(var item in db.KhenThuongKyLuats.ToList())
                    {
                        if (item.Loai == true && ngay == item.Ngay.ToString("MM/dd/yyyy"))
                        {
                            lis.Add(item);
                        }
                    }
                    return View(lis);
                }
                if (ktkl == "kyluat")
                {
                    List<KhenThuongKyLuat> lis = new List<KhenThuongKyLuat>();
                    foreach (var item in db.KhenThuongKyLuats.ToList())
                    {
                        if (item.Loai == false && ngay == item.Ngay.ToString("MM/dd/yyyy"))
                        {
                            lis.Add(item);
                        }
                    }
                    return View(lis);
                }
                if (ktkl == "tatca")
                {
                    List<KhenThuongKyLuat> lis = new List<KhenThuongKyLuat>();
                    foreach (var item in db.KhenThuongKyLuats.ToList())
                    {
                        if (ngay == item.Ngay.ToString("MM/dd/yyyy"))
                        {
                            lis.Add(item);
                        }
                    }
                    return View(lis);
                }
            }
            else if(ngay == "")
            {
                if (ktkl == "khenthuong")
                {
                    List<KhenThuongKyLuat> lis = new List<KhenThuongKyLuat>();
                    foreach (var item in db.KhenThuongKyLuats.ToList())
                    {
                        if (item.Loai == true)
                        {
                            lis.Add(item);
                        }
                    }
                    return View(lis);
                }
                if (ktkl == "kyluat")
                {
                    List<KhenThuongKyLuat> lis = new List<KhenThuongKyLuat>();
                    foreach (var item in db.KhenThuongKyLuats.ToList())
                    {
                        if (item.Loai == false)
                        {
                            lis.Add(item);
                        }
                    }
                    return View(lis);
                }
                if (ktkl == "tatca")
                {
                    return View(db.KhenThuongKyLuats.ToList());
                }
            }
            return View(db.KhenThuongKyLuats.ToList());
        }
        [HttpGet]
        public ActionResult Them()
        {
            List<_NhanVien> lis = new List<_NhanVien>();
            foreach (var item in db.NhanViens.ToList())
            {
                _NhanVien n = new _NhanVien();
                n.MaNhanVien = item.MaNhanVien;
                n.Ten = item.FirstName + " " + item.LastName;
                lis.Add(n);
            }
            ViewBag.MaNhanVien = new SelectList(lis, "MaNhanVien", "Ten", null);
            //ViewBag.MaNhanVien = new SelectList(db.NhanViens, "MaNhanVien", "FirstName", "LastName");
            return View();
        }
        [HttpPost]
        public ActionResult Them(KhenThuongKyLuat k)
        {
            if (ModelState.IsValid)
            {
                db.KhenThuongKyLuats.Add(k);
                db.SaveChanges();
                return RedirectToAction("KhenThuongKyLuat");
            }
            //ViewBag.MaNhanVien = new SelectList(db.NhanViens, "MaNhanVien", "FirstName", k.MaNhanVien);
            return View(k);
        }
        [HttpGet]
        public ActionResult Sua(int id)
        {
            return View(db.KhenThuongKyLuats.Where(m => m.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Sua(KhenThuongKyLuat k, int id)
        {
            if (ModelState.IsValid)
            {
                db.Entry(k).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("KhenThuongKyLuat");
            }
            return View(k);
        }
        public ActionResult ThanhTichThiDua(string ngay, string ktkl)
        {
            string id = Session["MaNhanVien"].ToString();
            if (ktkl != null && ngay != "")
            {
                if (ktkl == "khenthuong")
                {
                    List<KhenThuongKyLuat> lis = new List<KhenThuongKyLuat>();
                    foreach (var item in db.KhenThuongKyLuats.ToList())
                    {
                        if (item.Loai == true && ngay == item.Ngay.ToString("MM/dd/yyyy") && item.MaNhanVien == id)
                        {
                            lis.Add(item);
                        }
                    }
                    return View(lis);
                }
                if (ktkl == "kyluat")
                {
                    List<KhenThuongKyLuat> lis = new List<KhenThuongKyLuat>();
                    foreach (var item in db.KhenThuongKyLuats.ToList())
                    {
                        if (item.Loai == false && ngay == item.Ngay.ToString("MM/dd/yyyy") && item.MaNhanVien == id)
                        {
                            lis.Add(item);
                        }
                    }
                    return View(lis);
                }
                if (ktkl == "tatca")
                {
                    List<KhenThuongKyLuat> lis = new List<KhenThuongKyLuat>();
                    foreach (var item in db.KhenThuongKyLuats.ToList())
                    {
                        if (ngay == item.Ngay.ToString("MM/dd/yyyy") && item.MaNhanVien == id)
                        {
                            lis.Add(item);
                        }
                    }
                    return View(lis);
                }
            }
            else if (ngay == "")
            {
                if (ktkl == "khenthuong")
                {
                    List<KhenThuongKyLuat> lis = new List<KhenThuongKyLuat>();
                    foreach (var item in db.KhenThuongKyLuats.ToList())
                    {
                        if (item.Loai == true && item.MaNhanVien == id)
                        {
                            lis.Add(item);
                        }
                    }
                    return View(lis);
                }
                if (ktkl == "kyluat")
                {
                    List<KhenThuongKyLuat> lis = new List<KhenThuongKyLuat>();
                    foreach (var item in db.KhenThuongKyLuats.ToList())
                    {
                        if (item.Loai == false && item.MaNhanVien == id)
                        {
                            lis.Add(item);
                        }
                    }
                    return View(lis);
                }
                if (ktkl == "tatca")
                {
                    return View(db.KhenThuongKyLuats.Where(m => m.MaNhanVien == id).ToList());
                }
            }
            return View(db.KhenThuongKyLuats.Where(m => m.MaNhanVien == id).ToList());
        }
    }
}