using Microsoft.AspNetCore.Mvc;
using HastaneRandevuSistemi.Models;

namespace HastaneRandevuSistemi.Controllers
{
    public class DoctorController : Controller
    {
        private static List<Doctor> _doctors = new List<Doctor>
        {
            new Doctor { Id = 1, FirstName = "Ralph", LastName = "Edwards", Email = "ralph@hospital.com", PhoneNumber = "+1234567893", Specialty = "Dermatologist", ConsultationFee = 20, Experience = "Over 10 years in clinical dermatology", Bio = "Specialized in treating skin conditions and allergies." },
            new Doctor { Id = 2, FirstName = "Ronald", LastName = "Richards", Email = "ronald@hospital.com", PhoneNumber = "+1234567894", Specialty = "Neurologist", ConsultationFee = 25, Experience = "15 years of neurological practice", Bio = "Expert in neurological disorders and treatments." },
            new Doctor { Id = 3, FirstName = "Albert", LastName = "Boje", Email = "albert@hospital.com", PhoneNumber = "+1234567895", Specialty = "Dentist", ConsultationFee = 30, Experience = "12 years in dental care", Bio = "Specialized in cosmetic and general dentistry." },
            new Doctor { Id = 4, FirstName = "Floyd", LastName = "Miles", Email = "floyd@hospital.com", PhoneNumber = "+1234567896", Specialty = "Neurologist", ConsultationFee = 22, Experience = "8 years in neurology", Bio = "Focused on neurological diagnostics and treatment." },
            new Doctor { Id = 5, FirstName = "Leslie", LastName = "Alexander", Email = "leslie@hospital.com", PhoneNumber = "+1234567897", Specialty = "Psychiatrist", ConsultationFee = 35, Experience = "10 years in psychiatric care", Bio = "Specialized in mental health and therapy." }
        };
        
        private static List<Patient> _patients = new List<Patient>
        {
            new Patient { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com", PhoneNumber = "+1234567890", DateOfBirth = new DateTime(1990, 1, 1), Gender = "Male" },
            new Patient { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane@example.com", PhoneNumber = "+1234567891", DateOfBirth = new DateTime(1985, 5, 15), Gender = "Female" },
            new Patient { Id = 3, FirstName = "Mike", LastName = "Johnson", Email = "mike@example.com", PhoneNumber = "+1234567892", DateOfBirth = new DateTime(1992, 8, 20), Gender = "Male" }
        };
        
        private static List<Appointment> _appointments = new List<Appointment>();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyAppointments()
        {
            var doctorId = 1; // Default doctor for demo
            var doctorAppointments = _appointments
                .Where(a => a.DoctorId == doctorId)
                .Select(a => new
                {
                    a.Id,
                    a.AppointmentDate,
                    a.TimeSlot,
                    a.Status,
                    a.PatientConcerns,
                    PatientName = _patients.FirstOrDefault(p => p.Id == a.PatientId)?.FullName,
                    PatientEmail = _patients.FirstOrDefault(p => p.Id == a.PatientId)?.Email,
                    PatientPhone = _patients.FirstOrDefault(p => p.Id == a.PatientId)?.PhoneNumber
                }).ToList();
            
            return View(doctorAppointments);
        }

        [HttpPost]
        public IActionResult UpdateAppointmentStatus(int appointmentId, string status)
        {
            var appointment = _appointments.FirstOrDefault(a => a.Id == appointmentId);
            if (appointment != null)
            {
                appointment.Status = status;
                appointment.UpdatedAt = DateTime.Now;
            }
            return RedirectToAction("MyAppointments");
        }

        public IActionResult Schedule()
        {
            var doctorId = 1; // Default doctor for demo
            var today = DateTime.Today;
            var nextWeek = today.AddDays(7);
            
            var scheduleData = new
            {
                Doctor = _doctors.FirstOrDefault(d => d.Id == doctorId),
                Today = today,
                NextWeek = nextWeek,
                AvailableSlots = GetAvailableTimeSlots()
            };
            
            return View(scheduleData);
        }

        public IActionResult Profile()
        {
            var doctor = _doctors.FirstOrDefault(d => d.Id == 1); // Default doctor for demo
            return View(doctor);
        }

        [HttpPost]
        public IActionResult UpdateProfile(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                var existingDoctor = _doctors.FirstOrDefault(d => d.Id == doctor.Id);
                if (existingDoctor != null)
                {
                    existingDoctor.FirstName = doctor.FirstName;
                    existingDoctor.LastName = doctor.LastName;
                    existingDoctor.Email = doctor.Email;
                    existingDoctor.PhoneNumber = doctor.PhoneNumber;
                    existingDoctor.Specialty = doctor.Specialty;
                    existingDoctor.Experience = doctor.Experience;
                    existingDoctor.ConsultationFee = doctor.ConsultationFee;
                    existingDoctor.Bio = doctor.Bio;
                    existingDoctor.IsAvailable = doctor.IsAvailable;
                    existingDoctor.UpdatedAt = DateTime.Now;
                }
                
                TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToAction("Profile");
            }
            return View("Profile", doctor);
        }

        public IActionResult Patients()
        {
            var doctorId = 1; // Default doctor for demo
            var doctorPatients = _appointments
                .Where(a => a.DoctorId == doctorId)
                .Select(a => _patients.FirstOrDefault(p => p.Id == a.PatientId))
                .Where(p => p != null)
                .Distinct()
                .ToList();
            
            return View(doctorPatients);
        }

        private List<string> GetAvailableTimeSlots()
        {
            return new List<string>
            {
                "08:00 AM", "09:00 AM", "10:00 AM", "11:00 AM",
                "12:30 PM", "01:30 PM", "02:30 PM", "03:30 PM",
                "04:30 PM", "05:30 PM"
            };
        }
    }
}