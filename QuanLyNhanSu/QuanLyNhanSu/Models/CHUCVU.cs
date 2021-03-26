using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyNhanSu.Models
{
    public class CHUCVU
    {
        [Key]
        public int MaChucVu { get; set; }
        public string TenChucVu { get; set; }
        public double HeSoLuong { get; set; }
        public string MoTa { get; set; }
        public List<CHUCVU> listchucvu { get; set; }
    }
}