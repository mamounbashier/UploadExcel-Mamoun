namespace UploadExcel_Mamoun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUploadLog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UploadLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UploadDate = c.DateTime(nullable: false),
                        UploadedBy = c.String(),
                        FileName = c.String(),
                        NumberOfRecords = c.Int(nullable: false),
                        Status = c.String(),
                        ErrorDetails = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UploadLogs");
        }
    }
}
