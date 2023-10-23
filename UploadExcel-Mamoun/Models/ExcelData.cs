using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UploadExcel_Mamoun.Models
{
    public class ExcelData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }  // Auto-incremented primary key

        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOH { get; set; }
        public string Department { get; set; }
        public decimal BasicSal { get; set; }
        public decimal TotalSal { get; set; }
        public int Mflg { get; set; }

        public DateTime UploadDate { get; set; }
    }

}