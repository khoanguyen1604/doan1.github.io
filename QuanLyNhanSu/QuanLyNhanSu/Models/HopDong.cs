using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyNhanSu.Models
{
    public class HopDong
    {
        [Key]
        public int ID { get; set; }
        public string MaNhanVien { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public DateTime NgayKy { get; set; }
        public string NoiDung { get; set; }
        public virtual NHANVIEN NhanVien { get; set; }
    }
}