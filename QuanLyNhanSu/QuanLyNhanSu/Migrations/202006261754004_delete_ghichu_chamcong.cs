namespace QuanLyNhanSu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delete_ghichu_chamcong : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CHAMCONGs", "GhiChu");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CHAMCONGs", "GhiChu", c => c.String());
        }
    }
}
