using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rubrical.Models
{
    public class Rubric
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid Guid { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }

        [Required]
        public int GradeId { get; set; }

        [ForeignKey("GradeId")]
        public virtual Grade Grade { get; set; }

        [Required]
        public int PrivacyId { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public int? TotalRating { get; set; }
    }
}