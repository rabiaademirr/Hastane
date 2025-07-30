using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models
{
    public class Doctor
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
        
        [Required]
        public string Specialty { get; set; }
        
        public string Experience { get; set; }
        
        public decimal ConsultationFee { get; set; }
        
        public string Bio { get; set; }
        
        public string ProfileImage { get; set; }
        
        public bool IsAvailable { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; }
        
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
        
        public string FullName => $"Dr. {FirstName} {LastName}";
    }
}