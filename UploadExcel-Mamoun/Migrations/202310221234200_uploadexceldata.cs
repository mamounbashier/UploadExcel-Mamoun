namespace UploadExcel_Mamoun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uploadexceldata : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExcelDatas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LEAD_ID = c.String(),
                        LEAD_NAME = c.String(),
                        LEAD_DESC = c.String(),
                        LEAD_CODE = c.String(),
                        UploadDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExcelDatas");
        }
    }
}
