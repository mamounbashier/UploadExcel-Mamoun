using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UploadExcel_Mamoun.Models;

namespace UploadExcel_Mamoun.Controllers
{
    public class HomeController : Controller
    {
        // GET: Upload
        private readonly AppDbContext dbContext;

        public HomeController()
        {
            dbContext = new AppDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }
        private DateTime TryParseExcelDate(object excelValue)
        {
            if (excelValue == null)
                throw new ArgumentNullException(nameof(excelValue), "Excel cell value is null.");

            // Check if it's a valid OADate
            if (double.TryParse(excelValue.ToString(), out double oaDate))
            {
                try
                {
                    return DateTime.FromOADate(oaDate);
                }
                catch (ArgumentException)
                {
                    // Not a valid OADate
                }
            }

            // If it's not an OADate, try to parse using the ISO 8601 format
            if (DateTime.TryParseExact(excelValue.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                return date;

            // If it's not in ISO 8601 format, try to parse using 'M/d/yyyy hh:mm:ss tt' format
            if (DateTime.TryParseExact(excelValue.ToString(), "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                return date;

            // If it's not in 'M/d/yyyy hh:mm:ss tt' format, try 'dd/MM/yyyy' format
            if (DateTime.TryParseExact(excelValue.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                return date;

            throw new FormatException($"Cannot parse '{excelValue}' as a valid date in 'yyyy-MM-dd', 'M/d/yyyy hh:mm:ss tt', or 'dd/MM/yyyy' format, or as an OADate.");
        }





        [HttpPost]
        public ActionResult Index(HttpPostedFileBase chooseFile)
        {
            var currentUploadedData = new List<ExcelData>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (chooseFile != null && chooseFile.ContentLength > 0)
            {
                string fileExtension = Path.GetExtension(chooseFile.FileName).ToLower();
                if (fileExtension != ".xls" && fileExtension != ".xlsx")
                {
                    ViewBag.Message = "Please upload a valid Excel file.";
                    return View();
                }
                try
                {
                    using (var package = new ExcelPackage(chooseFile.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets.First();
                        var rowCount = currentSheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++) // Assuming first row has headers
                        {
                            var excelData = new ExcelData
                            {
                                        Name = currentSheet.Cells[row, 2].Text,
        Gender = currentSheet.Cells[row, 3].Text,
        DOB = TryParseExcelDate(currentSheet.Cells[row, 4].Value),
                                DOH = TryParseExcelDate(currentSheet.Cells[row, 5].Value),
        Department = currentSheet.Cells[row, 6].Text,
        BasicSal = decimal.Parse(currentSheet.Cells[row, 7].Value.ToString()),
        TotalSal = decimal.Parse(currentSheet.Cells[row, 8].Value.ToString()),
        Mflg = int.Parse(currentSheet.Cells[row, 9].Value.ToString()),
                                
                                UploadDate = DateTime.Now
                            };
                            currentUploadedData.Add(excelData);
                            dbContext.ExcelDatas.Add(excelData);
                        }

                        dbContext.SaveChanges();

                        // Log the upload
                        var logEntry = new UploadLog
                        {
                            UploadDate = DateTime.Now,
                            UploadedBy = User.Identity.Name,
                            FileName = chooseFile.FileName,
                            Status = "Success",
                            NumberOfRecords = rowCount - 1
                        };

                        dbContext.UploadLogs.Add(logEntry);
                        dbContext.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    // Log the failure
                    var logEntry = new UploadLog
                    {
                        UploadDate = DateTime.Now,
                        UploadedBy = User.Identity.Name,
                        FileName = chooseFile.FileName,
                        Status = "Failure",
                        ErrorDetails = ex.Message
                    };

                    dbContext.UploadLogs.Add(logEntry);
                    dbContext.SaveChanges();

                    // Return the error message to the view
                  //  ViewBag.ErrorMessage = "Error processing file: " + ex.Message;
                    TempData["Message"] = "Error processing file: " + ex.Message;
                   
                    return View("Index"); // or appropriate error view
                }

                //ViewBag.SuccessMessage = "File uploaded and processed successfully!";
                TempData["Message"] = "File uploaded and processed successfully!";


                // return RedirectToAction("Index");
                var totalBasicSal = currentUploadedData.Sum(x => x.BasicSal);
                var totalSal = currentUploadedData.Sum(x => x.TotalSal);
                decimal maxBasicSal = currentUploadedData.Max(e => e.BasicSal);
                decimal maxSal = currentUploadedData.Max(e => e.TotalSal);

                var viewModel = new UploadViewModel
                {
                    Message = "Upload successful",
                    UploadedData = currentUploadedData,
                    TotalBasicSal = totalBasicSal,
                    TotalSal = totalSal,
                    MaxBasicSal = maxBasicSal,    
                    MaxSal = maxSal              
                   
                };
                //System.Diagnostics.Debug.WriteLine($"MaxBasicSal: {viewModel.MaxBasicSal}, MaxSal: {viewModel.MaxSal}");
                return View("Index", viewModel);

            }
            else
            {
                ViewBag.Message = "Please select a file";
                return View();
            }
        }

    }
}