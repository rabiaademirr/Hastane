using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models
{
    public class Patient
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        public string Gender { get; set; }
        
        public string Address { get; set; }
        
        public string EmergencyContact { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; }
        
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
        
        public string FullName => $"{FirstName} {LastName}";
    }
}