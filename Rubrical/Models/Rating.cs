using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rubrical.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RubricId { get; set; }

        [ForeignKey("RubricId")]
        public virtual Rubric Rubric { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser RatedByUser { get; set; }

        [Required]
        public bool Value { get; set; }
    }
}
