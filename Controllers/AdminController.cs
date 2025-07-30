using Microsoft.AspNetCore.Mvc;
using HastaneRandevuSistemi.Models;
using System.Collections.Generic;
using System.Linq;

namespace HastaneRandevuSistemi.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            // Örnek veri - gerçek uygulamada veritabanından gelecek
            var model = new AdminDashboardViewModel
            {
                TotalAppointments = 150,
                TodayAppointments = 12,
                TotalDoctors = 25,
                TotalPatients = 500,
                RecentAppointments = new List<Appointment>
                {
                    new Appointment
                    {
                        Id = 1,
                        PatientName = "Ahmet Yılmaz",
                        DoctorName = "Dr. Mehmet Kaya",
                        AppointmentDate = System.DateTime.Today.AddDays(1),
                        AppointmentTime = "09:00",
                        Status = "Confirmed"
                    },
                    new Appointment
                    {
                        Id = 2,
                        PatientName = "Fatma Demir",
                        DoctorName = "Dr. Ayşe Özkan",
                        AppointmentDate = System.DateTime.Today.AddDays(2),
                        AppointmentTime = "14:30",
                        Status = "Pending"
                    },
                    new Appointment
                    {
                        Id = 3,
                        PatientName = "Ali Veli",
                        DoctorName = "Dr. Mustafa Çelik",
                        AppointmentDate = System.DateTime.Today.AddDays(3),
                        AppointmentTime = "11:15",
                        Status = "Confirmed"
                    }
                }
            };

            return View(model);
        }
    }
}