using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rubrical.Models
{
    public class Cell
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int ColumnId { get; set; }

        [ForeignKey("ColumnId")]
        public virtual Column Column { get; set; }
    }
}
