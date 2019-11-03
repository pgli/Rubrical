using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rubrical.RubricModels
{
    public class CreateRubricModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0,int.MaxValue)]
        public int Subject { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Grade { get; set; }

        [Required]
        public bool IsPrivate { get; set; }


    }
}
