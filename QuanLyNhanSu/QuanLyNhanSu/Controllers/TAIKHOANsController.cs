using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyNhanSu.DAL;
using QuanLyNhanSu.Models;

namespace QuanLyNhanSu.Controllers
{
    public class TAIKHOANsController : Controller
    {
        private QLNHANSUContext db = new QLNHANSUContext();

        // GET: TAIKHOANs
        public ActionResult Index()
        {
            var taiKhoans = db.TaiKhoans.Include(t => t.nhanvien);
            return View(taiKhoans.ToList());
        }

        // GET: TAIKHOANs/Create
        public ActionResult Create()
        {
            List<_taikhoan> lis = new List<_taikhoan>();
            foreach (var item in db.NhanViens.ToList())
            {
                _taikhoan n = new _taikhoan();
                n.MaTaiKhoan = item.MaNhanVien;
                n.Ten = item.FirstName + " " + item.LastName;
                lis.Add(n);
            }
            ViewBag.MaTaiKhoan = new SelectList(lis, "MaTaiKhoan", "Ten", null);
            return View();
        }

        // POST: TAIKHOANs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTaiKhoan,TenTaiKhoan,MatKhau,NhapLaiMatKhau")] TAIKHOAN tAIKHOAN)
        {
            if (ModelState.IsValid)
            {
                db.TaiKhoans.Add(tAIKHOAN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.MaTaiKhoan = new SelectList(db.NhanViens, "MaNhanVien", "FirstName", tAIKHOAN.MaTaiKhoan);
            List<_taikhoan> lis = new List<_taikhoan>();
            foreach (var item in db.NhanViens.ToList())
            {
                _taikhoan n = new _taikhoan();
                n.MaTaiKhoan = item.MaNhanVien;
                n.Ten = item.FirstName + " " + item.LastName;
                lis.Add(n);
            }
            ViewBag.MaTaiKhoan = new SelectList(lis, "MaTaiKhoan", "Ten", null);
            return View(tAIKHOAN);
        }

        // GET: TAIKHOANs/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TAIKHOAN tAIKHOAN = db.TaiKhoans.Find(id);
        //    if (tAIKHOAN == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tAIKHOAN);
        //}

        // POST: TAIKHOANs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TAIKHOAN tAIKHOAN = db.TaiKhoans.Find(id);
            db.TaiKhoans.Remove(tAIKHOAN);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult ResetPassword(string id)
        {
            NHANVIEN n = db.NhanViens.Find(id);
            TAIKHOAN tAIKHOAN = db.TaiKhoans.Find(id);
            tAIKHOAN.MatKhau = n.MaNhanVien + "123";
            tAIKHOAN.NhapLaiMatKhau = n.MaNhanVien + "123";
            db.Entry(tAIKHOAN).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
