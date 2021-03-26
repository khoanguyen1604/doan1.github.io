using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using QuanLyNhanSu.Models;
using QuanLyNhanSu.DAL;

namespace QuanLyNhanSu.Models
{
    public class PHONGBAN
    {
        [Key]
        public int MaPhongBan { get; set; }
        public string TenPhongBan { get; set; }
        public int SoNhanVienToiDa { get; set; }
        public int SoNhanVienHienTai { get; set; }
        public string MoTa { get; set; }
        public List<PHONGBAN> listphongban { get; set; }
        public PHONGBAN()
        {
            SoNhanVienHienTai = 0;
        }
    }
}