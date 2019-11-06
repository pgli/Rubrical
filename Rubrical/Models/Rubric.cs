using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rubrical.Models
{
    public class Rubric
    {
        public Rubric()
        {
            Rows = new List<Row>();
            Ratings = new List<Rating>();
            DateCreated = DateTime.Now;
            TotalRating = 0;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }

        [Required]
        public int GradeId { get; set; }

        [ForeignKey("GradeId")]
        public virtual Grade Grade { get; set; }

        public DateTime DateCreated { get; set; }

        public int? TotalRating { get; set; }

        public bool IsPrivate { get; set; }

        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser CreatedByUser { get; set; }

        public virtual List<Row> Rows { get; set; }

        public virtual List<Rating> Ratings { get; set; }
    }
}