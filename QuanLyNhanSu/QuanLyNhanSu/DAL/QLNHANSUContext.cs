using QuanLyNhanSu.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuanLyNhanSu.DAL
{
    public class QLNHANSUContext:DbContext
    {
        public QLNHANSUContext():base("name = quanlynhansu1")
        {

        }
        public DbSet<PHONGBAN> PhongBans { get; set; }
        public DbSet<CHUCVU> ChucVus { get; set; }
        public DbSet<NHANVIEN> NhanViens { get; set; }
        public DbSet<TAIKHOAN> TaiKhoans { get; set; }
        public DbSet<CHAMCONG> ChamCongs { get; set; }
        public DbSet<BangLuong> BangLuongs { get; set; }
        public DbSet<KhenThuongKyLuat> KhenThuongKyLuats { get; set; }
        public DbSet<HopDong> HopDongs { get; set; }
    }
}