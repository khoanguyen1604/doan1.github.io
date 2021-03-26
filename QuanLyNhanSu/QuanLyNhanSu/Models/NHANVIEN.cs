using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyNhanSu.Models
{
    public class NHANVIEN
    {
        public NHANVIEN()
        {
            HinhAnh = "image_mac_dinh.jpg";
        }
        [Key]
        public string MaNhanVien { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HinhAnh { get; set; }
        public string MaSoKhac { get; set; }
        public string ChungMinhThu { get; set; }
        public DateTime? NgayCap { get; set; }
        public string GioiTinh { get; set; }
        public string TinhTrangHonNhan { get; set; }
        public string QuocTich { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string TenGoiKhac { get; set; }
        public string SoThich { get; set; }
        public string DiaChi { get; set; }
        public string QuanHuyen { get; set; }
        public string ThanhPho { get; set; }
        public string SDTRieng { get; set; }
        public string SDTCongViec { get; set; }
        public string MailCaNhan { get; set; }
        public double LuongCoBan { get; set; }
        public string MailCongViec { get; set; }
        [ForeignKey("PhongBan")]
        public int? MaPhongBan { get; set; }
        public virtual PHONGBAN PhongBan { get; set; }
        [ForeignKey("ChucVu")]
        public int? MaChucVu { get; set; }
        public virtual CHUCVU ChucVu { get; set; }
        public virtual ICollection<CHAMCONG> chamcongs { get; set; }
        public virtual ICollection<BangLuong> bangluongs { get; set; }
        public virtual ICollection<KhenThuongKyLuat> khenthuongkyluats { get; set; }
        public virtual ICollection<HopDong> HopDongs { get; set; }
    }
}