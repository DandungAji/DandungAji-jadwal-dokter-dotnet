namespace DoctorScheduleApp.Models
{
	public class DoctorSchedule
	{
		public required string DoctorName { get; set; }
		public required string Specialization { get; set; }
		public required string PracticeDay { get; set; }
		public required string PracticeTime { get; set; }
	}
}