using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rubrical.Models
{
    public class Row
    {
        public Row()
        {
            Cells = new List<Cell>();
        }

        [Key]
        public int Id { get; set; }

        public virtual List<Cell> Cells { get; set; }

        [Required]
        public int RubricId { get; set; }

        [ForeignKey("RubricId")]
        public virtual Rubric Rubric { get; set; }
    }
}
