namespace QuanLyNhanSu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CHUCVUs",
                c => new
                    {
                        MaChucVu = c.Int(nullable: false, identity: true),
                        TenChucVu = c.String(),
                        HeSoLuong = c.Double(nullable: false),
                        MoTa = c.String(),
                        CHUCVU_MaChucVu = c.Int(),
                    })
                .PrimaryKey(t => t.MaChucVu)
                .ForeignKey("dbo.CHUCVUs", t => t.CHUCVU_MaChucVu)
                .Index(t => t.CHUCVU_MaChucVu);
            
            CreateTable(
                "dbo.NHANVIENs",
                c => new
                    {
                        MaNhanVien = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        HinhAnh = c.String(),
                        MaSoKhac = c.String(),
                        ChungMinhThu = c.String(),
                        NgayCap = c.DateTime(),
                        GioiTinh = c.String(),
                        TinhTrangHonNhan = c.String(),
                        QuocTich = c.String(),
                        NgaySinh = c.DateTime(),
                        TenGoiKhac = c.String(),
                        SoThich = c.String(),
                        DiaChi = c.String(),
                        QuanHuyen = c.String(),
                        ThanhPho = c.String(),
                        SDTRieng = c.String(),
                        SDTCongViec = c.String(),
                        MailCaNhan = c.String(),
                        MailCongViec = c.String(),
                        MaPhongBan = c.Int(),
                        MaChucVu = c.Int(),
                    })
                .PrimaryKey(t => t.MaNhanVien)
                .ForeignKey("dbo.CHUCVUs", t => t.MaChucVu)
                .ForeignKey("dbo.PHONGBANs", t => t.MaPhongBan)
                .Index(t => t.MaPhongBan)
                .Index(t => t.MaChucVu);
            
            CreateTable(
                "dbo.PHONGBANs",
                c => new
                    {
                        MaPhongBan = c.Int(nullable: false, identity: true),
                        TenPhongBan = c.String(),
                        SoNhanVienToiDa = c.Int(nullable: false),
                        SoNhanVienHienTai = c.Int(nullable: false),
                        MoTa = c.String(),
                        PHONGBAN_MaPhongBan = c.Int(),
                    })
                .PrimaryKey(t => t.MaPhongBan)
                .ForeignKey("dbo.PHONGBANs", t => t.PHONGBAN_MaPhongBan)
                .Index(t => t.PHONGBAN_MaPhongBan);
            
            CreateTable(
                "dbo.TAIKHOANs",
                c => new
                    {
                        MaTaiKhoan = c.String(nullable: false, maxLength: 128),
                        TenTaiKhoan = c.String(),
                        MatKhau = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MaTaiKhoan)
                .ForeignKey("dbo.NHANVIENs", t => t.MaTaiKhoan)
                .Index(t => t.MaTaiKhoan);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TAIKHOANs", "MaTaiKhoan", "dbo.NHANVIENs");
            DropForeignKey("dbo.NHANVIENs", "MaPhongBan", "dbo.PHONGBANs");
            DropForeignKey("dbo.PHONGBANs", "PHONGBAN_MaPhongBan", "dbo.PHONGBANs");
            DropForeignKey("dbo.NHANVIENs", "MaChucVu", "dbo.CHUCVUs");
            DropForeignKey("dbo.CHUCVUs", "CHUCVU_MaChucVu", "dbo.CHUCVUs");
            DropIndex("dbo.TAIKHOANs", new[] { "MaTaiKhoan" });
            DropIndex("dbo.PHONGBANs", new[] { "PHONGBAN_MaPhongBan" });
            DropIndex("dbo.NHANVIENs", new[] { "MaChucVu" });
            DropIndex("dbo.NHANVIENs", new[] { "MaPhongBan" });
            DropIndex("dbo.CHUCVUs", new[] { "CHUCVU_MaChucVu" });
            DropTable("dbo.TAIKHOANs");
            DropTable("dbo.PHONGBANs");
            DropTable("dbo.NHANVIENs");
            DropTable("dbo.CHUCVUs");
        }
    }
}
