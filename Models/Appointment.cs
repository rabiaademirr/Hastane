using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        
        [Required]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        
        [Required]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        
        [Required]
        public DateTime AppointmentDate { get; set; }
        
        [Required]
        public string TimeSlot { get; set; }
        
        public string PatientConcerns { get; set; }
        
        public string Status { get; set; } = "Pending"; // Pending, Confirmed, Completed, Cancelled
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; }
    }
}