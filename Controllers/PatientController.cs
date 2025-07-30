using Microsoft.AspNetCore.Mvc;
using HastaneRandevuSistemi.Models;

namespace HastaneRandevuSistemi.Controllers
{
    public class PatientController : Controller
    {
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
            return View();
        }

        public IActionResult BookAppointment()
        {
            ViewBag.Doctors = _doctors;
            return View();
        }

        [HttpPost]
        public IActionResult BookAppointment(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                appointment.Id = _appointments.Count + 1;
                appointment.PatientId = 1; // Default patient for demo
                appointment.Status = "Pending";
                appointment.CreatedAt = DateTime.Now;
                _appointments.Add(appointment);
                
                TempData["SuccessMessage"] = "Appointment booked successfully!";
                return RedirectToAction("MyAppointments");
            }
            
            ViewBag.Doctors = _doctors;
            return View(appointment);
        }

        public IActionResult MyAppointments()
        {
            var patientAppointments = _appointments
                .Where(a => a.PatientId == 1) // Default patient for demo
                .Select(a => new
                {
                    a.Id,
                    a.AppointmentDate,
                    a.TimeSlot,
                    a.Status,
                    a.PatientConcerns,
                    DoctorName = _doctors.FirstOrDefault(d => d.Id == a.DoctorId)?.FullName,
                    DoctorSpecialty = _doctors.FirstOrDefault(d => d.Id == a.DoctorId)?.Specialty
                }).ToList();
            
            return View(patientAppointments);
        }

        public IActionResult DoctorList()
        {
            return View(_doctors);
        }

        public IActionResult DoctorDetail(int id)
        {
            var doctor = _doctors.FirstOrDefault(d => d.Id == id);
            return View(doctor);
        }

        public IActionResult CancelAppointment(int id)
        {
            var appointment = _appointments.FirstOrDefault(a => a.Id == id);
            if (appointment != null)
            {
                appointment.Status = "Cancelled";
                appointment.UpdatedAt = DateTime.Now;
            }
            return RedirectToAction("MyAppointments");
        }

        public IActionResult Profile()
        {
            var patient = _patients.FirstOrDefault(p => p.Id == 1); // Default patient for demo
            return View(patient);
        }

        [HttpPost]
        public IActionResult UpdateProfile(Patient patient)
        {
            if (ModelState.IsValid)
            {
                var existingPatient = _patients.FirstOrDefault(p => p.Id == patient.Id);
                if (existingPatient != null)
                {
                    existingPatient.FirstName = patient.FirstName;
                    existingPatient.LastName = patient.LastName;
                    existingPatient.Email = patient.Email;
                    existingPatient.PhoneNumber = patient.PhoneNumber;
                    existingPatient.DateOfBirth = patient.DateOfBirth;
                    existingPatient.Gender = patient.Gender;
                    existingPatient.Address = patient.Address;
                    existingPatient.EmergencyContact = patient.EmergencyContact;
                    existingPatient.UpdatedAt = DateTime.Now;
                }
                
                TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToAction("Profile");
            }
            return View("Profile", patient);
        }
    }
}