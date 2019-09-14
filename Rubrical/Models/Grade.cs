using System.ComponentModel.DataAnnotations;

namespace Rubrical.Models
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string GradeName { get; set; }

        public string GradeDescription { get; set; }
    }
}