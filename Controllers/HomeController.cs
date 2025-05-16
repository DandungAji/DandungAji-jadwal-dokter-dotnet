using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DoctorScheduleApp.Models;
using ClosedXML.Excel;
using Rotativa.AspNetCore;

namespace DoctorScheduleApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var schedules = new List<DoctorSchedule>
        {
            new DoctorSchedule { DoctorName = "dr. Ricki Rajagukguk, Sp. A", Specialization = "Spesialis Anak", PracticeDay = "Senin", PracticeTime = "08.00-11.00"},
            new DoctorSchedule { DoctorName = "dr. Yogie Setyabudi, Sp. PD", Specialization = "Spesialis Penyakit Dalam", PracticeDay = "Selasa", PracticeTime = "08.00-10.00"},
            new DoctorSchedule { DoctorName = "dr. Andi, Sp. B", Specialization = "Spesialis Bedah", PracticeDay = "Rabu", PracticeTime = "13.30-15.00"}
        };
        return View(schedules);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult ExportToExcel()
    {
        var schedules = new List<DoctorSchedule>
        {
            new DoctorSchedule { DoctorName = "dr. Ricki Rajagukguk, Sp. A", Specialization = "Spesialis Anak", PracticeDay = "Senin", PracticeTime = "08.00-11.00"},
            new DoctorSchedule { DoctorName = "dr. Yogie Setyabudi, Sp. PD", Specialization = "Spesialis Penyakit Dalam", PracticeDay = "Selasa", PracticeTime = "08.00-10.00"},
            new DoctorSchedule { DoctorName = "dr. Andi, Sp. B", Specialization = "Spesialis Bedah", PracticeDay = "Rabu", PracticeTime = "13.30-15.00"}
        };

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Jadwal Dokter");
            worksheet.Cell(1, 1).Value = "Nama Dokter";
            worksheet.Cell(1, 2).Value = "Spesialisasi";
            worksheet.Cell(1, 3).Value = "Hari Praktik";
            worksheet.Cell(1, 4).Value = "Jam Praktik";

            for (int i = 0; i < schedules.Count; i++)
            {
                worksheet.Cell(i + 2, 1).Value = schedules[i].DoctorName;
                worksheet.Cell(i + 2, 2).Value = schedules[i].Specialization;
                worksheet.Cell(i + 2, 3).Value = schedules[i].PracticeDay;
                worksheet.Cell(i + 2, 4).Value = schedules[i].PracticeTime;
            }

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Jadwal_Dokter.xlsx");
            }
        }
    }

    public IActionResult ExportToPdf()
    {
        var schedules = new List<DoctorSchedule>
        {
            new DoctorSchedule { DoctorName = "dr. Ricki Rajagukguk, Sp. A", Specialization = "Spesialis Anak", PracticeDay = "Senin", PracticeTime = "08.00-11.00"},
            new DoctorSchedule { DoctorName = "dr. Yogie Setyabudi, Sp. PD", Specialization = "Spesialis Penyakit Dalam", PracticeDay = "Selasa", PracticeTime = "08.00-10.00"},
            new DoctorSchedule { DoctorName = "dr. Andi, Sp. B", Specialization = "Spesialis Bedah", PracticeDay = "Rabu", PracticeTime = "13.30-15.00"}
        };
        return new ViewAsPdf("ExportToPdf", schedules)
        {
            FileName = "Jadwal_Dokter.pdf"
        };
    }
}
