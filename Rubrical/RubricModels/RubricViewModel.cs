using Rubrical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubrical.RubricModels
{
    public class RubricViewModel
    {
        public List<Subject> Subjects { get; set; }

        public List<Grade> Grades { get; set; }
    }
}
