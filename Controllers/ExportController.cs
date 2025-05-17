using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DoctorScheduleApp.Models;
using DoctorScheduleApp.Repositories;
using ClosedXML.Excel;
using Rotativa.AspNetCore;
using System.IO;
using System.ComponentModel;

namespace DoctorScheduleApp.Controllers
{
    public class ExportController : Controller
    {
        private readonly IDoctorRepository _doctorRepo;

        public ExportController(IDoctorRepository doctorRepo)
        {
            _doctorRepo = doctorRepo;
        }

        public IActionResult Index()
        {
            var data = _doctorRepo.GetAll();
            return View(data);
        }

        public IActionResult ExportToExcel()
        {
            var data = _doctorRepo.GetAll();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Jadwal Dokter");

            worksheet.Cell(1, 1).Value = "Nama Dokter";
            worksheet.Cell(1, 2).Value = "Spesialis";
            worksheet.Cell(1, 3).Value = "Hari";
            worksheet.Cell(1, 4).Value = "Jam";

            for (int i = 0; i < data.Count; i++)
            {
                worksheet.Cell(i + 2, 1).Value = data[i].NamaDokter;
                worksheet.Cell(i + 2, 2).Value = data[i].Spesialis;
                worksheet.Cell(i + 2, 3).Value = data[i].Hari;
                worksheet.Cell(i + 2, 4).Value = data[i].Jam;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "JadwalDokter.xlsx");
        }


        public IActionResult ExportToPdf()
        {
            var data = _doctorRepo.GetAll();
            return new ViewAsPdf("PdfTemplate", data)
            {
                FileName = "JadwalDokter.pdf"
            };
        }
    }
}