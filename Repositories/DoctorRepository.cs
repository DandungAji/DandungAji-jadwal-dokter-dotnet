using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using DoctorScheduleApp.Models;

namespace DoctorScheduleApp.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly IConfiguration _configuration;

        public DoctorRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<DoctorSchedule> GetAll()
        {
            var data = new List<DoctorSchedule>();
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("GetJadwalDokter", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                data.Add(new DoctorSchedule
                {
                    NamaDokter = reader["NamaDokter"].ToString(),
                    Spesialis = reader["Spesialis"].ToString(),
                    Hari = reader["Hari"].ToString(),
                    Jam = reader["Jam"].ToString()
                });
            }

            return data;
        }
    }
}
