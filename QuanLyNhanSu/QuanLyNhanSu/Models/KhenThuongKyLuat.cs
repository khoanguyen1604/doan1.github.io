using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyNhanSu.Models
{
    public class KhenThuongKyLuat
    {
        [Key]
        public int ID { get; set; }
        public string MaNhanVien { get; set; }
        public DateTime Ngay { get; set; }
        public bool Loai { get; set; }
        public string TieuDe { get; set; }
        public string GhiChu { get; set; }
        public virtual NHANVIEN Nhanvien { get; set; }
    }
}