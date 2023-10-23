using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UploadExcel_Mamoun.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("PortalDB")
        {
        }

        public DbSet<ExcelData> ExcelDatas { get; set; }

        public DbSet<UploadLog> UploadLogs { get; set; }

    }
}