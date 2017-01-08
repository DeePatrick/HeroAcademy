using System.ComponentModel.DataAnnotations;

namespace HeroAcademy.Models
{
    public class Course
    {
        public byte Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}