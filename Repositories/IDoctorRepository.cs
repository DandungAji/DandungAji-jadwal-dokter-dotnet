using DoctorScheduleApp.Models;
using System.Collections.Generic;

namespace DoctorScheduleApp.Repositories
{
    public interface IDoctorRepository
    {
        List<DoctorSchedule> GetAll();
    }
}