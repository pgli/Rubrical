using System.ComponentModel.DataAnnotations;

namespace Rubrical.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SubjectName { get; set; }

        public string SubjectDescription { get; set; }
    }
}