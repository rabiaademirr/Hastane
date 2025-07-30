using Microsoft.AspNetCore.Mvc;
using HastaneRandevuSistemi.Models;

namespace HastaneRandevuSistemi.Controllers
{
    public class AdminController : Controller
    {
        private static List<Admin> _admins = new List<Admin>
        {
            new Admin { Id = 1, FirstName = "Admin", LastName = "User", Email = "admin@hospital.com", Password = "admin123" }
        };
        
        private static List<Patient> _patients = new List<Patient>
        {
            new Patient { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com", PhoneNumber = "+1234567890", DateOfBirth = new DateTime(1990, 1, 1), Gender = "Male" },
            new Patient { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane@example.com", PhoneNumber = "+1234567891", DateOfBirth = new DateTime(1985, 5, 15), Gender = "Female" },
            new Patient { Id = 3, FirstName = "Mike", LastName = "Johnson", Email = "mike@example.com", PhoneNumber = "+1234567892", DateOfBirth = new DateTime(1992, 8, 20), Gender = "Male" }
        };
        
        private static List<Doctor> _doctors = new List<Doctor>
        {
            new Doctor { Id = 1, FirstName = "Ralph", LastName = "Edwards", Email = "ralph@hospital.com", PhoneNumber = "+1234567893", Specialty = "Dermatologist", ConsultationFee = 20, Experience = "Over 10 years in clinical dermatology", Bio = "Specialized in treating skin conditions and allergies." },
            new Doctor { Id = 2, FirstName = "Ronald", LastName = "Richards", Email = "ronald@hospital.com", PhoneNumber = "+1234567894", Specialty = "Neurologist", ConsultationFee = 25, Experience = "15 years of neurological practice", Bio = "Expert in neurological disorders and treatments." },
            new Doctor { Id = 3, FirstName = "Albert", LastName = "Boje", Email = "albert@hospital.com", PhoneNumber = "+1234567895", Specialty = "Dentist", ConsultationFee = 30, Experience = "12 years in dental care", Bio = "Specialized in cosmetic and general dentistry." },
            new Doctor { Id = 4, FirstName = "Floyd", LastName = "Miles", Email = "floyd@hospital.com", PhoneNumber = "+1234567896", Specialty = "Neurologist", ConsultationFee = 22, Experience = "8 years in neurology", Bio = "Focused on neurological diagnostics and treatment." },
            new Doctor { Id = 5, FirstName = "Leslie", LastName = "Alexander", Email = "leslie@hospital.com", PhoneNumber = "+1234567897", Specialty = "Psychiatrist", ConsultationFee = 35, Experience = "10 years in psychiatric care", Bio = "Specialized in mental health and therapy." }
        };
        
        private static List<Appointment> _appointments = new List<Appointment>();

        public IActionResult Index()
        {
            var dashboardData = new
            {
                TotalPatients = _patients.Count,
                TotalDoctors = _doctors.Count,
                TotalAppointments = _appointments.Count,
                PendingAppointments = _appointments.Count(a => a.Status == "Pending"),
                RecentAppointments = _appointments.Take(5).ToList()
            };
            
            return View(dashboardData);
        }

        public IActionResult Patients()
        {
            return View(_patients);
        }

        public IActionResult Doctors()
        {
            return View(_doctors);
        }

        public IActionResult Appointments()
        {
            var appointmentsWithDetails = _appointments.Select(a => new
            {
                a.Id,
                a.AppointmentDate,
                a.TimeSlot,
                a.Status,
                a.PatientConcerns,
                PatientName = _patients.FirstOrDefault(p => p.Id == a.PatientId)?.FullName,
                DoctorName = _doctors.FirstOrDefault(d => d.Id == a.DoctorId)?.FullName
            }).ToList();
            
            return View(appointmentsWithDetails);
        }

        [HttpPost]
        public IActionResult AddPatient(Patient patient)
        {
            if (ModelState.IsValid)
            {
                patient.Id = _patients.Count + 1;
                patient.CreatedAt = DateTime.Now;
                _patients.Add(patient);
                return RedirectToAction("Patients");
            }
            return View("Patients", _patients);
        }

        [HttpPost]
        public IActionResult AddDoctor(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                doctor.Id = _doctors.Count + 1;
                doctor.CreatedAt = DateTime.Now;
                _doctors.Add(doctor);
                return RedirectToAction("Doctors");
            }
            return View("Doctors", _doctors);
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
            return RedirectToAction("Appointments");
        }

        public IActionResult DeletePatient(int id)
        {
            var patient = _patients.FirstOrDefault(p => p.Id == id);
            if (patient != null)
            {
                _patients.Remove(patient);
            }
            return RedirectToAction("Patients");
        }

        public IActionResult DeleteDoctor(int id)
        {
            var doctor = _doctors.FirstOrDefault(d => d.Id == id);
            if (doctor != null)
            {
                _doctors.Remove(doctor);
            }
            return RedirectToAction("Doctors");
        }
    }
}