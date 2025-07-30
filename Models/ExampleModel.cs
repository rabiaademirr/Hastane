using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models
{
    public class ExampleModel
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}