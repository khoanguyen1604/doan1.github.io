namespace QuanLyNhanSu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class luongcoban_nhanvien : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NHANVIENs", "LuongCoBan", c => c.Double(nullable: false));
            DropColumn("dbo.BangLuongs", "LuongCoBan");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BangLuongs", "LuongCoBan", c => c.Double(nullable: false));
            DropColumn("dbo.NHANVIENs", "LuongCoBan");
        }
    }
}
