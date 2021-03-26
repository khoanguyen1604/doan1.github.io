using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyNhanSu.Models;
using QuanLyNhanSu.DAL;
using Microsoft.Reporting.WebForms;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System.Data;

namespace QuanLyNhanSu.Controllers
{
    public class BangLuongController : Controller
    {
        QLNHANSUContext db = new QLNHANSUContext();
        // GET: BangLuong
        public ActionResult BangLuong(string bangluong, string thang)
        {
            if (bangluong == "tatca")
            {
                List<BangLuong> lis = new List<BangLuong>();
                foreach (var item in db.BangLuongs.ToList())
                {
                    var thangtinh = item.Thang.Date.ToString("MM/yyyy");
                    if (thangtinh == thang)
                    {
                        lis.Add(item);
                    }
                }
                return View(lis);
            }
            else if (bangluong == "chuatinh")
            {
                List<BangLuong> lis = new List<BangLuong>();
                foreach (var item in db.BangLuongs.ToList())
                {
                    var thangtinh = item.Thang.Date.ToString("MM/yyyy");
                    if (thangtinh == thang && item.TongLuong == 0)
                    {
                        lis.Add(item);
                    }
                }
                return View(lis);
            }
            else if (bangluong == "datinh")
            {
                List<BangLuong> lis = new List<BangLuong>();
                foreach (var item in db.BangLuongs.ToList())
                {
                    var thangtinh = item.Thang.Date.ToString("MM/yyyy");
                    if (thangtinh == thang && item.TongLuong != 0)
                    {
                        lis.Add(item);
                    }
                }
                return View(lis);
            }
            return View(db.BangLuongs.ToList());
        }
        public ActionResult TaoMoi()
        {
            int a = 0;
            foreach (var item in db.BangLuongs.ToList())
            {
                var _thang = item.Thang.Date.ToString("MM/yyyy");
                if (_thang == DateTime.Today.ToString("MM/yyyy"))
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
                    BangLuong b = new BangLuong();
                    b.MaNhanVien = item.MaNhanVien;
                    b.Thang = DateTime.Today;
                    db.BangLuongs.Add(b);
                }
                db.SaveChanges();
                return RedirectToAction("BangLuong");
            }
            return RedirectToAction("BangLuong");
        }
        [HttpGet]
        public ActionResult TinhLuong(int id)
        {
            var t = db.BangLuongs.Where(m => m.IDBangLuong == id).FirstOrDefault();
            if (t != null)
            {
                int ngayducong = 0;
                int ngaynghi = 0;
                //var chamcong = db.ChamCongs.Where(m => m.MaNhanVien == t.MaNhanVien && t.Thang.Month.ToString("MM/yyyy") == m.Ngay.Month.ToString("MM/yyyy")).FirstOrDefault();
                foreach (var item in db.ChamCongs.ToList())
                {
                    if (item.MaNhanVien == t.MaNhanVien && t.Thang.ToString("MM/yyyy") == item.Ngay.ToString("MM/yyyy") && (item.TrangThai == "Đúng giờ" || item.TrangThai == "Đi trễ"))
                    {
                        ngayducong += 1;
                    }
                    else if (item.MaNhanVien == t.MaNhanVien && t.Thang.ToString("MM/yyyy") == item.Ngay.ToString("MM/yyyy") && item.TrangThai == "Nghỉ")
                    {
                        ngaynghi += 1;
                    }
                }
                t.SoNgayLam = ngayducong;
                t.SoNgayNghi = ngaynghi;
                db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return View(t);
            }
            return View();
        }
        [HttpPost]
        public ActionResult TinhLuong(BangLuong b, int id, double luongcoban)
        {
            //var nhanvien = db.BangLuongs.Where(m => m.IDBangLuong == id).FirstOrDefault();
            //var _nhanvien = db.NhanViens.Where(m => m.MaNhanVien == nhanvien.MaNhanVien).FirstOrDefault();
            b.TongLuong = (luongcoban + b.LuongThuong - b.LuongTru) * b.SoNgayLam / 22;
            db.Entry(b).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("BangLuong");
        }
        public ActionResult BangTinhLuong_NhanVien(string thang)
        {
            string id = Session["MaNhanVien"].ToString();
            if (thang != "" && thang != null)
            {
                List<BangLuong> lis = new List<BangLuong>();
                foreach (var item in db.BangLuongs.ToList())
                {
                    if (thang == item.Thang.ToString("MM/yyyy") && item.MaNhanVien == id)
                    {
                        lis.Add(item);
                    }
                }
                return View(lis);
            }
            return View(db.BangLuongs.Where(m => m.MaNhanVien == id).ToList());
        }
    }
}