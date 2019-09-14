using System.ComponentModel.DataAnnotations;

namespace Rubrical.Models
{
    public class Privacy
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PrivacyName { get; set; }

        public string PrivacyDescription { get; set; }
    }
}