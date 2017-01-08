using System.ComponentModel.DataAnnotations;

namespace HeroAcademy.Models
{
    public class Qualification
    {
        public byte Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }
    }
}