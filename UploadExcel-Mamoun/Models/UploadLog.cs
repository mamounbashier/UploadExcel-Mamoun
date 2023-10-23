using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UploadExcel_Mamoun.Models
{
    public class UploadLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Unique identifier for each log entry
        public DateTime UploadDate { get; set; } // When the upload took place
        public string UploadedBy { get; set; } // Username or identifier of the person who uploaded the file
        public string FileName { get; set; } // Name of the uploaded file
        public int NumberOfRecords { get; set; } // Number of records processed from the file
        public string Status { get; set; } // Success, Partial Failure, Failure
        public string ErrorDetails { get; set; } // Detailed error message if the upload failed
    }

}