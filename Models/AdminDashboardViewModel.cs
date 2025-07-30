using System.Collections.Generic;

namespace HastaneRandevuSistemi.Models
{
    public class AdminDashboardViewModel
    {
        public int TotalAppointments { get; set; }
        public int TodayAppointments { get; set; }
        public int TotalDoctors { get; set; }
        public int TotalPatients { get; set; }
        public List<Appointment> RecentAppointments { get; set; } = new List<Appointment>();
    }
}