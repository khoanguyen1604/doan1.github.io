namespace QuanLyNhanSu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bangluong_chamcong : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BangLuongs",
                c => new
                    {
                        IDBangLuong = c.Int(nullable: false, identity: true),
                        MaNhanVien = c.String(nullable: false, maxLength: 128),
                        Thang = c.DateTime(nullable: false),
                        SoNgayLam = c.Int(nullable: false),
                        SoNgayNghi = c.Int(nullable: false),
                        LuongCoBan = c.Double(nullable: false),
                        LuongThuong = c.Double(nullable: false),
                        LuongTru = c.Double(nullable: false),
                        TongLuong = c.Double(nullable: false),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.IDBangLuong)
                .ForeignKey("dbo.NHANVIENs", t => t.MaNhanVien, cascadeDelete: true)
                .Index(t => t.MaNhanVien);
            
            CreateTable(
                "dbo.CHAMCONGs",
                c => new
                    {
                        IDChamCong = c.Int(nullable: false, identity: true),
                        MaNhanVien = c.String(nullable: false, maxLength: 128),
                        Ngay = c.DateTime(nullable: false),
                        TrangThai = c.String(),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.IDChamCong)
                .ForeignKey("dbo.NHANVIENs", t => t.MaNhanVien, cascadeDelete: true)
                .Index(t => t.MaNhanVien);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CHAMCONGs", "MaNhanVien", "dbo.NHANVIENs");
            DropForeignKey("dbo.BangLuongs", "MaNhanVien", "dbo.NHANVIENs");
            DropIndex("dbo.CHAMCONGs", new[] { "MaNhanVien" });
            DropIndex("dbo.BangLuongs", new[] { "MaNhanVien" });
            DropTable("dbo.CHAMCONGs");
            DropTable("dbo.BangLuongs");
        }
    }
}
