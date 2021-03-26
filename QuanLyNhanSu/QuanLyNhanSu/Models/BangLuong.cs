using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyNhanSu.Models
{
    public class BangLuong
    {
        [Key]
        public int IDBangLuong { get; set; }
        [Required]
        //[ForeignKey("NhanVien")]
        public string MaNhanVien { get; set; }
        //public NHANVIEN NhanVien { get; set; }
        [DisplayFormat(DataFormatString = "{0:MMM yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Thang { get; set; }
        public int SoNgayLam { get; set; }
        public int SoNgayNghi { get; set; }
        //public double LuongCoBan { get; set; }
        public double LuongThuong { get; set; }
        public double LuongTru { get; set; }
        public double TongLuong { get; set; }
        public string GhiChu { get; set; }
        public virtual NHANVIEN Nhanvien { get; set; }
    }
}