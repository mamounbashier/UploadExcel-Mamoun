namespace UploadExcel_Mamoun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatexceldata : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExcelDatas", "Name", c => c.String());
            AddColumn("dbo.ExcelDatas", "Gender", c => c.String());
            AddColumn("dbo.ExcelDatas", "DOB", c => c.DateTime(nullable: false));
            AddColumn("dbo.ExcelDatas", "DOH", c => c.DateTime(nullable: false));
            AddColumn("dbo.ExcelDatas", "Department", c => c.String());
            AddColumn("dbo.ExcelDatas", "BasicSal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ExcelDatas", "TotalSal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ExcelDatas", "Mflg", c => c.Int(nullable: false));
            DropColumn("dbo.ExcelDatas", "LEAD_ID");
            DropColumn("dbo.ExcelDatas", "LEAD_NAME");
            DropColumn("dbo.ExcelDatas", "LEAD_DESC");
            DropColumn("dbo.ExcelDatas", "LEAD_CODE");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ExcelDatas", "LEAD_CODE", c => c.String());
            AddColumn("dbo.ExcelDatas", "LEAD_DESC", c => c.String());
            AddColumn("dbo.ExcelDatas", "LEAD_NAME", c => c.String());
            AddColumn("dbo.ExcelDatas", "LEAD_ID", c => c.String());
            DropColumn("dbo.ExcelDatas", "Mflg");
            DropColumn("dbo.ExcelDatas", "TotalSal");
            DropColumn("dbo.ExcelDatas", "BasicSal");
            DropColumn("dbo.ExcelDatas", "Department");
            DropColumn("dbo.ExcelDatas", "DOH");
            DropColumn("dbo.ExcelDatas", "DOB");
            DropColumn("dbo.ExcelDatas", "Gender");
            DropColumn("dbo.ExcelDatas", "Name");
        }
    }
}
