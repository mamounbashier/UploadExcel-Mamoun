using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UploadExcel_Mamoun.Models
{
    public class UploadViewModel
    {
        public string Message { get; set; }
        public List<ExcelData> UploadedData { get; set; } = new List<ExcelData>();
        public int NumberOfUploadedRecords => UploadedData.Count;

        public decimal TotalBasicSal { get; set; }
        public decimal TotalSal { get; set; }
        public decimal MaxBasicSal { get; set; }
        public decimal MaxSal { get; set; }

    }

}