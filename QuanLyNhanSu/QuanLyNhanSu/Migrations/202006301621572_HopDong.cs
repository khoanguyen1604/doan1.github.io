namespace QuanLyNhanSu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HopDong : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HopDongs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MaNhanVien = c.String(maxLength: 128),
                        NgayBatDau = c.DateTime(nullable: false),
                        NgayKetThuc = c.DateTime(nullable: false),
                        NgayKy = c.DateTime(nullable: false),
                        NoiDung = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.NHANVIENs", t => t.MaNhanVien)
                .Index(t => t.MaNhanVien);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HopDongs", "MaNhanVien", "dbo.NHANVIENs");
            DropIndex("dbo.HopDongs", new[] { "MaNhanVien" });
            DropTable("dbo.HopDongs");
        }
    }
}
