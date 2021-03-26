namespace QuanLyNhanSu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class khenthuongkyluat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KhenThuongKyLuats",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MaNhanVien = c.String(maxLength: 128),
                        Ngay = c.DateTime(nullable: false),
                        Loai = c.Boolean(nullable: false),
                        TieuDe = c.String(),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.NHANVIENs", t => t.MaNhanVien)
                .Index(t => t.MaNhanVien);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KhenThuongKyLuats", "MaNhanVien", "dbo.NHANVIENs");
            DropIndex("dbo.KhenThuongKyLuats", new[] { "MaNhanVien" });
            DropTable("dbo.KhenThuongKyLuats");
        }
    }
}
