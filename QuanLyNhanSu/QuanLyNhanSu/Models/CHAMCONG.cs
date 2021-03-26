using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyNhanSu.Models
{
    public class CHAMCONG
    {
        [Key]
        public int IDChamCong { get; set; }
        [Required]
        //[ForeignKey("NhanVien")]
        public string MaNhanVien { get; set; }
        //public NHANVIEN NhanVien { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Ngay { get; set; }
        public string TrangThai { get; set; }
        //public string GhiChu { get; set; }
        public virtual NHANVIEN Nhanvien { get; set; }
    }
}