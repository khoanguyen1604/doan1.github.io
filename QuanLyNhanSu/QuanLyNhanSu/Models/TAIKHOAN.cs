using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyNhanSu.Models
{
    public class TAIKHOAN
    {
        [ForeignKey("nhanvien")]
        [Key]
        public string MaTaiKhoan { get; set; }
        public string TenTaiKhoan { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }
        [NotMapped]
        [Required]
        [DataType(DataType.Password)]
        [Compare("MatKhau",ErrorMessage ="Mat Khau nhap lai khong trung khop")]
        public string NhapLaiMatKhau { get; set; }
        public NHANVIEN nhanvien { get; set; }
    }
}